using CleanArchitecture.Application.Abstractions.Clock;           // Proveedor de fecha/hora (abstracción)
using CleanArchitecture.Application.Abstractions.Messaging;      // Interfaces CQRS (ICommandHandler)
using CleanArchitecture.Domain.Abstractions;                     // Result, IUnitOfWork
using CleanArchitecture.Domain.Alquileres;                       // Entidad Alquiler, DateRange, AlquilerErrors, PrecioService
using CleanArchitecture.Domain.Users;                            // IUserRepository, UserErrors
using CleanArchitecture.Domain.Vehiculos;                        // IVehiculoRepository, VehiculoErrors

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    /// <summary>
    /// Handler que procesa el comando <see cref="ReservarAlquilerCommand"/>.
    /// Orquesta el flujo de reservar un alquiler:
    /// 1) Verifica que el usuario y el vehículo existen.
    /// 2) Valida que el vehículo esté disponible en el rango de fechas.
    /// 3) Crea la reserva utilizando la lógica del dominio.
    /// 4) Persiste los cambios usando el patrón Unit of Work.
    /// Retorna el <c>Guid</c> del nuevo alquiler si la operación es exitosa.
    /// </summary>
    internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
    {
        // --- Dependencias inyectadas ---
        private readonly IUserRepository _userRepository;           // Acceso y gestión de usuarios
        private readonly IVehiculoRepository _vehiculoRepository;   // Acceso y gestión de vehículos
        private readonly IAlquilerRepository _alquilerRepository;   // Acceso y gestión de alquileres
        private readonly PrecioService _precioService;              // Cálculo de precios
        private readonly IUniteOfWork _uniteOfWork;                  // Transacción atómica
        private readonly IDateTimeProvider _dateTimeProvider;       // Fecha/hora actual

        /// <summary>
        /// Constructor que recibe todas las dependencias necesarias para ejecutar el caso de uso.
        /// </summary>
        public ReservarAlquilerCommandHandler(
            IUserRepository userRepository,
            IVehiculoRepository vehiculoRepository,
            IAlquilerRepository alquilerRepository,
            PrecioService precioService,
            IUniteOfWork uniteOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _userRepository = userRepository;
            _vehiculoRepository = vehiculoRepository;
            _alquilerRepository = alquilerRepository;
            _precioService = precioService;
            _uniteOfWork = uniteOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Ejecuta el comando para reservar un alquiler, aplicando validaciones de negocio y registrando la reserva.
        /// </summary>
        /// <param name="request">Datos de la solicitud de reserva.</param>
        /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
        /// <returns>
        /// Un <see cref="Result{Guid}"/> con el identificador del nuevo alquiler si es exitoso,
        /// o un error de dominio si falla alguna validación.
        /// </returns>
        public async Task<Result<Guid>> Handle(
            ReservarAlquilerCommand request,
            CancellationToken cancellationToken)
        {
            // 1) Verificar que el usuario existe
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Failure<Guid>(UserErros.NotFound);
            }

            // 2) Verificar que el vehículo existe
            var vehiculo = await _vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);
            if (vehiculo is null)
            {
                return Result.Failure<Guid>(VehiculoErrors.NotFound);
            }

            // 3) Crear el rango de fechas del alquiler (valida que inicio <= fin)
            var duracion = DateRange.Create(request.FechaInicio, request.FechaFin);

            // 4) Validar que no haya otra reserva en el mismo período
            if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
            {
                return Result.Failure<Guid>(AlquilerErrors.Overlap);
            }

            // 5) Crear la reserva desde el dominio
            var alquier = Alquiler.Reservar(
                vehiculo,
                user.Id,
                duracion,
                _dateTimeProvider.currenTime, // Usamos proveedor de fecha/hora
                _precioService);

            // 6) Agregar la nueva reserva al repositorio
            _alquilerRepository.Add(alquier);

            // 7) Guardar todos los cambios de forma atómica
            await _uniteOfWork.SaveChangesAsync(cancellationToken);

            // 8) Devolver el identificador del alquiler creado
            return alquier.Id;
        }
    }
}
