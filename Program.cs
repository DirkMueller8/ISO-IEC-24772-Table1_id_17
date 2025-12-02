using System;

namespace Table1_id_17
{
    internal class Program
    {
        static int counter = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Example 1: short-circuit && (RHS NOT evaluated)");
            counter = 0;
            bool leftFalse = false;
            bool result1 = leftFalse && SideEffectIncrement("RHS with &&"); // RHS will NOT run
            Console.WriteLine($"Result1: {result1}, counter: {counter}");
            Console.WriteLine();

            Console.WriteLine("Example 2: non-short-circuit & (RHS evaluated)");
            counter = 0;
            bool result2 = leftFalse & SideEffectIncrement("RHS with &"); // RHS WILL run
            Console.WriteLine($"Result2: {result2}, counter: {counter}");
            Console.WriteLine();

            Console.WriteLine("Example 3: short-circuit || (RHS NOT evaluated)");
            counter = 0;
            bool leftTrue = true;
            bool result3 = leftTrue || SideEffectIncrement("RHS with ||"); // RHS will NOT run
            Console.WriteLine($"Result3: {result3}, counter: {counter}");
            Console.WriteLine();

            Console.WriteLine("Example 4: non-short-circuit | (RHS evaluated)");
            counter = 0;
            bool result4 = leftTrue | SideEffectIncrement("RHS with |"); // RHS WILL run
            Console.WriteLine($"Result4: {result4}, counter: {counter}");
            Console.WriteLine();

            Console.WriteLine("Safe alternative: perform side effects explicitly before combining");
            counter = 0;
            bool rhsForced = SideEffectIncrement("forced RHS");
            bool resultSafe = leftFalse && rhsForced; // rhsForced already executed
            Console.WriteLine($"ResultSafe: {resultSafe}, counter: {counter}");
        }

        static bool SideEffectIncrement(string name)
        {
            Console.WriteLine($"Invoking RHS function: {name}");
            counter++;
            return true;
        }
    }
}