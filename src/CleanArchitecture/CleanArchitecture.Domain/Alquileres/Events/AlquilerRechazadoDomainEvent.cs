using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

/// <summary>
/// Evento de dominio que se dispara cuando un alquiler es rechazado.
/// Permite que otras partes del sistema reaccionen ante el rechazo del alquiler,
/// como notificar al usuario o actualizar reportes de disponibilidad.
/// </summary>
/// <param name="AlquilerId">
/// Identificador único del alquiler que fue rechazado.
/// </param>
public sealed record AlquilerRechazadoDomainEvent(Guid AlquilerId) : IDomainEvent;
