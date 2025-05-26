namespace CleanArchitecture.Domain.Vehiculos
{
    /// <summary>
    /// Representa una cantidad de dinero como un objeto de valor, incluyendo su monto y tipo de moneda.
    /// Proporciona operaciones seguras y validadas para trabajar con valores monetarios dentro del dominio.
    /// </summary>
    /// <param name="Monto">Valor numérico de la cantidad monetaria.</param>
    /// <param name="TipoMoneda">Tipo de moneda asociado al monto (e.g., USD, PEN, etc.).</param>
    public record Moneda(decimal Monto, TipoMoneda TipoMoneda)
    {
        /// <summary>
        /// Suma dos valores monetarios del mismo tipo de moneda.
        /// </summary>
        /// <param name="primero">Primer valor monetario.</param>
        /// <param name="segundo">Segundo valor monetario.</param>
        /// <returns>Una nueva instancia de <see cref="Moneda"/> con la suma de ambos montos.</returns>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si los tipos de moneda son diferentes.
        /// </exception>
        public static Moneda operator +(Moneda primero, Moneda segundo)
        {
            if (primero.TipoMoneda != segundo.TipoMoneda)
            {
                throw new InvalidOperationException("El tipo de moneda debe ser el mismo");
            }

            return new Moneda(primero.Monto + segundo.Monto, primero.TipoMoneda);
        }

        /// <summary>
        /// Devuelve una instancia de <see cref="Moneda"/> con valor cero y tipo de moneda 'None'.
        /// </summary>
        public static Moneda Zero() => new(0, TipoMoneda.None);

        /// <summary>
        /// Devuelve una instancia de <see cref="Moneda"/> con valor cero para un tipo de moneda específico.
        /// </summary>
        /// <param name="tipoMoneda">Tipo de moneda que se desea usar.</param>
        public static Moneda Zero(TipoMoneda tipoMoneda) => new(0, tipoMoneda);

        /// <summary>
        /// Indica si el valor de la moneda es cero (0) para el tipo de moneda actual.
        /// </summary>
        public bool IsZero => this == Zero(TipoMoneda);
    }
}
