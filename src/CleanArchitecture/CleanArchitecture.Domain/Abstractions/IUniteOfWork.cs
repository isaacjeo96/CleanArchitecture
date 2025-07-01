namespace CleanArchitecture.Domain.Abstractions;

/// <summary>
/// Unidad de trabajo que coordina la escritura de cambios en la base de datos.
/// Se encarga de confirmar o descartar transacciones.
/// </summary>
public interface IUniteOfWork
{
    /// <summary>
    /// Guarda todos los cambios pendientes en la base de datos.
    /// Úsalo después de agregar, actualizar o eliminar entidades.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación para interrumpir la operación si es necesario.</param>
    /// <returns>El número de registros afectados.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
