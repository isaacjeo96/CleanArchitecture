using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

/// <summary>
/// Evento de dominio que se dispara cuando un alquiler es confirmado.
/// Permite que otras partes del sistema reaccionen ante la confirmación del alquiler,
/// como bloquear la disponibilidad del vehículo, generar notificaciones o registrar auditorías.
/// </summary>
/// <param name="AlquilerId">
/// Identificador único del alquiler que fue confirmado.
/// </param>
public sealed record AlquilerConfirmadoDomainEvent(Guid AlquilerId) : IDomainEvent;
