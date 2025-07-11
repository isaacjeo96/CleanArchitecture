using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

/// <summary>
/// Evento de dominio que se dispara cuando un alquiler es completado.
/// Permite que otras partes del sistema reaccionen ante la finalización exitosa del alquiler,
/// como actualizar estadísticas, liberar el vehículo para futuros alquileres o notificar al usuario.
/// </summary>
/// <param name="AlquilerId">
/// Identificador único del alquiler que fue completado.
/// </param>
public sealed record AlquilerCompletadoDomainEvent(Guid AlquilerId) : IDomainEvent;
