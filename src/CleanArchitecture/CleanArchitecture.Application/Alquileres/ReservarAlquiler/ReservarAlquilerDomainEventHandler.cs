using CleanArchitecture.Application.Abstractions.Email; // Contrato para enviar correos electrónicos
using CleanArchitecture.Domain.Alquileres;              // Entidades y repositorios de Alquiler
using CleanArchitecture.Domain.Alquileres.Events;       // Eventos de dominio relacionados con alquileres
using CleanArchitecture.Domain.Users;                   // Entidades y repositorios de Usuarios
using MediatR;                                           // Librería para implementar el patrón Mediator (eventos, comandos, etc.)

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

/// <summary>
/// Manejador de eventos de dominio para el evento <see cref="AlquilerReservadoDomainEvent"/>.
/// 
/// Este handler se ejecuta automáticamente cuando se dispara el evento de "alquiler reservado"
/// en el dominio, con el objetivo de:
/// 1) Consultar el alquiler y el usuario relacionados.
/// 2) Enviar un correo de notificación al usuario para que confirme su reserva.
/// 
/// Implementa <see cref="INotificationHandler{TNotification}"/> de MediatR, lo que significa
/// que es un manejador de eventos que no devuelve resultado (solo efectos secundarios).
/// </summary>
internal sealed class ReservarAlquilerDomainEventHandler
    : INotificationHandler<AlquilerReservadoDomainEvent>
{
    // Dependencias necesarias
    private readonly IAlquilerRepository _alquilerRepository; // Acceso a datos de alquileres
    private readonly IUserRepository _userRepository;         // Acceso a datos de usuarios
    private readonly IEmailService _emailService;              // Servicio para enviar correos

    /// <summary>
    /// Constructor que recibe las dependencias vía inyección de dependencias.
    /// </summary>
    public ReservarAlquilerDomainEventHandler(
        IAlquilerRepository alquilerRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _alquilerRepository = alquilerRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    /// <summary>
    /// Método que maneja el evento de "Alquiler Reservado".
    /// </summary>
    /// <param name="notification">
    /// Instancia del evento de dominio que contiene los datos relevantes (en este caso, el Id del alquiler).
    /// </param>
    /// <param name="cancellationToken">
    /// Token opcional para cancelar la operación asíncrona.
    /// </param>
    public async Task Handle(
        AlquilerReservadoDomainEvent notification,
        CancellationToken cancellationToken)
    {
        // 1) Buscar el alquiler por su Id usando el repositorio
        var alquiler = await _alquilerRepository.GetByIdAsync(
            notification.AlquilerId,
            cancellationToken);

        // Si no se encuentra, termina sin hacer nada
        if (alquiler is null)
        {
            return;
        }

        // 2) Buscar el usuario relacionado con el alquiler
        var user = await _userRepository.GetByIdAsync(
            alquiler.UserId,
            cancellationToken
        );

        // Si no se encuentra el usuario, termina sin hacer nada
        if (user is null)
        {
            return;
        }

        // 3) Enviar correo electrónico al usuario
        await _emailService.SendAsync(
            user.Email,
            "Alquiler Reservado", // Asunto
            "Tienes que confirmar esta reserva, de lo contrario se va a perder."); // Cuerpo
    }
}
