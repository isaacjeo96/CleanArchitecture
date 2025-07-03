namespace CleanArchitecture.Domain.Abstractions;

/// <summary>
/// Representa un error de dominio o de validación en la aplicación.
/// Un error contiene un código identificador y un nombre o mensaje descriptivo.
/// </summary>
/// <param name="Code">
/// Código único que identifica el tipo de error (por ejemplo, "Error.NullValue").
/// </param>
/// <param name="Name">
/// Mensaje descriptivo o nombre legible del error.
/// </param>
public record Error(string Code, string Name)
{
    /// <summary>
    /// Representa la ausencia de un error.
    /// Puede usarse como valor por defecto cuando no hay ningún error que reportar.
    /// </summary>
    public static Error None = new(string.Empty, string.Empty);

    /// <summary>
    /// Representa un error por haber recibido un valor nulo donde no se esperaba.
    /// </summary>
    public static Error NullValue = new("Error.NullValue", "Se ingresó un valor nulo");
}
