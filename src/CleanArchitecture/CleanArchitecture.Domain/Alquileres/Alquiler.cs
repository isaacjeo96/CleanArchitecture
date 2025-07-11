using System.Xml.XPath;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
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
    /// Constructor privado. Se debe utilizar un método de fábrica para crear instancias controladas del dominio.
    /// </summary>
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

    /// <summary>
    /// Método de fábrica para crear un nuevo alquiler en estado reservado.
    /// Calcula automáticamente los costos utilizando el <see cref="PrecioService"/> 
    /// y dispara un evento de dominio que indica que el alquiler ha sido reservado.
    /// Además, actualiza la fecha del último alquiler en el vehículo.
    /// </summary>
    /// <param name="vehiculo">Vehículo que será alquilado.</param>
    /// <param name="userId">Identificador del usuario que realiza la reserva.</param>
    /// <param name="duracion">Periodo de duración del alquiler.</param>
    /// <param name="fechaCreacion">Fecha de creación de la reserva.</param>
    /// <param name="precioService">Servicio del dominio para calcular los precios.</param>
    /// <returns>Una nueva instancia de <see cref="Alquiler"/> en estado reservado.</returns>
    public static Alquiler Reservar(
        Vehiculo vehiculo,
        Guid userId,
        DateRange duracion,
        DateTime fechaCreacion,
        PrecioService precioService)
    {
        // Calcula el precio detallado del alquiler utilizando el servicio de dominio.
        var precioDetalle = precioService.CalcularPrecio(vehiculo, duracion);

        // Crea una nueva instancia del alquiler.
        var alquiler = new Alquiler(
            Guid.NewGuid(),
            vehiculo.Id,
            userId,
            duracion,
            precioDetalle.PreicioPorPeriodo,
            precioDetalle.Mantenimiento,
            precioDetalle.Accesorios,
            precioDetalle.PrecioTotal,
            AlquilerStatus.Reservado,
            fechaCreacion);

        // Dispara evento de dominio para que otras partes del sistema reaccionen.
        alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.Id!));

        // Actualiza la fecha del último alquiler del vehículo.
        vehiculo.FechaUltimaAlquiler = fechaCreacion;

        return alquiler;
    }

    /// <summary>
    /// Confirma un alquiler previamente reservado, cambiando su estado a <see cref="AlquilerStatus.Confirmado"/>.
    /// Solo se puede confirmar si el alquiler está actualmente en estado <see cref="AlquilerStatus.Reservado"/>.
    /// Si el estado no es válido, retorna un error.
    /// </summary>
    /// <param name="utcNow">
    /// Fecha y hora actual en formato UTC, usada para registrar el momento de la confirmación.
    /// </param>
    /// <returns>
    /// Un resultado (<see cref="Result"/>) que indica si la operación fue exitosa.
    /// Retorna <c>Result.Success()</c> si la confirmación es válida,
    /// o un <c>Result.Failure()</c> con <see cref="AlquilerErrors.NotReserved"/> si no es posible confirmar.
    /// </returns>

    public Result Confirmar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);
        }

        Status = AlquilerStatus.Confirmado;
        FechaConfirmacion = utcNow;


        RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(Id));
        return Result.Success();
    }

    /// <summary>
    /// Rechaza un alquiler previamente reservado, cambiando su estado a <see cref="AlquilerStatus.Rechazado"/>.
    /// Solo se puede rechazar si el alquiler está actualmente en estado <see cref="AlquilerStatus.Reservado"/>.
    /// Si el estado no es válido, retorna un error.
    /// </summary>
    /// <param name="utcNow">
    /// Fecha y hora actual en formato UTC, usada para registrar el momento del rechazo.
    /// </param>
    /// <returns>
    /// Un resultado (<see cref="Result"/>) que indica si la operación fue exitosa.
    /// Retorna <c>Result.Success()</c> si el rechazo es válido,
    /// o un <c>Result.Failure()</c> con <see cref="AlquilerErrors.NotReserved"/> si no es posible rechazar.
    /// </returns>

    public Result Rechazar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);

        }

        Status = AlquilerStatus.Rechazado;
        FechaDeNegacion = utcNow;
        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(Id));

        return Result.Success();

    }

    /// <summary>
    /// Cancela un alquiler previamente confirmado, cambiando su estado a <see cref="AlquilerStatus.Cancelado"/>.
    /// Solo se puede cancelar si el alquiler está actualmente en estado <see cref="AlquilerStatus.Confirmado"/>
    /// y la fecha actual es anterior al inicio del periodo de alquiler.
    /// Si las condiciones no se cumplen, retorna un error.
    /// </summary>
    /// <param name="utcNow">
    /// Fecha y hora actual en formato UTC, usada para registrar el momento de la cancelación.
    /// </param>
    /// <returns>
    /// Un resultado (<see cref="Result"/>) que indica si la operación fue exitosa.
    /// Retorna <c>Result.Success()</c> si la cancelación es válida,
    /// o un <c>Result.Failure()</c> con <see cref="AlquilerErrors.NotConfirmed"/> o <see cref="AlquilerErrors.AlreadyStarted"/> si no es posible cancelar.
    /// </returns>

    public Result Cancelar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmed);

        }

        var currentDate = DateOnly.FromDateTime(utcNow);

        if (currentDate > Duracion!.Inicio)
        {
            return Result.Failure(AlquilerErrors.AlreadyStarted);
        }

        Status = AlquilerStatus.Cancelado;
        FechaCancelacion = utcNow;
        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(Id));

        return Result.Success();

    }

    /// <summary>
    /// Completa un alquiler previamente confirmado, cambiando su estado a <see cref="AlquilerStatus.Completado"/>.
    /// Solo se puede completar si el alquiler está actualmente en estado <see cref="AlquilerStatus.Confirmado"/>.
    /// Si el estado no es válido, retorna un error.
    /// </summary>
    /// <param name="utcNow">
    /// Fecha y hora actual en formato UTC, usada para registrar el momento de la finalización del alquiler.
    /// </param>
    /// <returns>
    /// Un resultado (<see cref="Result"/>) que indica si la operación fue exitosa.
    /// Retorna <c>Result.Success()</c> si la finalización es válida,
    /// o un <c>Result.Failure()</c> con <see cref="AlquilerErrors.NotConfirmed"/> si no es posible completar.
    /// </returns>

    public Result Completar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmed);

        }

        Status = AlquilerStatus.Completado;
        FechaCompletado = utcNow;
        RaiseDomainEvent(new AlquilerCompletadoDomainEvent(Id));

        return Result.Success();

    }
}
