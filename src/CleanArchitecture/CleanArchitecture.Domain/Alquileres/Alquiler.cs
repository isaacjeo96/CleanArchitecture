using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Representa un alquiler de vehículo en el sistema.
/// Contiene toda la información necesaria para gestionar el ciclo de vida de un alquiler,
/// incluyendo fechas clave, costos, estado y referencias a usuario y vehículo.
/// </summary>
public sealed class Alquiler : Entity
{
    /// <summary>
    /// Constructor privado. Se debe utilizar un método de fábrica para crear instancias.
    /// </summary>
    /// <param name="id">Identificador único del alquiler.</param>
    /// <param name="vehiculoId">ID del vehículo alquilado.</param>
    /// <param name="userId">ID del usuario que realiza el alquiler.</param>
    /// <param name="duracion">Rango de fechas que abarca el alquiler.</param>
    /// <param name="precioPorPeriodo">Precio base por cada periodo de tiempo alquilado.</param>
    /// <param name="mantenimiento">Costo adicional por mantenimiento del vehículo.</param>
    /// <param name="accesorios">Costo adicional por accesorios añadidos al vehículo.</param>
    /// <param name="precioTotal">Precio total del alquiler (suma de todos los conceptos).</param>
    /// <param name="status">Estado actual del alquiler.</param>
    /// <param name="fechaCreacion">Fecha en la que se creó el alquiler.</param>
    private Alquiler(
        Guid id,
        Guid vehiculoId,
        Guid userId,
        DateRange duracion,
        Moneda precioPorPeriodo,
        Moneda mantenimiento,
        Moneda accesorios,
        Moneda precioTotal,
        AlquilerStatus status,
        DateTime fechaCreacion
    ) : base(id)
    {
        VehiculoId = vehiculoId;
        UserId = userId;
        Duracion = duracion;
        PrecioPorPeriodo = precioPorPeriodo;
        Mantenimiento = mantenimiento;
        Accesorios = accesorios;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacion = fechaCreacion;
    }

    /// <summary>
    /// Identificador del vehículo relacionado con el alquiler.
    /// </summary>
    public Guid VehiculoId { get; private set; }

    /// <summary>
    /// Identificador del usuario que realiza el alquiler.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Precio base por cada periodo del alquiler (por ejemplo, por día).
    /// </summary>
    public Moneda? PrecioPorPeriodo { get; private set; }

    /// <summary>
    /// Costo adicional relacionado con el mantenimiento del vehículo durante el alquiler.
    /// </summary>
    public Moneda? Mantenimiento { get; private set; }

    /// <summary>
    /// Costo adicional por accesorios opcionales incluidos en el alquiler.
    /// </summary>
    public Moneda? Accesorios { get; private set; }

    /// <summary>
    /// Precio total del alquiler, incluyendo todos los costos adicionales.
    /// </summary>
    public Moneda? PrecioTotal { get; private set; }

    /// <summary>
    /// Estado actual del alquiler (reservado, confirmado, cancelado, etc.).
    /// </summary>
    public AlquilerStatus Status { get; private set; }

    /// <summary>
    /// Rango de fechas que cubre el periodo de alquiler.
    /// </summary>
    public DateRange? Duracion { get; private set; }

    /// <summary>
    /// Fecha de creación del alquiler.
    /// </summary>
    public DateTime? FechaCreacion { get; private set; }

    /// <summary>
    /// Fecha en la que el alquiler fue confirmado.
    /// </summary>
    public DateTime? FechaConfirmacion { get; private set; }

    /// <summary>
    /// Fecha en la que el alquiler fue rechazado o no aceptado.
    /// </summary>
    public DateTime? FechaDeNegacion { get; private set; }

    /// <summary>
    /// Fecha en la que el alquiler fue marcado como completado.
    /// </summary>
    public DateTime? FechaCompletado { get; private set; }

    /// <summary>
    /// Fecha en la que el alquiler fue cancelado.
    /// </summary>
    public DateTime? FechaCancelacion { get; private set; }
}
