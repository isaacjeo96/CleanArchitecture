namespace CleanArchitecture.Domain.Reviews;

/// <summary>
/// Objeto de valor que representa el comentario textual asociado a una reseña.
/// Permite encapsular la semántica de un comentario dentro del modelo de dominio.
/// </summary>
/// <param name="value">
/// Texto del comentario proporcionado por el usuario.
/// </param>
public sealed record Comentario(string value);
