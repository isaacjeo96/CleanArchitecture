using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Servicio de dominio responsable de calcular el precio total de un alquiler de vehículo,
/// tomando en cuenta la duración, el precio base, los accesorios y posibles cargos adicionales como mantenimiento.
/// </summary>
public class PrecioService
{
    /// <summary>
    /// Calcula el detalle completo del precio de un alquiler.
    /// </summary>
    /// <param name="vehiculo">El vehículo a alquilar, con sus precios y accesorios definidos.</param>
    /// <param name="periodo">El rango de fechas del alquiler.</param>
    /// <returns>Un objeto <see cref="PrecioDetalle"/> que contiene el desglose completo del precio.</returns>
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
    {
        var tipoMoneda = vehiculo.Precio!.TipoMoneda;

        // Precio base por la duración total del alquiler.
        var precioPorPeriodo = new Moneda(periodo.CantidadDias * vehiculo.Precio.Monto, tipoMoneda);

        // Acumulador para calcular el porcentaje extra por accesorios.
        decimal porcentangeChange = 0;

        // Recorre los accesorios y suma el porcentaje extra según el tipo.
        foreach (var accesorio in vehiculo.Accesorios)
        {
            porcentangeChange += accesorio switch
            {
                Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.01m,
                _ => 0
            };
        }

        // Calcula el cargo adicional por accesorios si aplica.
        var accesorioCharges = Moneda.Zero(tipoMoneda);

        if (porcentangeChange > 0)
        {
            accesorioCharges = new Moneda(precioPorPeriodo.Monto * porcentangeChange, tipoMoneda);
        }

        // Inicializa el precio total.
        var precioTotal = Moneda.Zero();

        // Suma el precio base.
        precioTotal += precioPorPeriodo;

        // Si hay mantenimiento, lo suma al total.
        if (!vehiculo!.Mantenimiento!.IsZero)
        {
            precioTotal += vehiculo.Mantenimiento;
        }

        // Suma los cargos por accesorios.
        precioTotal += accesorioCharges;

        // Devuelve el detalle completo del precio.
        return new PrecioDetalle(
            precioPorPeriodo,
            vehiculo.Mantenimiento,
            accesorioCharges,
            precioTotal
        );
    }
}
