using CleanArchitecture.Domain.Abstractions;  // Para usar Result<TResponse>
using MediatR;                                // Para usar IRequest<T>

// 📂 Namespace en la capa Application que agrupa contratos de Mensajería (Commands/Queries)
namespace CleanArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Contrato base para definir una consulta (query) en el patrón CQRS.
/// Una consulta representa una operación de solo lectura, que no modifica el estado del sistema,
/// y devuelve un resultado con datos de tipo <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">
/// Tipo de dato que devolverá la consulta (por ejemplo: DTO, lista, entidad).
/// </typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    // 📝 Esta interfaz no define ningún miembro adicional, simplemente hereda
    // de IRequest<Result<TResponse>> para estandarizar la forma en que las
    // consultas se envían a través de MediatR.
    //
    // MediatR identificará las consultas que implementen esta interfaz
    // y las enviará al handler correspondiente.
}
