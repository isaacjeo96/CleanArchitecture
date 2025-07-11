using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

/// <summary>
/// Objeto de valor que representa la puntuación (raiting) otorgada en una reseña.
/// La puntuación es un número entero entre 1 y 5, validado al momento de su creación.
/// </summary>
public sealed record Raiting
{
    /// <summary>
    /// Error que indica que la puntuación proporcionada es inválida (fuera del rango permitido).
    /// </summary>
    public static readonly Error Invalid = new("Raiting.Invalid", "El raiting es inválido");

    /// <summary>
    /// Valor entero de la puntuación otorgada.
    /// </summary>
    public int Value { get; init; }

    /// <summary>
    /// Constructor privado. Se debe usar <see cref="Create(int)"/> para validar y crear instancias.
    /// </summary>
    /// <param name="value">Valor entero de la puntuación, previamente validado.</param>
    private Raiting(int value) => Value = value;

    /// <summary>
    /// Método de fábrica para crear una instancia de <see cref="Raiting"/>, validando que el valor esté dentro del rango permitido (1–5).
    /// </summary>
    /// <param name="value">Puntuación deseada.</param>
    /// <returns>
    /// Un <see cref="Result{T}"/> exitoso con la puntuación válida o un fallo con <see cref="Invalid"/> si el valor es inválido.
    /// </returns>
    public static Result<Raiting> Create(int value)
    {
        if (value < 1 || value > 5)
        {
            return Result.Failure<Raiting>(Invalid);
        }

        return new Raiting(value);
    }
}
