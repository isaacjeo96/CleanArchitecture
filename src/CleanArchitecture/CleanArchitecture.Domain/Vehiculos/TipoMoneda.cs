namespace CleanArchitecture.Domain.Vehiculos
{
    /// <summary>
    /// Representa un tipo de moneda como un objeto de valor controlado, similar a un enum extendido.
    /// Proporciona una lista fija de monedas válidas y validación para crear instancias.
    /// </summary>
    public record TipoMoneda
    {
        /// <summary>
        /// Tipo de moneda no especificado o nulo (valor por defecto).
        /// </summary>
        public static readonly TipoMoneda None = new("");

        /// <summary>
        /// Dólar estadounidense (USD).
        /// </summary>
        public static readonly TipoMoneda Usd = new("USD");

        /// <summary>
        /// Euro (EUR).
        /// </summary>
        public static readonly TipoMoneda Eur = new("EUR");

        /// <summary>
        /// Inicializa una nueva instancia del tipo <see cref="TipoMoneda"/> con el código especificado.
        /// Este constructor es privado para restringir la creación a tipos predefinidos.
        /// </summary>
        /// <param name="codigo">Código ISO de la moneda.</param>
        private TipoMoneda(string codigo) => Codigo = codigo;

        /// <summary>
        /// Código ISO de la moneda (por ejemplo, "USD", "EUR").
        /// </summary>
        public string? Codigo { get; init; }

        /// <summary>
        /// Colección de todos los tipos de moneda válidos del sistema.
        /// </summary>
        public static readonly IReadOnlyCollection<TipoMoneda> All = new[]
        {
            Usd,
            Eur
        };

        /// <summary>
        /// Devuelve una instancia de <see cref="TipoMoneda"/> correspondiente al código especificado.
        /// </summary>
        /// <param name="codigo">Código de la moneda a buscar.</param>
        /// <returns>Una instancia válida de <see cref="TipoMoneda"/>.</returns>
        /// <exception cref="ApplicationException">Se lanza si el código no corresponde a ninguna moneda válida.</exception>
        public static TipoMoneda FromCoidgo(string codigo)
        {
            return All.FirstOrDefault(c => c.Codigo == codigo) ??
                throw new ApplicationException("El tipo de moneda es invalido");
        }
    }
}
