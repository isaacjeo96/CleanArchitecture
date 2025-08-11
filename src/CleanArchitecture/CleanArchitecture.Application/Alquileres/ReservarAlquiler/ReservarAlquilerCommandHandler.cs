using CleanArchitecture.Application.Abstractions.Messaging; // Interfaces CQRS (ICommandHandler)
using CleanArchitecture.Domain.Abstractions;               // Result, IUnitOfWork (aquí está tu IUniteOfWork)
using CleanArchitecture.Domain.Alquileres;                 // Entidad Alquiler, DateRange, AlquilerErrors, PrecioService
using CleanArchitecture.Domain.Users;                      // IUserRepository, UserErrors
using CleanArchitecture.Domain.Vehiculos;                  // IVehiculoRepository, VehiculoErrors

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

/// <summary>
/// Handler (manejador) del comando <see cref="ReservarAlquilerCommand"/>.
/// Orquesta el caso de uso "reservar un alquiler":
/// 1) Valida existencia de usuario y vehículo.
/// 2) Valida disponibilidad (no solapamiento).
/// 3) Crea la reserva mediante la entidad de dominio.
/// 4) Persiste cambios vía repositorio + Unit of Work.
/// Devuelve el <c>Guid</c> del alquiler creado si todo es correcto.
/// </summary>
internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
{
    // Dependencias necesarias para resolver el caso de uso (inyección de dependencias).
    private readonly IUserRepository _userRepository;           // Acceso/lectura de usuarios
    private readonly IVehiculoRepository _vehiculoRepository;   // Acceso/lectura de vehículos
    private readonly IAlquilerRepository _alquilerRepository;   // Persistencia/consultas de alquileres
    private readonly PrecioService _precioService;              // Lógica de cálculo de precio (servicio de dominio)
    private readonly IUniteOfWork _uniteOfWork;                 // Confirmación atómica de cambios (transacción)

    /// <summary>
    /// Constructor con inyección de dependencias.
    /// </summary>
    public ReservarAlquilerCommandHandler(
        IUserRepository userRepository,
        IVehiculoRepository vehiculoRepository,
        IAlquilerRepository alquilerRepository,
        PrecioService precioService,
        IUniteOfWork uniteOfWork)
    {
        _userRepository = userRepository;             // Guarda repo de usuarios
        _vehiculoRepository = vehiculoRepository;     // Guarda repo de vehículos
        _alquilerRepository = alquilerRepository;     // Guarda repo de alquileres
        _precioService = precioService;               // Guarda servicio de precios
        _uniteOfWork = uniteOfWork;                   // Guarda unidad de trabajo
    }

    /// <summary>
    /// Maneja el comando de reserva. Aplica validaciones de negocio y persiste el resultado.
    /// </summary>
    /// <param name="request">Datos de la reserva: vehículo, usuario y fechas.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>
    /// <see cref="Result{T}"/> con el <c>Guid</c> del alquiler creado en caso de éxito;
    /// o <c>Result.Failure</c> con el error de dominio correspondiente.
    /// </returns>
    public async Task<Result<Guid>> Handle(
        ReservarAlquilerCommand request,
        CancellationToken cancellationToken)
    {
        // 1) Validar que el usuario exista
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            // Si no existe, devolvemos fallo con el error de dominio apropiado
            // (nota: en tu código está como UserErros.NotFound; idealmente UserErrors.NotFound)
            return Result.Failure<Guid>(UserErros.NotFound);
        }

        // 2) Validar que el vehículo exista
        var vehiculo = await _vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);

        if (vehiculo is null)
        {
            // Si no existe, devolvemos el error de dominio correspondiente
            return Result.Failure<Guid>(VehiculoErrors.NotFound);
        }

        // 3) Crear el objeto de valor para la duración (valida inicio/fin)
        var duracion = DateRange.Create(request.FechaInicio, request.FechaFin);

        // 4) Verificar solapamiento: si hay otra reserva para el mismo vehículo/periodo
        if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
        {
            // Si hay solape, regresamos error del dominio
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }

        // 5) Crear la reserva desde el dominio (método de fábrica)
        //    - Calcula precios con PrecioService
        //    - Deja estado en Reservado
        //    - Dispara evento de dominio AlquilerReservadoDomainEvent
        var alquier = Alquiler.Reservar(
            vehiculo,
            user.Id,
            duracion,
            DateTime.UtcNow,   // Siempre en UTC para consistencia
            _precioService);

        // 6) Registrar la nueva reserva para persistir
        _alquilerRepository.Add(alquier);

        // 7) Confirmar cambios en una sola transacción
        await _uniteOfWork.SaveChangesAsync(cancellationToken);

        // 8) Devolver el Id generado del alquiler (implicit cast a Result<Guid> si lo tienes definido;
        //    si no, sería: Result.Success(alquier.Id))
        return alquier.Id;
    }
}
