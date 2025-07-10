using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

/// <summary>
/// Evento de dominio que se dispara cuando un alquiler es cancelado.
/// Permite que otras partes del sistema reaccionen ante la cancelación de un alquiler,
/// como liberar la disponibilidad del vehículo, notificar al usuario, etc.
/// </summary>
/// <param name="AlquilerId">
/// Identificador único del alquiler que fue cancelado.
/// </param>
public sealed record AlquilerCanceladoDomainEvent(Guid AlquilerId) : IDomainEvent;
