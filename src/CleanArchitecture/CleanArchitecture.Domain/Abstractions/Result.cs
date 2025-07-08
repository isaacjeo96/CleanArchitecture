using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.Domain.Abstractions;

/// <summary>
/// Representa el resultado de una operación, indicando si fue exitosa o fallida.
/// Incluye información sobre un posible error cuando la operación falla.
/// </summary>
public class Result
{
    /// <summary>
    /// Constructor protegido para crear una instancia de <see cref="Result"/>.
    /// Valida que un resultado exitoso no tenga error y que un fallo tenga un error.
    /// </summary>
    /// <param name="isSuccess">Indica si la operación fue exitosa.</param>
    /// <param name="error">Error asociado si la operación falló.</param>
    /// <exception cref="InvalidOperationException">
    /// Lanza excepción si la combinación de parámetros es inválida.
    /// </exception>
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("Un resultado exitoso no puede tener un error.");
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Un resultado fallido debe tener un error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Indica si la operación fue exitosa.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Indica si la operación falló.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Error asociado cuando la operación falla.
    /// </summary>
    public Error Error { get; set; }

    /// <summary>
    /// Crea un resultado exitoso sin valor adicional.
    /// </summary>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Crea un resultado fallido con el error especificado.
    /// </summary>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Crea un resultado exitoso con un valor asociado.
    /// </summary>
    public static Result<TValue> Success<TValue>(TValue value)
        => new(value, true, Error.None);

    /// <summary>
    /// Crea un resultado fallido con un error especificado para un tipo con valor.
    /// </summary>
    public static Result<TValue> Failure<TValue>(Error error)
        => new(default, false, error);

    /// <summary>
    /// Crea un resultado a partir de un valor, devolviendo éxito si no es null o fallo con error <see cref="Error.NullValue"/>.
    /// </summary>
    public static Result<TValue> Create<TValue>(TValue? value)
        => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}

/// <summary>
/// Representa el resultado de una operación que, además de indicar éxito o fallo,
/// puede devolver un valor de tipo <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">Tipo del valor devuelto si la operación es exitosa.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    /// Constructor protegido para crear una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>
    /// Valor devuelto cuando la operación es exitosa.
    /// Acceder a esta propiedad cuando el resultado es fallido lanza excepción.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Lanza excepción si el resultado no es exitoso.
    /// </exception>
    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("El resultado no es exitoso. No hay valor disponible.");

    /// <summary>
    /// Permite convertir implícitamente un valor de tipo <typeparamref name="TValue"/> en un resultado exitoso.
    /// </summary>
    public static implicit operator Result<TValue>(TValue value) => Create(value);
}
