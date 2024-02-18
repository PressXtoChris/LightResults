# Result&lt;TValue&gt;.Fail method (1 of 6)

Creates a failed result.

```csharp
public static Result Fail()
```

## Return Value

A new instance of [`Result`](../Result-1.md) representing a failed result.

## See Also

* class [Result&lt;TValue&gt;](../Result-1.md)
* namespace [LightResults](../../LightResults.md)

---

# Result&lt;TValue&gt;.Fail method (2 of 6)

Creates a failed result with the given errors.

```csharp
public static Result Fail(IEnumerable<IError> errors)
```

| parameter | description |
| --- | --- |
| errors | A collection of errors associated with the failure. |

## Return Value

A new instance of [`Result`](../Result-1.md) representing a failed result with the specified errors.

## See Also

* interface [IError](../IError.md)
* class [Result&lt;TValue&gt;](../Result-1.md)
* namespace [LightResults](../../LightResults.md)

---

# Result&lt;TValue&gt;.Fail method (3 of 6)

Creates a failed result with the given error.

```csharp
public static Result Fail(IError error)
```

| parameter | description |
| --- | --- |
| error | The error associated with the failure. |

## Return Value

A new instance of [`Result`](../Result-1.md) representing a failed result with the specified error.

## See Also

* interface [IError](../IError.md)
* class [Result&lt;TValue&gt;](../Result-1.md)
* namespace [LightResults](../../LightResults.md)

---

# Result&lt;TValue&gt;.Fail method (4 of 6)

Creates a failed result with the given error message.

```csharp
public static Result Fail(string errorMessage)
```

| parameter | description |
| --- | --- |
| errorMessage | The error message associated with the failure. |

## Return Value

A new instance of [`Result`](../Result-1.md) representing a failed result with the specified error message.

## See Also

* class [Result&lt;TValue&gt;](../Result-1.md)
* namespace [LightResults](../../LightResults.md)

---

# Result&lt;TValue&gt;.Fail method (5 of 6)

Creates a failed result with the given error message and metadata.

```csharp
public static Result Fail(string errorMessage, (string Key, object Value) metadata)
```

| parameter | description |
| --- | --- |
| errorMessage | The error message associated with the failure. |
| metadata | The metadata associated with the failure. |

## Return Value

A new instance of [`Result`](../Result-1.md) representing a failed result with the specified error message.

## See Also

* class [Result&lt;TValue&gt;](../Result-1.md)
* namespace [LightResults](../../LightResults.md)

---

# Result&lt;TValue&gt;.Fail method (6 of 6)

Creates a failed result with the given error message and metadata.

```csharp
public static Result Fail(string errorMessage, IDictionary<string, object> metadata)
```

| parameter | description |
| --- | --- |
| errorMessage | The error message associated with the failure. |
| metadata | The metadata associated with the failure. |

## Return Value

A new instance of [`Result`](../Result-1.md) representing a failed result with the specified error message.

## See Also

* class [Result&lt;TValue&gt;](../Result-1.md)
* namespace [LightResults](../../LightResults.md)

<!-- DO NOT EDIT: generated by xmldocmd for LightResults.dll -->