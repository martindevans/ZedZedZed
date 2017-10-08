using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Z3;
using ZedZedZed.Extensions;

namespace ZedZedZed
{
    public class Sudoku
        : IDisposable
    {
        private readonly Context _ctx;

        // sudoku instance, we use '0' for empty cells
        private readonly int[,] _problem = {
            {0,0,0,0,9,4,0,3,0},
            {0,0,0,5,1,0,0,0,7},
            {0,8,9,0,0,0,0,4,0},
            {0,0,0,0,0,0,2,0,8},
            {0,6,0,2,0,1,0,5,0},
            {1,0,2,0,0,0,0,0,0},
            {0,7,0,0,0,0,5,2,0},
            {9,0,0,0,6,5,0,0,0},
            {0,4,0,9,7,0,0,0,0}
        };

        public Sudoku([NotNull] Context ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public void ExpressionRun()
        {
            var solver = _ctx.MkSolver();

            //Create cells
            var cells = new IntExpr[9, 9];
            for (var i = 0; i < cells.GetLength(0); i++)
            for (var j = 0; j < cells.GetLength(1); j++)
                cells[i, j] = _ctx.MkConstInt($"[{i},{j}]");

            //Constrain each cell to the valid range
            foreach (var cell in cells)
                solver.Assert(_ctx.MkConstraint(cell, c => c > 0 & c < 10));

            //Constrain cells to the problem
            for (var i = 0; i < _problem.GetLength(0); i++)
            for (var j = 0; j < _problem.GetLength(1); j++)
            {
                var known = _problem[i, j];
                if (known != 0)
                    solver.Assert(_ctx.MkConstraint(cells[i, j], known, (c, p) => c == p));
            }

            //Constrain column/row cells to be distinct
            for (var i = 0; i < cells.GetLength(0); i++)
                solver.Assert(_ctx.MkDistinct(Enumerable.Range(0, cells.GetLength(1)).Select(j => cells[i, j]).ToArray()));
            for (var j = 0; j < cells.GetLength(1); j++)
                solver.Assert(_ctx.MkDistinct(Enumerable.Range(0, cells.GetLength(0)).Select(i => cells[i, j]).ToArray()));
        }

        /// <summary>
        /// Sudoku solving example.
        /// </summary>
        public void BasicRun()
        {
            Console.WriteLine("SudokuExample");

            // 9x9 matrix of integer variables
            var X = new IntExpr[9][];
            for (uint i = 0; i < 9; i++)
            {
                X[i] = new IntExpr[9];
                for (uint j = 0; j < 9; j++)
                    X[i][j] = (IntExpr)_ctx.MkConst(_ctx.MkSymbol("x_" + (i + 1) + "_" + (j + 1)), _ctx.IntSort);
            }

            // each cell contains a value in {1, ..., 9}
            var cells_c = new Expr[9][];
            for (uint i = 0; i < 9; i++)
            {
                cells_c[i] = new BoolExpr[9];
                for (uint j = 0; j < 9; j++)
                    cells_c[i][j] = _ctx.MkAnd(_ctx.MkLe(_ctx.MkInt(1), X[i][j]),
                                              _ctx.MkLe(X[i][j], _ctx.MkInt(9)));
            }

            // each row contains a digit at most once
            var rows_c = new BoolExpr[9];
            for (uint i = 0; i < 9; i++)
                rows_c[i] = _ctx.MkDistinct(X[i]);

            // each column contains a digit at most once
            var cols_c = new BoolExpr[9];
            for (uint j = 0; j < 9; j++)
            {
                var column = new IntExpr[9];
                for (uint i = 0; i < 9; i++)
                    column[i] = X[i][j];

                cols_c[j] = _ctx.MkDistinct(column);
            }

            // each 3x3 square contains a digit at most once
            var sq_c = new BoolExpr[3][];
            for (uint i0 = 0; i0 < 3; i0++)
            {
                sq_c[i0] = new BoolExpr[3];
                for (uint j0 = 0; j0 < 3; j0++)
                {
                    var square = new IntExpr[9];
                    for (uint i = 0; i < 3; i++)
                        for (uint j = 0; j < 3; j++)
                            square[3 * i + j] = X[3 * i0 + i][3 * j0 + j];
                    sq_c[i0][j0] = _ctx.MkDistinct(square);
                }
            }

            var sudoku_c = _ctx.MkTrue();
            foreach (BoolExpr[] t in cells_c)
                sudoku_c = _ctx.MkAnd(_ctx.MkAnd(t), sudoku_c);
            sudoku_c = _ctx.MkAnd(_ctx.MkAnd(rows_c), sudoku_c);
            sudoku_c = _ctx.MkAnd(_ctx.MkAnd(cols_c), sudoku_c);
            foreach (var t in sq_c)
                sudoku_c = _ctx.MkAnd(_ctx.MkAnd(t), sudoku_c);

            

            var instance_c = _ctx.MkTrue();
            for (uint i = 0; i < 9; i++)
                for (uint j = 0; j < 9; j++)
                    instance_c = _ctx.MkAnd(instance_c,
                        (BoolExpr)
                        _ctx.MkITE(_ctx.MkEq(_ctx.MkInt(_problem[i, j]), _ctx.MkInt(0)),
                                    _ctx.MkTrue(),
                                    _ctx.MkEq(X[i][j], _ctx.MkInt(_problem[i, j]))));

            var s = _ctx.MkSolver();
            s.Assert(sudoku_c);
            s.Assert(instance_c);

            if (s.Check() == Status.SATISFIABLE)
            {
                var m = s.Model;
                var R = new Expr[9, 9];
                for (uint i = 0; i < 9; i++)
                    for (uint j = 0; j < 9; j++)
                        R[i, j] = m.Evaluate(X[i][j]);
                Console.WriteLine("Sudoku solution:");
                for (uint i = 0; i < 9; i++)
                {
                    for (uint j = 0; j < 9; j++)
                        Console.Write(" " + R[i, j]);
                    Console.WriteLine();
                }
            }
            else
            {
                throw new Exception("Failed to solve");
            }
        }

        public void Dispose()
        {
            _ctx?.Dispose();
        }
    }
}
