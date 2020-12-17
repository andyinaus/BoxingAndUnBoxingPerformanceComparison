# Boxing and UnBoxing Performance Comparison

## TL;DR

Apart from the fact that boxing is more expensice than unboxing as stated in [MSDN Doc](https://docs.microsoft.com/en-us/dotnet/framework/performance/performance-tips#boxing-and-unboxing), we also found that using [LINQ Select](https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs#L38) is way faster than [LINQ Cast](https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs#L1031) and [LINQ OfType](https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs#L1020) when doing explicit casting for each item in a collection.

## Comparing with 100,000,000 exections or items in a collection

|Case|Type|Total time elapsed|Average time per execution|
|----|----|-----------------:|-------------------------:|
|ExecuteAndTrackBoxingOperation|Multiple Executions|736 ms|0.00000736 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingSelectMethod|Single Execution For Each Item|6266 ms|0.00006266 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingCastMethod|Single Execution For Each Item|13401 ms|0.00013401 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingOfTypeMethod|Single Execution For Each Item|14133 ms|0.00014133 ms|
|ExecuteAndTrackBoxingOperationInCollectionUsingMyOwnCastMethod|Single Execution For Each Item|8965 ms|0.00008965 ms|
|ExecuteAndTrackUnboxingOperation|Multiple Executions|460 ms|0.0000046 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingSelectMethod|Single Execution For Each Item|657 ms|0.00000657 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingCastMethod|Single Execution For Each Item|2817 ms|0.00002817 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingOfTypeMethod|Single Execution For Each Item|2876 ms|0.00002876 ms|
|ExecuteAndTrackUnboxingOperationInCollectionUsingMyOwnCastMethod|Single Execution For Each Item|3255 ms|0.00003255 ms|
|ExecuteAndTrackVariableAssignmentForValueTypes|Multiple Executions|356 ms|0.00000356 ms|
|ExecuteAndTrackVariableAssignmentForReferenceTypes|Multiple Executions|342 ms|0.00000342 ms|
|ExecuteAndTrackCastingFromReferenceTypeToAnotherReferenceType|Multiple Executions|327 ms|0.00000327 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingSelectMethod|Single Execution For Each Item|971 ms|0.00000971 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCastMethod|Single Execution For Each Item|4020 ms|0.0000402 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingOfTypeMethod|Single Execution For Each Item|4625 ms|0.00004625 ms|
|ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingMyOwnCastMethod|Single Execution For Each Item|3921 ms|0.00003921 ms|

For comparison with less number of execution, please refer to the below screenshot(s).

![Comparison - 1](https://user-images.githubusercontent.com/12688884/102488954-8685ae00-40b8-11eb-802e-d307a12bd4f7.PNG)
![Comparison - 2](https://user-images.githubusercontent.com/12688884/102488959-884f7180-40b8-11eb-88f1-752492163a79.PNG)