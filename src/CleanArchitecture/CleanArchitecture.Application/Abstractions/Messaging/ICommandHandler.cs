using CleanArchitecture.Domain.Abstractions;  // Para usar Result y Result<T>
using MediatR;                                // Para IRequestHandler<TRequest, TResponse>

namespace CleanArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Contrato para los handlers que procesan comandos en el patrón CQRS.
/// Define la firma para manejar un <see cref="ICommand"/> y devolver un <see cref="Result"/>.
/// </summary>
/// <typeparam name="TCommand">
/// Tipo de comando que maneja este handler. Debe implementar <see cref="ICommand"/>.
/// </typeparam>
public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Result>      // Hereda de IRequestHandler para MediatR
    where TCommand : ICommand                // Restringe a que sea solo comandos válidos
{
    // 📝 No define nada nuevo, simplemente da un nombre claro y explícito
    // para los handlers de comandos que NO devuelven datos adicionales.
}

/// <summary>
/// Contrato para los handlers que procesan comandos en el patrón CQRS y devuelven datos adicionales.
/// Define la firma para manejar un <see cref="ICommand{TResponse}"/> y devolver un <see cref="Result{TResponse}"/>.
/// </summary>
/// <typeparam name="TCommand">
/// Tipo de comando que maneja este handler. Debe implementar <see cref="ICommand{TResponse}"/>.
/// </typeparam>
/// <typeparam name="TResponse">
/// Tipo de dato adicional que devuelve el comando si es exitoso.
/// </typeparam>
public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>      // MediatR + resultado con datos
    where TCommand : ICommand<TResponse>                // Restringe a comandos con respuesta
{
    // 📝 Similar al anterior, pero para comandos que también devuelven datos
    // junto con el resultado de la operación.
}
