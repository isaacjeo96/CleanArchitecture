using CleanArchitecture.Domain.Abstractions; // Importa la clase Error, que modela un error de dominio con código y mensaje

namespace CleanArchitecture.Domain.Users;

/// <summary>
/// Contenedor estático para todos los errores de dominio relacionados con <see cref="User"/>.
/// Centraliza y estandariza la definición de mensajes y códigos de error
/// para evitar duplicación y facilitar su mantenimiento.
/// </summary>
public static class UserErros
{
    /// <summary>
    /// Error que indica que no se encontró un usuario con el identificador proporcionado.
    /// 
    /// - Code: "User.Found" (⚠ debería ser "User.NotFound" para mantener consistencia con el significado real).
    /// - Message: "No existe el usuario buscado por este Id".
    /// </summary>
    public static Error NotFound = new(
        "User.Found", // Código identificador del error (recomendación: cambiar a "User.NotFound")
        "No existe el usuario buscado por este Id" // Mensaje claro en español
    );

    /// <summary>
    /// Error que indica que las credenciales proporcionadas son inválidas.
    /// 
    /// - Code: "User.InvalidCredentials".
    /// - Message: "El usuario o contraseña son incorrectos".
    /// </summary>
    public static Error InvalidCredentials = new(
        "User.InvalidCredentials", // Código identificador único del error
        "El usuario o contraseña son incorrectos" // Mensaje descriptivo en español
    );
}
