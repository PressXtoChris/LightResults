﻿using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using LightResults.Common;

namespace LightResults;

// ReSharper disable StaticMemberInGenericType
/// <summary>Represents a result.</summary>
/// <typeparam name="TValue">The type of the value in the result.</typeparam>
public readonly struct Result<TValue> :
#if NET7_0_OR_GREATER
    IActionableResult<TValue, Result<TValue>>, IEquatable<Result<TValue>>
#else
    IResult<TValue>, IEquatable<Result<TValue>>
#endif
{
    /// <inheritdoc />
    TValue IResult<TValue>.Value
    {
        get
        {
            if (!_isSuccess)
                throw new InvalidOperationException($"{nameof(Result)} is failed. {nameof(IResult<TValue>.Value)} is not set.");

            return _valueOrDefault!;
        }
    }

    /// <inheritdoc />
    public IReadOnlyCollection<IError> Errors => _errors ?? (_isSuccess ? Error.EmptyCollection : Error.DefaultCollection);

    /// <inheritdoc />
    IError IResult.Error
    {
        get
        {
            if (_isSuccess)
                throw new InvalidOperationException($"{nameof(Result)} is successful. {nameof(Error)} is not set.");

            return _errors is { Length: > 0 } ? _errors.Value[0] : Error.Empty;
        }
    }

    private static readonly Result<TValue> FailedResult = new(Error.Empty);
    private readonly bool _isSuccess = false;
    private readonly ImmutableArray<IError>? _errors;
    private readonly TValue? _valueOrDefault;

    /// <summary>Initializes a new instance of the <see cref="Result{TValue}" /> struct.</summary>
    public Result()
    {
    }

    private Result(TValue value)
    {
        _isSuccess = true;
        _valueOrDefault = value;
    }

    private Result(IError error)
    {
        _errors = ImmutableArray.Create(error);
    }

    private Result(IEnumerable<IError> errors)
    {
        _errors = errors.ToImmutableArray();
    }

    /// <inheritdoc />
    public bool IsSuccess()
    {
        return _isSuccess;
    }

    /// <inheritdoc />
    public bool IsSuccess([MaybeNullWhen(false)] out TValue value)
    {
        value = _valueOrDefault;
        return _isSuccess;
    }

    /// <inheritdoc />
    public bool IsFailed()
    {
        return !_isSuccess;
    }

    /// <inheritdoc />
    public bool IsFailed([MaybeNullWhen(false)] out IError error)
    {
        if (_isSuccess)
            error = null;
        else
            error = _errors is { Length: > 0 } ? _errors.Value[0] : Error.Empty;
        return !_isSuccess;
    }

#if NET7_0_OR_GREATER
    /// <summary>Creates a success result with the specified value.</summary>
    /// <param name="value">The value to include in the result.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a success result with the specified value.</returns>
    static Result<TValue> IActionableResult<TValue, Result<TValue>>.Ok(TValue value)
    {
        return Ok(value);
    }
#endif

    internal static Result<TValue> Ok(TValue value)
    {
        var result = new Result<TValue>(value);
        return result;
    }

#if NET7_0_OR_GREATER
    /// <summary>Creates a failed result.</summary>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result.</returns>
    static Result<TValue> IActionableResult<TValue, Result<TValue>>.Fail()
    {
        return Fail();
    }
#endif

    internal static Result<TValue> Fail()
    {
        return FailedResult;
    }

#if NET7_0_OR_GREATER
    /// <summary>Creates a failed result with the given error message.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message.</returns>
    static Result<TValue> IActionableResult<TValue, Result<TValue>>.Fail(string errorMessage)
    {
        return Fail(errorMessage);
    }
#endif

    internal static Result<TValue> Fail(string errorMessage)
    {
        var error = new Error(errorMessage);
        return Fail(error);
    }

#if NET7_0_OR_GREATER
    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message.</returns>
    static Result<TValue> IActionableResult<TValue, Result<TValue>>.Fail(string errorMessage, (string Key, object Value) metadata)
    {
        return Fail(errorMessage, metadata);
    }
#endif

    internal static Result<TValue> Fail(string errorMessage, (string Key, object Value) metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Fail(error);
    }

#if NET7_0_OR_GREATER
    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message.</returns>
    static Result<TValue> IActionableResult<TValue, Result<TValue>>.Fail(string errorMessage, IDictionary<string, object> metadata)
    {
        return Fail(errorMessage, metadata);
    }
#endif

    internal static Result<TValue> Fail(string errorMessage, IDictionary<string, object> metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Fail(error);
    }

#if NET7_0_OR_GREATER
    /// <summary>Creates a failed result with the given error.</summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error.</returns>
    static Result<TValue> IActionableResult<TValue, Result<TValue>>.Fail(IError error)
    {
        return Fail(error);
    }
#endif

    internal static Result<TValue> Fail(IError error)
    {
        return new Result<TValue>(error);
    }

#if NET7_0_OR_GREATER
    /// <summary>Creates a failed result with the given errors.</summary>
    /// <param name="errors">A collection of errors associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified errors.</returns>
    static Result<TValue> IActionableResult<TValue, Result<TValue>>.Fail(IEnumerable<IError> errors)
    {
        return Fail(errors);
    }
#endif

    internal static Result<TValue> Fail(IEnumerable<IError> errors)
    {
        return new Result<TValue>(errors);
    }

    /// <inheritdoc />
    public bool HasError<TError>()
        where TError : IError
    {
        if (_isSuccess)
            return false;

        if (_errors is null && typeof(TError) == typeof(Error))
            return true;

        if (_errors is null || _errors.Value.Length == 0)
            return false;

        // Do not convert to LINQ, this creates unnecessary heap allocations.
        // For is the most efficient way to loop. It is the fastest and does not allocate.
        // ReSharper disable once ForCanBeConvertedToForeach
        // ReSharper disable once LoopCanBeConvertedToQuery
        for (var index = 0; index < _errors.Value.Length; index++)
        {
            var error = _errors.Value[index];
            if (error is TError)
                return true;
        }

        return false;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (_isSuccess)
        {
            var valueString = StringHelper.GetResultValueString(_valueOrDefault);
            return StringHelper.GetResultString(nameof(Result), "True", valueString);
        }

        if (_errors is null || _errors.Value.Length == 0 || _errors.Value[0].Message.Length == 0)
            return $"{nameof(Result)} {{ IsSuccess = False }}";

        var errorString = StringHelper.GetResultErrorString(_errors.Value);
        return StringHelper.GetResultString(nameof(Result), "False", errorString);
    }

    /// <summary>Determines whether two <see cref="Result{TValue}" /> instances are equal.</summary>
    /// <param name="other">The <see cref="Result{TValue}" /> instance to compare with this instance.</param>
    /// <returns><c>true</c> if the specified <see cref="Result{TValue}" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public bool Equals(Result<TValue> other)
    {
        return Nullable.Equals(_errors, other._errors) && EqualityComparer<TValue?>.Default.Equals(_valueOrDefault, other._valueOrDefault);
    }

    /// <summary>Determines whether the specified object is equal to this instance.</summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the specified object is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Result<TValue> other && Equals(other);
    }

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(_errors, _valueOrDefault);
    }

    /// <summary>Determines whether two <see cref="Result{TValue}" /> instances are equal.</summary>
    /// <param name="left">The first <see cref="Result{TValue}" /> instance to compare.</param>
    /// <param name="right">The second <see cref="Result{TValue}" /> instance to compare.</param>
    /// <returns><c>true</c> if the specified <see cref="Result{TValue}" /> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Result<TValue> left, Result<TValue> right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two <see cref="Result{TValue}" /> instances are not equal.</summary>
    /// <param name="left">The first <see cref="Result{TValue}" /> instance to compare.</param>
    /// <param name="right">The second <see cref="Result{TValue}" /> instance to compare.</param>
    /// <returns><c>true</c> if the specified <see cref="Result{TValue}" /> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Result<TValue> left, Result<TValue> right)
    {
        return !left.Equals(right);
    }

    /// <summary>Implicitly converts a value to a success <see cref="Result{TValue}" />.</summary>
    /// <param name="value">The value to convert into a success result.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a success result with the specified value.</returns>
    [SuppressMessage("Usage", "CA2225: Operator overloads have named alternates", Justification = $"{nameof(Ok)} is the named alternate.")]
    public static implicit operator Result<TValue>(TValue value)
    {
        return Ok(value);
    }

    /// <summary>Converts the current <see cref="Result{TValue}" /> to a non-generic <see cref="Result" /> containing the same errors, if any.</summary>
    /// <returns>A new instance of <see cref="Result" /> representing the current result's errors, if any, or a successful result otherwise.</returns>
    /// <remarks>This method is useful for scenarios where a generic result needs to be converted into a non-generic result.</remarks>
    public Result ToResult()
    {
        return Result.Fail(Errors);
    }
}