using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews.Events;

namespace CleanArchitecture.Domain.Reviews;

/// <summary>
/// Representa una reseña o calificación realizada por un usuario sobre un alquiler completado.
/// Incluye información sobre el vehículo, el usuario, la puntuación (como objeto de valor), el comentario (como objeto de valor) y la fecha de creación.
/// </summary>
public sealed class Review : Entity
{
    /// <summary>
    /// Constructor privado. Se debe utilizar el método <see cref="Create"/> para instanciar una nueva reseña.
    /// </summary>
    private Review(
        Guid id,
        Guid vehiculoId,
        Guid alquilerId,
        Guid userId,
        Raiting raiting,
        Comentario comentario,
        DateTime fechaCreacion) : base(id)
    {
        VehiculoId = vehiculoId;
        AlquilerId = alquilerId;
        UserId = userId;
        Raiting = raiting;
        Comentario = comentario;
        FechaCreacion = fechaCreacion;
    }

    /// <summary>
    /// Identificador del vehículo al que pertenece la reseña.
    /// </summary>
    public Guid VehiculoId { get; private set; }

    /// <summary>
    /// Identificador del alquiler relacionado con la reseña.
    /// </summary>
    public Guid AlquilerId { get; private set; }

    /// <summary>
    /// Identificador del usuario que hizo la reseña.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Calificación otorgada al vehículo o al servicio, representada como objeto de valor <see cref="Raiting"/>.
    /// </summary>
    public Raiting Raiting { get; private set; }

    /// <summary>
    /// Comentario adicional proporcionado por el usuario, representado como objeto de valor <see cref="Comentario"/>.
    /// </summary>
    public Comentario? Comentario { get; private set; }

    /// <summary>
    /// Fecha en la que se creó la reseña.
    /// </summary>
    public DateTime? FechaCreacion { get; set; }

    /// <summary>
    /// Método de fábrica para crear una nueva reseña sobre un alquiler completado.
    /// Valida que el alquiler esté en estado <see cref="AlquilerStatus.Completado"/> antes de permitir la creación.
    /// Dispara un evento de dominio al ser creada.
    /// </summary>
    /// <param name="alquiler">Instancia del alquiler sobre la que se realiza la reseña.</param>
    /// <param name="raiting">Puntuación otorgada por el usuario como <see cref="Raiting"/>.</param>
    /// <param name="comentario">Comentario adicional del usuario como <see cref="Comentario"/>.</param>
    /// <param name="fechaCreacion">Fecha y hora de creación de la reseña.</param>
    /// <returns>
    /// Un resultado exitoso con la reseña creada si las validaciones son correctas,
    /// o un <see cref="Result.Failure{T}"/> con <see cref="ReviewErrors.NotEligible"/> si el alquiler no es elegible.
    /// </returns>
    public static Result<Review> Create(
        Alquiler alquiler,
        Raiting raiting,
        Comentario comentario,
        DateTime fechaCreacion)
    {
        if (alquiler.Status != AlquilerStatus.Completado)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(
            Guid.NewGuid(),
            alquiler.VehiculoId,
            alquiler.Id,
            alquiler.UserId,
            raiting,
            comentario,
            fechaCreacion);

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return review;
    }
}
