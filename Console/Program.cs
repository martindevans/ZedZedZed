using System.Collections.Generic;
using Microsoft.Z3;
using ZedZedZed;

namespace Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var ctx = new Context(new Dictionary<string, string>() { { "model", "true" } }))
            using (var sudoku = new Sudoku(ctx))
                sudoku.BasicRun();

            using (var ctx = new Context(new Dictionary<string, string>() { { "model", "true" } }))
            using (var sudoku = new Sudoku(ctx))
                sudoku.ExpressionRun();

            System.Console.ReadLine();
        }
    }
}
