using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeapAndStackMemoryOptimization
{
    class Program
    {
        private static int _numberOfExecutionsOrItemsForEachCase;

        static void Main(string[] _)
        {
            for (_numberOfExecutionsOrItemsForEachCase = 10000; _numberOfExecutionsOrItemsForEachCase <= 100000000; _numberOfExecutionsOrItemsForEachCase *= 10)
            {
                Console.WriteLine($"Comparing with {_numberOfExecutionsOrItemsForEachCase:n0} operations or items in a collection:");

                ExecuteAndTrackBoxingOperation();
                ExecuteAndTrackBoxingOperationInCollectionUsingSelectMethod();
                ExecuteAndTrackBoxingOperationInCollectionUsingCastMethod();
                ExecuteAndTrackBoxingOperationInCollectionUsingOfTypeMethod();
                ExecuteAndTrackBoxingOperationInCollectionUsingMyOwnCastMethod();
                ExecuteAndTrackUnboxingOperation();
                ExecuteAndTrackUnboxingOperationInCollectionUsingSelectMethod();
                ExecuteAndTrackUnboxingOperationInCollectionUsingCastMethod();
                ExecuteAndTrackUnboxingOperationInCollectionUsingOfTypeMethod();
                ExecuteAndTrackUnboxingOperationInCollectionUsingMyOwnCastMethod();
                ExecuteAndTrackVariableAssignmentForValueTypes();
                ExecuteAndTrackVariableAssignmentForReferenceTypes();
                ExecuteAndTrackCastingFromReferenceTypeToAnotherReferenceType();
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingSelectMethod();
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCastMethod();
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingOfTypeMethod();
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingMyOwnCastMethod();

                Console.WriteLine("-----------END LINE-----------\n");
            }
        }

        private static long GetExecutionElapsedTimeForAction(Action operation)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            operation();
            stopwatch.Stop();

            // Force GC collection so there is no interference in each test
            GC.Collect();

            return stopwatch.ElapsedMilliseconds;
        }

        private static void ExecuteMultipleTimes(Action action)
        {
            for (var i = 0; i < _numberOfExecutionsOrItemsForEachCase; i++)
            {
                action();
            }
        }

        private static void OutputTimeElapsedToConsoleForOperation(long totalMillisecondsElapsed,
            int numberOfOperationsOrItems, [CallerMemberName] string operationName = "")
        {
            Console.WriteLine($"{operationName} - total time elapsed: {totalMillisecondsElapsed} ms, average time per operation/item: {(decimal)totalMillisecondsElapsed / numberOfOperationsOrItems} ms");
        }

        private static void ExecuteAndTrackBoxingOperation()
        {
            const int number = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    object boxedNumber = number;
                });
            });
            
            OutputTimeElapsedToConsoleForOperation(timeElapsed, _numberOfExecutionsOrItemsForEachCase);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingSelectMethod()
        {
            var listOfNumbers = Enumerable.Repeat(1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = listOfNumbers.Select(number => (object)number).ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfNumbers.Length);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingCastMethod()
        {
            var listOfNumbers = Enumerable.Repeat(1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = listOfNumbers.Cast<object>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfNumbers.Length);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingOfTypeMethod()
        {
            var listOfNumbers = Enumerable.Repeat(1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = listOfNumbers.OfType<object>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfNumbers.Length);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingMyOwnCastMethod()
        {
            var listOfNumbers = Enumerable.Repeat(1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = listOfNumbers.CastFromValueTypeToObject().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfNumbers.Length);
        }

        private static void ExecuteAndTrackUnboxingOperation()
        {
            object obj = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    var unboxedNumber = (int)obj;
                });
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, _numberOfExecutionsOrItemsForEachCase);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingSelectMethod()
        {
            var listOfObjects = Enumerable.Repeat((object) 1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = listOfObjects.Select(obj => (int)obj).ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingCastMethod()
        {
            var listOfObjects = Enumerable.Repeat((object)1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = listOfObjects.Cast<int>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingOfTypeMethod()
        {
            var listOfObjects = Enumerable.Repeat((object)1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = listOfObjects.OfType<int>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingMyOwnCastMethod()
        {
            var listOfObjects = Enumerable.Repeat((object)1, _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = listOfObjects.CastFromObject<int>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }

        private static void ExecuteAndTrackVariableAssignmentForValueTypes()
        {
            var number = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    int number1 = number;
                });
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, _numberOfExecutionsOrItemsForEachCase);
        }

        private static void ExecuteAndTrackVariableAssignmentForReferenceTypes()
        {
            object number = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    object number1 = number;
                });
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, _numberOfExecutionsOrItemsForEachCase);
        }

        private static void ExecuteAndTrackCastingFromReferenceTypeToAnotherReferenceType()
        {
            var referenceType = new TestItem();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    var obj = (object)referenceType;
                });
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, _numberOfExecutionsOrItemsForEachCase);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingSelectMethod()
        {
            var listOfObjects = Enumerable.Repeat((object)new TestItem(), _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = listOfObjects.Select(o => (TestItem)o).ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCastMethod()
        {
            var listOfObjects = Enumerable.Repeat((object) new TestItem(), _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = listOfObjects.Cast<TestItem>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingOfTypeMethod()
        {
            var listOfObjects = Enumerable.Repeat((object)new TestItem(), _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = listOfObjects.OfType<TestItem>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingMyOwnCastMethod()
        {
            var listOfObjects = Enumerable.Repeat((object)new TestItem(), _numberOfExecutionsOrItemsForEachCase).ToArray();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = listOfObjects.CastFromObject<TestItem>().ToArray();
            });

            OutputTimeElapsedToConsoleForOperation(timeElapsed, listOfObjects.Length);
        }
    }

    public class TestItem
    {

    }
}
