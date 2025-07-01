namespace CleanArchitecture.Domain.Users;

/// <summary>
/// Contrato para acceder y manipular los datos de usuarios.
/// Define las operaciones básicas necesarias sobre la entidad Usuario.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Busca un usuario por su identificador único.
    /// </summary>
    /// <param name="id">ID del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>El usuario correspondiente o null si no existe.</returns>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Agrega un nuevo usuario al contexto en memoria.
    /// Importante: Aún no se guarda en la base de datos hasta llamar a SaveChanges.
    /// </summary>
    /// <param name="user">Entidad de usuario a agregar.</param>
    void Add(User user);
}
