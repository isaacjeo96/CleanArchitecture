using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    /// <summary>
    /// Manejador para la consulta <see cref="GetAlquilerQuery"/> que recupera un alquiler desde la base de datos usando Dapper.
    /// </summary>
    internal sealed record GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="GetAlquilerQueryHandler"/>.
        /// </summary>
        /// <param name="sqlConnectionFactory">Fábrica de conexiones SQL utilizada para acceder a la base de datos.</param>
        public GetAlquilerQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        /// <summary>
        /// Maneja la consulta para obtener un alquiler por su identificador.
        /// </summary>
        /// <param name="request">Consulta que contiene el identificador del alquiler.</param>
        /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
        /// <returns>Una instancia de <see cref="Result{AlquilerResponse}"/> que contiene los datos del alquiler o null si no se encuentra.</returns>
        public async Task<Result<AlquilerResponse>> Handle(
            GetAlquilerQuery request,
            CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(
                sql, new
                {
                    request.AlquilerId
                });

            return alquiler;
        }
    }
}
