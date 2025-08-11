namespace CleanArchitecture.Application.Abstractions.Clock
{
    /// <summary>
    /// Proporciona una abstracción para obtener la hora y fecha actuales.
    /// Esta interfaz es útil para aislar dependencias de tiempo del sistema,
    /// lo que facilita las pruebas unitarias y el mantenimiento del código.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Obtiene la fecha y hora actuales.
        /// </summary>
        /// <remarks>
        /// El valor devuelto puede ser en UTC o en hora local dependiendo
        /// de la implementación concreta.
        /// Al abstraer el tiempo, podemos simular distintas fechas y horas
        /// en pruebas sin depender del reloj real del sistema.
        /// </remarks>
        DateTime currenTime { get; }
    }
}
