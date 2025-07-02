using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Representa un objeto de valor que agrupa todos los componentes del costo de un alquiler de vehículo.
/// Contiene el precio base, los cargos por mantenimiento, accesorios y el precio total.
/// </summary>
/// <param name="PreicioPorPeriodo">Precio base calculado por la duración del alquiler.</param>
/// <param name="Mantenimiento">Costo adicional por mantenimiento del vehículo durante el alquiler.</param>
/// <param name="Accesorios">Cargos adicionales por los accesorios opcionales incluidos.</param>
/// <param name="PrecioTotal">Suma total de todos los conceptos del alquiler.</param>
public record PrecioDetalle(
    Moneda PreicioPorPeriodo,
    Moneda Mantenimiento,
    Moneda Accesorios,
    Moneda PrecioTotal
);
