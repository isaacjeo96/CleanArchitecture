namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Representa un rango de fechas con una fecha de inicio y una de fin.
/// Se utiliza, por ejemplo, para definir la duración de un alquiler de vehículo.
/// </summary>
public sealed record DateRange
{
    /// <summary>
    /// Constructor privado para forzar el uso del método Create.
    /// </summary>
    private DateRange()
    {
    }

    /// <summary>
    /// Fecha de inicio del rango.
    /// </summary>
    public DateOnly Inicio { get; init; }

    /// <summary>
    /// Fecha de fin del rango.
    /// </summary>
    public DateOnly Fin { get; init; }

    /// <summary>
    /// Cantidad total de días entre la fecha de inicio y fin.
    /// </summary>
    public int CantidadDias => Fin.DayNumber - Inicio.DayNumber;

    /// <summary>
    /// Crea una instancia válida de un rango de fechas.
    /// </summary>
    /// <param name="inicio">Fecha de inicio.</param>
    /// <param name="fin">Fecha de fin.</param>
    /// <returns>Una nueva instancia de DateRange.</returns>
    /// <exception cref="ApplicationException">
    /// Se lanza si la fecha de inicio es posterior a la fecha de fin.
    /// </exception>
    public static DateRange Create(DateOnly inicio, DateOnly fin)
    {
        if (inicio > fin)
        {
            throw new ApplicationException("La fecha final es anterior a la fecha de inicio");
        }

        return new DateRange
        {
            Inicio = inicio,
            Fin = fin
        };
    }
}
