# IActionableResult&lt;TValue,TResult&gt; interface

Defines an actionable result.

```csharp
public interface IActionableResult<TValue, out TResult> : IResult<TValue>
    where TResult : IActionableResult
```

## Members

| name | description |
| --- | --- |
| static [Fail](IActionableResult-2/Fail.md)() | Creates a failed result. |
| static [Fail](IActionableResult-2/Fail.md)(…) | Creates a failed result with the given error message. (5 methods) |
| static [Ok](IActionableResult-2/Ok.md)(…) | Creates a success result with the given value. |

## See Also

* interface [IResult&lt;TValue&gt;](./IResult-1.md)
* namespace [LightResults](../LightResults.md)

<!-- DO NOT EDIT: generated by xmldocmd for LightResults.dll -->