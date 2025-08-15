using System.Data;

namespace CleanArchitecture.Application.Abstractions.Data
{
    /// <summary>
    /// Define un contrato para una fábrica de conexiones SQL.
    /// Esta interfaz abstrae la creación de conexiones a la base de datos,
    /// permitiendo una implementación desacoplada y más fácil de probar.
    /// </summary>
    public interface ISqlConnectionFactory
    {
        /// <summary>
        /// Crea y devuelve una nueva instancia de <see cref="IDbConnection"/> con la configuración adecuada.
        /// </summary>
        /// <returns>Una conexión abierta o cerrada según la implementación.</returns>
        IDbConnection CreateConnection();
    }
}
