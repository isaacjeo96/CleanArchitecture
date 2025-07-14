using CleanArchitecture.Domain.Abstractions;  // Para usar Result<TResponse>
using MediatR;                                // Para usar IRequestHandler<TRequest, TResponse>

namespace CleanArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Contrato para los handlers de consultas en el patrón CQRS.
/// Define la firma que debe tener cualquier handler que procese un <see cref="IQuery{TResponse}"/>
/// y devuelva un <see cref="Result{TResponse}"/>.
/// </summary>
/// <typeparam name="TQuery">
/// Tipo de la consulta a manejar. Debe implementar <see cref="IQuery{TResponse}"/>.
/// </typeparam>
/// <typeparam name="TResponse">
/// Tipo de dato que devolverá la consulta (por ejemplo: DTO, lista, entidad).
/// </typeparam>
public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>> // Indica a MediatR que maneja TQuery y devuelve Result<TResponse>
    where TQuery : IQuery<TResponse>             // Restringe para que TQuery sea una IQuery<TResponse>
{
    // 📝 Esta interfaz no agrega nuevos métodos, simplemente
    // le da un nombre más explícito al hecho de que se trata
    // de un Handler para una Query, y estandariza la convención.
    //
    // MediatR sabrá encontrar e invocar la implementación correcta
    // de este handler cuando se envíe un objeto que implemente IQuery<TResponse>.
}
