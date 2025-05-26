namespace CleanArchitecture.Domain.Users;

/// <summary>
/// Representa el apellido de una persona o usuario como un objeto de valor.
/// Encapsula una cadena de texto que identifica al usuario dentro del dominio.
/// </summary>
/// <param name="Value">Cadena de texto con el apellido del usuario.</param>

public record Apellido(string Value);
