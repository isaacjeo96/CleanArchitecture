using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

/// <summary>
/// Contiene los errores de dominio específicos relacionados con la creación de reseñas.
/// Estos errores representan las reglas de negocio que pueden impedir que un usuario califique un alquiler.
/// </summary>
public static class ReviewErrors
{
    /// <summary>
    /// Error que indica que no es posible crear una reseña porque el alquiler aún no ha sido completado.
    /// </summary>
    public static readonly Error NotEligible = new(
        "Review.NotEligible",
        "Este review y calificación no es elegible porque aún no se completa el alquiler"
    );
}
