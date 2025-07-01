using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Representa una operación de alquiler de un vehículo en el sistema.
/// Contiene información clave como su estado actual.
/// </summary>
public sealed class Alquiler : Entity
{
    /// <summary>
    /// Crea una nueva instancia de un alquiler con el identificador proporcionado.
    /// </summary>
    /// <param name="id">Identificador único del alquiler.</param>
    public Alquiler(Guid id) : base(id)
    {
    }

    /// <summary>
    /// Estado
}