# Boxing and UnBoxing Performance Comparison

## TL;DR

Apart from the fact that boxing is more expensice than unboxing as stated in [MSDN Doc](https://docs.microsoft.com/en-us/dotnet/framework/performance/performance-tips#boxing-and-unboxing), we also found that using [LINQ Select](https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs#L38) is way faster than [LINQ Cast](https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs#L1031) and [LINQ OfType](https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs#L1020) when doing explicit casting for each item in a collection.

## Comparing with 100,000,000 exections or items in a collection

|Case|Type|Total time elapsed|Average time per execution|
|----|----|-----------------:|-------------------------:|
|ExecuteAndTrackBoxingOperation|Multiple Executions|185 ms|0.00000185 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingSelectMethod|Single Execution For Each Item|6055 ms|0.00006055 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingCastMethod|Single Execution For Each Item|13103 ms|0.00013103 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingOfTypeMethod|Single Execution For Each Item|13589 ms|0.00013589 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingCustomCastFromValueTypeToObjectMethod|Single Execution For Each Item|7184 ms|0.00007184 ms|
|ExecuteAndTrackUnboxingOperation|Multiple Executions|246 ms|0.00000246 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingSelectMethod|Single Execution For Each Item|469 ms|0.00000469 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingCastMethod|Single Execution For Each Item|2528 ms|0.00002528 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingOfTypeMethod|Single Execution For Each Item|3024 ms|0.00003024 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingCustomCastFromObjectMethod|Single Execution For Each Item|1789 ms|0.00001789 ms|
|ExecuteAndTrackVariableAssignmentForValueTypes|Multiple Executions|176 ms|0.00000176 ms|
|ExecuteAndTrackVariableAssignmentForReferenceTypes|Multiple Executions|180 ms|0.00000180 ms|
|ExecuteAndTrackCastingFromReferenceTypeToAnotherReferenceType|Multiple Executions|207 ms|0.00000207 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingSelectMethod|Single Execution For Each Item|834 ms|0.00000834 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCastMethod|Single Execution For Each Item|3924 ms|0.00003924 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingOfTypeMethod|Single Execution For Each Item|4495 ms|0.00004495 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCustomCastFromObjectMethod|Single Execution For Each Item|2611 ms|0.00002611 ms|

For comparison with less number of execution, please refer to the below screenshot(s).

![Comparison - 1](https://user-images.githubusercontent.com/12688884/102574183-e7a29580-413b-11eb-9b7d-cf3a3c2c024f.png)
![Comparison - 2](https://user-images.githubusercontent.com/12688884/102574209-f7ba7500-413b-11eb-9b47-7d9638fbf778.png)