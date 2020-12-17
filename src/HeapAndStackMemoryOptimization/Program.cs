using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeapAndStackMemoryOptimization
{
    class Program
    {
        private static IList<int> _listOfNumbers = new List<int>();
        private static IList<object> _listOfBoxedNumbers = new List<object>();
        private static IList<object> _listOfTestItemsAsObject = new List<object>();

        static void Main(string[] _)
        {
            for (var numberOfExecutionsOrItemsForEachCase = 10000; numberOfExecutionsOrItemsForEachCase <= 100000000; numberOfExecutionsOrItemsForEachCase *= 10)
            {
                Console.WriteLine($"Comparing with {numberOfExecutionsOrItemsForEachCase:n0} executions or items in a collection:");

                SetUpData(numberOfExecutionsOrItemsForEachCase);

                ExecuteAndTrackBoxingOperation(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackBoxingOperationInCollectionUsingSelectMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackBoxingOperationInCollectionUsingCastMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackBoxingOperationInCollectionUsingOfTypeMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackBoxingOperationInCollectionUsingCustomCastFromValueTypeToObjectMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackUnboxingOperation(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackUnboxingOperationInCollectionUsingSelectMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackUnboxingOperationInCollectionUsingCastMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackUnboxingOperationInCollectionUsingOfTypeMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackUnboxingOperationInCollectionUsingCustomCastFromObjectMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackVariableAssignmentForValueTypes(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackVariableAssignmentForReferenceTypes(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackCastingFromReferenceTypeToAnotherReferenceType(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingSelectMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCastMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingOfTypeMethod(numberOfExecutionsOrItemsForEachCase);
                ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCustomCastFromObjectMethod(numberOfExecutionsOrItemsForEachCase);

                Console.WriteLine("-----------END LINE-----------\n");
            }
        }

        private static void SetUpData(int numberOfItems)
        {
            _listOfNumbers = Enumerable.Repeat(1, numberOfItems).ToList();
            _listOfBoxedNumbers = Enumerable.Repeat((object)1, numberOfItems).ToList();
            _listOfTestItemsAsObject = Enumerable.Repeat((object)new TestItem(), numberOfItems).ToList();
        }

        private static long GetExecutionElapsedTimeForAction(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();

            // Force GC collection so there is no interference in each test
            GC.Collect();

            return stopwatch.ElapsedMilliseconds;
        }

        private static void ExecuteMultipleTimes(Action action, int numberOfExecutions)
        {
            for (var i = 0; i < numberOfExecutions; i++)
            {
                action();
            }
        }

        private static void OutputTimeElapsedToConsole(long totalMillisecondsElapsed,
            int numberOfExecutionsOrItems, [CallerMemberName] string callerName = "")
        {
            Console.WriteLine($"{callerName} - total time elapsed: {totalMillisecondsElapsed} ms, average time per operation/item: {(decimal)totalMillisecondsElapsed / numberOfExecutionsOrItems} ms");
        }

        private static void ExecuteAndTrackBoxingOperation(int numberOfExecutions)
        {
            const int number = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    object boxedNumber = number;
                }, numberOfExecutions);
            });
            
            OutputTimeElapsedToConsole(timeElapsed, numberOfExecutions);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingSelectMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = _listOfNumbers.Select(number => (object)number).ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingCastMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = _listOfNumbers.Cast<object>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingOfTypeMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = _listOfNumbers.OfType<object>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackBoxingOperationInCollectionUsingCustomCastFromValueTypeToObjectMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfBoxedNumbers = _listOfNumbers.CastFromValueTypeToObject().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackUnboxingOperation(int numberOfExecutions)
        {
            object obj = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    var unboxedNumber = (int)obj;
                }, numberOfExecutions);
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfExecutions);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingSelectMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = _listOfBoxedNumbers.Select(obj => (int)obj).ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingCastMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = _listOfBoxedNumbers.Cast<int>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingOfTypeMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = _listOfBoxedNumbers.OfType<int>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackUnboxingOperationInCollectionUsingCustomCastFromObjectMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfUnboxedObjects = _listOfBoxedNumbers.CastFromObject<int>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackVariableAssignmentForValueTypes(int numberOfExecutions)
        {
            var number = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    int number1 = number;
                }, numberOfExecutions);
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfExecutions);
        }

        private static void ExecuteAndTrackVariableAssignmentForReferenceTypes(int numberOfExecutions)
        {
            object number = 1;
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    object number1 = number;
                }, numberOfExecutions);
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfExecutions);
        }

        private static void ExecuteAndTrackCastingFromReferenceTypeToAnotherReferenceType(int numberOfExecutions)
        {
            var referenceType = new TestItem();
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                ExecuteMultipleTimes(() =>
                {
                    var obj = (object)referenceType;
                }, numberOfExecutions);
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfExecutions);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingSelectMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = _listOfTestItemsAsObject.Select(o => (TestItem)o).ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCastMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = _listOfTestItemsAsObject.Cast<TestItem>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingOfTypeMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = _listOfTestItemsAsObject.OfType<TestItem>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }

        private static void ExecuteAndTrackCastingFromObjectToOriginalReferenceTypeInCollectionUsingCustomCastFromObjectMethod(int numberOfItems)
        {
            var timeElapsed = GetExecutionElapsedTimeForAction(() =>
            {
                var listOfItems = _listOfTestItemsAsObject.CastFromObject<TestItem>().ToArray();
            });

            OutputTimeElapsedToConsole(timeElapsed, numberOfItems);
        }
    }

    class TestItem
    {

    }
}
