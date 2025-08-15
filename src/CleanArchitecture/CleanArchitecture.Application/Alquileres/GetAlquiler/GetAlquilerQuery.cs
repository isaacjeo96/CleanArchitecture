using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    /// <summary>
    /// Representa una consulta para obtener la información de un alquiler específico por su identificador.
    /// </summary>
    /// <param name="AlquilerId">Identificador único del alquiler a consultar.</param>
    public sealed record GetAlquiler(Guid AlquilerId) : IQuery<AlquilerResponse>;
}
