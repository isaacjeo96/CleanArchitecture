using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Contiene los errores de dominio específicos relacionados con las operaciones sobre un <see cref="Alquiler"/>.
/// Estos errores representan reglas de negocio que pueden fallar durante el ciclo de vida de un alquiler.
/// </summary>
public static class AlquilerErrors
{
    /// <summary>
    /// Error que ocurre cuando no se encuentra un alquiler con el identificador especificado.
    /// </summary>
    public static Error NotFound = new Error(
        "Alquiler.Found",
        "El alquiler con el Id especificado no fue encontrado"
    );

    /// <summary>
    /// Error que ocurre cuando un alquiler se solapa con otro para el mismo vehículo y fechas.
    /// </summary>
    public static Error Overlap = new Error(
        "Alquiler.Overlap",
        "El alquiler para este auto está siendo tomado por más de una persona a la vez o en la misma fecha"
    );

    /// <summary>
    /// Error que ocurre cuando se intenta confirmar un alquiler que no está en estado reservado.
    /// </summary>
    public static Error NotReserved = new Error(
        "Alquiler.NotReserved",
        "El alquiler no está reservado"
    );

    /// <summary>
    /// Error que ocurre cuando se intenta completar o cancelar un alquiler que no está confirmado.
    /// </summary>
    public static Error NotConfirmed = new Error(
       "Alquiler.NotConfirmed",
       "El alquiler no está confirmado"
    );

    /// <summary>
    /// Error que ocurre cuando se intenta iniciar un alquiler que ya ha comenzado.
    /// </summary>
    public static Error AlreadyStarted = new Error(
        "Alquiler.AlreadyStarted",
        "El alquiler ya ha comenzado"
    );
}
