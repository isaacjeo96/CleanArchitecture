using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews.Events;

/// <summary>
/// Evento de dominio que se dispara cuando se crea una nueva reseña para un alquiler.
/// Permite que otras partes del sistema reaccionen ante la creación de la reseña,
/// como actualizar estadísticas, enviar notificaciones al propietario del vehículo o registrar en auditorías.
/// </summary>
/// <param name="AlquilerId">
/// Identificador único del alquiler al que pertenece la reseña creada.
/// </param>
public sealed record ReviewCreatedDomainEvent(Guid AlquilerId) : IDomainEvent;
