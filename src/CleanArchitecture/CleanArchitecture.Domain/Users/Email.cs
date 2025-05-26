namespace CleanArchitecture.Domain.Users;

/// <summary>
/// Representa una dirección de correo electrónico como un objeto de valor.
/// Encapsula una cadena que debe tener un formato de email válido.
/// </summary>
/// <param name="Value">Cadena que contiene la dirección de correo electrónico.</param>
public record Email(string Values);