using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

/// <summary>
/// Comando que representa la solicitud de reservar un alquiler de vehículo.
/// Contiene toda la información necesaria para que el sistema procese la reserva:
/// - El vehículo que se quiere alquilar.
/// - El usuario que realiza la reserva.
/// - El periodo de tiempo solicitado (fecha de inicio y fecha de fin).
/// </summary>
/// <remarks>
/// Este comando sigue el patrón CQRS y se envía a través de MediatR, 
/// donde será manejado por un <see cref="ReservarAlquilerCommandHandler"/> 
/// para aplicar las reglas de negocio.
/// </remarks>
/// <param name="VehiculoId">Identificador único del vehículo a reservar.</param>
/// <param name="UserId">Identificador único del usuario que realiza la reserva.</param>
/// <param name="FechaInicio">Fecha en la que se desea iniciar el alquiler.</param>
/// <param name="FechaFin">Fecha en la que finaliza el alquiler.</param>
public record ReservarAlquilerCommand(
    Guid VehiculoId, // 📌 ID único del vehículo a alquilar.
    Guid UserId,     // 📌 ID único del usuario que está realizando la reserva.
    DateOnly FechaInicio, // 📅 Día en que comienza el alquiler.
    DateOnly FechaFin     // 📅 Día en que termina el alquiler.
) : ICommand<Guid>; // 🔄 Implementa ICommand indicando que devolverá un Guid (ID del alquiler creado).
