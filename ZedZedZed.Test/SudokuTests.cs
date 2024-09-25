using System.Text;
using Microsoft.Z3;
using ZedZedZed.Extensions;

namespace ZedZedZed.Test
{
    public class Sudoku
    {
        private readonly int[,] _problem;

        public Sudoku(int[,] problem)
        {
            if (problem.Rank != 2) throw new ArgumentException("Problem is not rank 2");
            if (problem.GetLength(0) != 9) throw new ArgumentException("Problem is not 9 wide");
            if (problem.GetLength(1) != 9) throw new ArgumentException("Problem is not 9 tall");
            _problem = problem;
        }

        public IntExpr[,]? Solve(Context context)
        {
            using (var solver = context.MkSolver())
            {
                //Create cells
                var cells = new IntExpr[9, 9];
                for (var i = 0; i < cells.GetLength(0); i++)
                for (var j = 0; j < cells.GetLength(1); j++)
                    cells[i, j] = context.MkConstInt($"[{i},{j}]");

                //Set up the constraints which apply to all sudoku problems (i.e. the rules of the game)
                ConstrainRules(context, solver, cells);

                //Set up the constraints specific to this problem
                ConstrainProblem(context, solver, cells);

                return ExtractSolution(solver, cells);
            }
        }

        private void ConstrainProblem(Context context, Solver solver, IntExpr[,] cells)
        {
            //Constrain each cell to the value in the problem (if we know it)
            for (var i = 0; i < 9; i++)
            for (var j = 0; j < 9; j++)
            {
                var cc = cells[i, j];
                var vv = _problem[i, j];
                if (vv > 0)
                    solver.Assert(context.MkConstraint(cc, vv, (c, v) => c == v));
            }
        }

        private void ConstrainRules(Context context, Solver solver, IntExpr[,] cells)
        {
            IEnumerable<IntExpr> Get3X3(int x, int y)
            {
                for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    yield return cells[x + i, y + j];
            }

            IEnumerable<IntExpr> GetRow(int y)
            {
                for (var i = 0; i < 9; i++)
                    yield return cells[i, y];
            }

            IEnumerable<IntExpr> GetCol(int x)
            {
                for (var i = 0; i < 9; i++)
                    yield return cells[x, i];
            }

            //Constrain each cell to the valid range
            foreach (var cell in cells)
                solver.Assert(context.MkConstraint(cell, c => c > 0 & c < 10));

            //Constrain cells in a 3x3 groups to be unique
            // ReSharper disable once CoVariantArrayConversion
            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                solver.Assert(context.MkDistinct(Get3X3(i * 3, j * 3).ToArray()));

            //Constrain cells in row to be unique
            // ReSharper disable once CoVariantArrayConversion
            for (var i = 0; i < 9; i++)
                solver.Assert(context.MkDistinct(GetRow(i).ToArray()));

            //Constrain cells in column to be unique
            // ReSharper disable once CoVariantArrayConversion
            for (var i = 0; i < 9; i++)
                solver.Assert(context.MkDistinct(GetCol(i).ToArray()));
        }

        private static IntExpr[,]? ExtractSolution(Solver solver, IntExpr[,] cells)
        {
            if (solver.Check() == Status.UNSATISFIABLE)
                return null;

            var solution = new IntExpr[9, 9];
            for (var i = 0; i < 9; i++)
                for (var j = 0; j < 9; j++)
                    solution[i, j] = (IntExpr)solver.Model.Eval(cells[i, j]);

            return solution;
        }
    }

    public class HexaSudoku
    {
        private readonly int[,] _problem;

        public HexaSudoku(int[,] problem)
        {
            if (problem.Rank != 2) throw new ArgumentException("Problem is not rank 2");
            if (problem.GetLength(0) != 16) throw new ArgumentException("Problem is not 16 wide");
            if (problem.GetLength(1) != 16) throw new ArgumentException("Problem is not 16 tell");
            _problem = problem;
        }

        public IntExpr[,]? Solve(Context context)
        {
            using (var solver = context.MkSolver())
            {
                //Create cells
                var cells = new IntExpr[16, 16];
                for (var i = 0; i < cells.GetLength(0); i++)
                for (var j = 0; j < cells.GetLength(1); j++)
                    cells[i, j] = context.MkConstInt($"[{i},{j}]");

                //Set up the constraints which apply to all sudoku problems (i.e. the rules of the game)
                ConstrainRules(context, solver, cells);

                //Set up the constraints specific to this problem
                ConstrainProblem(context, solver, cells);

                return ExtractSolution(solver, cells);
            }
        }

        private void ConstrainProblem(Context context, Solver solver, IntExpr[,] cells)
        {
            //Constrain each cell to the value in the problem (if we know it)
            for (var i = 0; i < 16; i++)
            for (var j = 0; j < 16; j++)
            {
                var cc = cells[i, j];
                var vv = _problem[i, j];
                if (vv >= 0)
                    solver.Assert(context.MkConstraint(cc, vv, (c, v) => c == v));
            }
        }

        private void ConstrainRules(Context context, Solver solver, IntExpr[,] cells)
        {
            IEnumerable<IntExpr> Get4X4(int x, int y)
            {
                for (var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                    yield return cells[x + i, y + j];
            }

            IEnumerable<IntExpr> GetRow(int y)
            {
                for (var i = 0; i < 16; i++)
                    yield return cells[i, y];
            }

            IEnumerable<IntExpr> GetCol(int x)
            {
                for (var i = 0; i < 16; i++)
                    yield return cells[x, i];
            }

            //Constrain each cell to the valid range
            foreach (var cell in cells)
                solver.Assert(context.MkConstraint(cell, c => c >= 0 & c < 16));

            //Constrain cells in a 4x4 groups to be unique
            // ReSharper disable once CoVariantArrayConversion
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    solver.Assert(context.MkDistinct(Get4X4(i * 4, j * 4).ToArray()));

            //Constrain cells in row to be unique
            // ReSharper disable once CoVariantArrayConversion
            for (var i = 0; i < 16; i++)
                solver.Assert(context.MkDistinct(GetRow(i).ToArray()));

            //Constrain cells in column to be unique
            // ReSharper disable once CoVariantArrayConversion
            for (var i = 0; i < 16; i++)
                solver.Assert(context.MkDistinct(GetCol(i).ToArray()));
        }

        private static IntExpr[,]? ExtractSolution(Solver solver, IntExpr[,] cells)
        {
            if (solver.Check() == Status.UNSATISFIABLE)
                return null;

            var solution = new IntExpr[16, 16];
            for (var i = 0; i < 16; i++)
                for (var j = 0; j < 16; j++)
                    solution[i, j] = (IntExpr)solver.Model.Eval(cells[i, j]);

            return solution;
        }
    }

    [TestClass]
    public class SudokuTests
    {
        private static void PrintSudoku(IntExpr[,]? solution)
        {
            ArgumentNullException.ThrowIfNull(solution, nameof(solution));

            for (var i = 0; i < 9; i++)
            {
                var line = new StringBuilder();

                line.Append(solution[i, 0]);
                line.Append(solution[i, 1]);
                line.Append(solution[i, 2]);

                line.Append('|');

                line.Append(solution[i, 3]);
                line.Append(solution[i, 4]);
                line.Append(solution[i, 5]);

                line.Append('|');

                line.Append(solution[i, 6]);
                line.Append(solution[i, 7]);
                line.Append(solution[i, 8]);

                line.Replace('0', '.');

                Console.WriteLine(line);

                if (i % 3 == 2 && i != 8)
                    Console.WriteLine("---+---+---");
            }
        }

        private static void PrintSudoku4x4(IntExpr[,] solution)
        {
            for (var i = 0; i < 16; i++)
            {
                var line = new StringBuilder();

                for (var j = 0; j < 4; j++)
                {
                    line.Append(solution[i, j * 4 + 0]);
                    line.Append(',');
                    line.Append(solution[i, j * 4 + 1]);
                    line.Append(',');
                    line.Append(solution[i, j * 4 + 2]);
                    line.Append(',');
                    line.Append(solution[i, j * 4 + 3]);
                    line.Append('|');
                }

                Console.WriteLine(line);

                if (i % 3 == 2 && i != 8)
                    Console.WriteLine("---+---+---");
            }
        }

        [TestMethod]
        public void Solve_Sudoku()
        {
            using (var ctx = new Context())
            {
                var _ = 0;
                var sudoku = new Sudoku(new[,] {
                        {_, _, _, _, 9, 4, _, 3, _},
                        {_, _, _, 5, 1, _, _, _, 7},
                        {_, 8, 9, _, _, _, _, 4, _},
                        {_, _, _, _, _, _, 2, _, 8},
                        {_, 6, _, 2, _, 1, _, 5, _},
                        {1, _, 2, _, _, _, _, _, _},
                        {_, 7, _, _, _, _, 5, 2, _},
                        {9, _, _, _, 6, 5, _, _, _},
                        {_, 4, _, 9, 7, _, _, _, _}
                    }
                );

                var solution = sudoku.Solve(ctx);

                PrintSudoku(solution);
            }
        }

        [TestMethod]
        public void Solve_Sudoku_Fiendish()
        {
            using (var ctx = new Context())
            {
                var _ = 0;
                var sudoku = new Sudoku(new[,] {
                        {3, _, 8, _, 4, _, _, _, 9},
                        {_, 1, _, _, 2, _, _, 3, _},
                        {_, _, 4, _, _, 7, 2, _, _},
                        {_, _, _, _, _, 5, 8, _, _},
                        {2, 8, _, _, 7, _, _, 9, 1},
                        {_, _, 7, 2, _, _, _, _, _},
                        {_, _, 3, 7, _, _, 6, _, _},
                        {_, 6, _, _, 3, _, _, 8, _},
                        {1, _, _, _, 5, _, 9, _, 3}
                    }
                );

                var solution = sudoku.Solve(ctx);

                PrintSudoku(solution);
            }
        }

        [TestMethod]
        public void Solve_Sudoku_4x4()
        {
            using (var ctx = new Context())
            {
                //A=10
                //B=11
                //C=12
                //D=13
                //E=14
                //F=15

                var _ = -1;
                var sudoku = new HexaSudoku(new[,] {
                        {_, 10, _, _,   5, _, _, _,   _, 15, _, 7,  13, 9, 2, 6},
                        {13, _, 1, _,   6, 9, _, _,   14, 5, _, 3,  _, _, _, 12},
                        {_, 2, 7, _,   13, _, 12, _,   _, _, _, _,  14, _, _, 10},
                        {_, _, _, 8,   14, _, _, 4,   _, 13, _, _,  3, 5, _, 7},

                        {10, 7, 4, 11,   _, _, 13, _,   _, 2, _, _,  _, _, 5, 1},
                        {_, 1, _, _,   _, _, _, 3,   _, 14, 7, _,  _, _, _, _},
                        {_, _, 8, _,   7, _, _, 1,   _, _, 13, 12,  4, _, 6, 14},
                        {_, _, _, 15,   _, 6, 10, _,   _, _, _, _,  _, _, 7, _},

                        {_, 11, _, _,   _, _, _, _,   _, 0, 9, _,  15, _, _, _},
                        {3, 13, _, 14,   10, 11, _, _,   1, _, _, 6,  _, 7, _, _},
                        {_, _, _, _,   _, 14, 1, _,   3, _, _, _,  _, _, 9, _},
                        {8, 9, _, _,   _, _, 4, _,   _, 12, _, _,  1, 13, 14, 3},

                        {15, _, 2, 13,   _, _, 3, _,   8, _, _, 0,  6, _, _, _},
                        {6, _, _, 9,   _, _, _, _,   _, 3, _, 10,  _, 1, 4, _},
                        {5, _, _, _,   2, _, 6, 7,   _, _, 14, 11,  _, 15, _, 0},
                        {7, 0, 3, 10,   4, _, 5, _,   _, _, _, 2,  _, _, 13, _},
                    }
                );

                var solution = sudoku.Solve(ctx);
                Assert.IsNotNull(solution);

                PrintSudoku4x4(solution);
            }
        }
    }
}
