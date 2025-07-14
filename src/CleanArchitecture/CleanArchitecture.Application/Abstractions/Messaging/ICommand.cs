using CleanArchitecture.Domain.Abstractions;  // Para usar Result y Result<T>
using MediatR;                                // Para usar IRequest

namespace CleanArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Contrato base para definir un comando (command) en el patrón CQRS.
/// Un comando representa una operación que **modifica el estado del sistema**
/// y devuelve un <see cref="Result"/> indicando si fue exitosa.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand
{
    // 📝 Hereda de IRequest<Result> para integrarse con MediatR,
    // lo que permite enviar el comando a través de _mediator.Send(...)
    // También implementa la interfaz vacía IBaseCommand para
    // dar consistencia entre los contratos.
}

/// <summary>
/// Contrato base para definir un comando (command) en el patrón CQRS
/// que devuelve un valor adicional además del resultado de éxito/error.
/// </summary>
/// <typeparam name="TResponse">
/// Tipo de dato que devuelve el comando si es exitoso.
/// </typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
    // 📝 Igual que ICommand simple, pero para cuando el comando
    // también devuelve datos al completarse.
}

/// <summary>
/// Marcador para todos los comandos de la aplicación.
/// No contiene miembros por sí misma, pero permite tratar
/// todos los comandos de manera uniforme.
/// </summary>
public interface IBaseCommand
{
    // 📝 Esta interfaz vacía actúa como "marcador",
    // útil si en algún momento quieres hacer validaciones,
    // filtros o procesamiento adicional a todos los comandos
    // sin importar si son con o sin respuesta.
}
