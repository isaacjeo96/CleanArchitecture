namespace CleanArchitecture.Application.Abstractions.Email;

/// <summary>
/// Contrato para un servicio de envío de correos electrónicos.
/// Forma parte de la capa de aplicación y define cómo se debe realizar el envío,
/// sin depender de un proveedor o implementación específica (SMTP, SendGrid, etc.).
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Envía un correo electrónico de forma asíncrona.
    /// </summary>
    /// <param name="recipient">
    /// Dirección de correo del destinatario, representada como un objeto de valor 
    /// <see cref="Domain.Users.Email"/> para asegurar que sea válida.
    /// </param>
    /// <param name="subject">
    /// Asunto del correo.
    /// </param>
    /// <param name="body">
    /// Contenido del mensaje (puede ser texto plano o HTML según la implementación).
    /// </param>
    /// <returns>
    /// Una tarea que representa la operación asíncrona de envío.
    /// </returns>
    Task SendAsync(Domain.Users.Email recipient, string subject, string body);
}
