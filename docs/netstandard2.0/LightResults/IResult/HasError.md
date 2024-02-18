# IResult.HasError&lt;TError&gt; method

Checks if the result contains an error of the specific type.

```csharp
public bool HasError<TError>()
    where TError : IError
```

| parameter | description |
| --- | --- |
| TError | The type of error to check for. |

## Return Value

`true` if an error of the specified type is present; otherwise, `false`.

## See Also

* interface [IError](../IError.md)
* interface [IResult](../IResult.md)
* namespace [LightResults](../../LightResults.md)

<!-- DO NOT EDIT: generated by xmldocmd for LightResults.dll -->