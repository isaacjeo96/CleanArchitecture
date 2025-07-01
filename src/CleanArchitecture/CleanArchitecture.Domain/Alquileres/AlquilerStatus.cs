namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Define los posibles estados que puede tener un alquiler de vehículo.
/// Cada estado representa una etapa específica del proceso de alquiler.
/// </summary>
public enum AlquilerStatus
{
    /// <summary>
    /// El alquiler ha sido reservado pero aún no confirmado.
    /// </summary>
    Reservado = 1,

    /// <summary>
    /// El alquiler ha sido confirmado y está listo para ser completado.
    /// </summary>
    Confirmado = 2,

    /// <summary>
    /// La solicitud de alquiler fue rechazada (por algún motivo como falta de disponibilidad).
    /// </summary>
    Rechazado = 3,

    /// <summary>
    /// El alquiler fue cancelado por el cliente o el sistema.
    /// </summary>
    Cancelado = 4,

    /// <summary>
    /// El proceso de alquiler fue completado exitosamente.
    /// </summary>
    Completado = 5
}
