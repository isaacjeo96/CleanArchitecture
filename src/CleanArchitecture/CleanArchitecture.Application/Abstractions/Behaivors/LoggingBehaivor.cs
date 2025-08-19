using CleanArchitecture.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.Behaivors
{
    /// <summary>
    /// Comportamiento que intercepta la ejecución de comandos y registra información 
    /// sobre su inicio, éxito o errores durante la ejecución.
    /// Este comportamiento es un cross-cutting concern que se aplica automáticamente 
    /// a todos los <see cref="IBaseCommand"/> mediante el pipeline de MediatR.
    /// </summary>
    /// <typeparam name="TRequest">Tipo del comando que implementa <see cref="IBaseCommand"/>.</typeparam>
    /// <typeparam name="TResponse">Tipo de la respuesta que devuelve el comando.</typeparam>
    public class LoggingBehaivor<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="LoggingBehaivor{TRequest, TResponse}"/>.
        /// </summary>
        /// <param name="logger">Instancia de <see cref="ILogger"/> utilizada para registrar información.</param>
        public LoggingBehaivor(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Intercepta la ejecución del comando y registra logs antes y después de su ejecución.
        /// En caso de error, también registra la excepción capturada.
        /// </summary>
        /// <param name="request">Instancia del comando recibido.</param>
        /// <param name="next">Delegado que representa el siguiente paso en el pipeline.</param>
        /// <param name="cancellationToken">Token de cancelación de la operación asincrónica.</param>
        /// <returns>El resultado del comando tras su ejecución.</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var nameRequest = request.GetType().Name;

            try
            {
                _logger.LogInformation($"Ejecutando el command request: {nameRequest}");

                var result = await next();

                _logger.LogInformation($"El comando {nameRequest} se ejecutó exitosamente");

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"El comando {nameRequest} tuvo errores");
                throw;
            }
        }
    }
}
