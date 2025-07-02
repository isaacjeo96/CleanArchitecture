using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

/// <summary>
/// Evento de dominio que se dispara cuando un alquiler ha sido reservado exitosamente.
/// Este evento permite que otras partes del sistema reaccionen ante la creación de una nueva reserva,
/// como enviar notificaciones, actualizar disponibilidad, entre otros.
/// </summary>
/// <param name="AlquilerId">Identificador único del alquiler recién reservado.</param>
public sealed record AlquilerReservadoDomainEvent(Guid AlquilerId) : IDomainEvent;
