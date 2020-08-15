using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Data
{
    public class GameBoard
    {
        private const int WIN_LENGTH = 4; // 0-3
        private int Columns { get; set; }
        private int Rows { get; set; }
        private int[][] Board { get; set; }
        private int FreeSpaces { get; set; }

        public GameBoard(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
            FreeSpaces = rows * columns;
            Board = new int[rows][];
            for (int i = 0; i < columns; i++)
                Board[i] = new int[columns];
        }

        private void Print()
        {
            Debug.Write("   ");
            for (int i = 0; i < Columns; i++)
                Debug.Write($" {i}");
            Debug.WriteLine(string.Empty);

            Debug.Write("   ");
            for (int i = 0; i < Columns; i++)
                Debug.Write($" -");
            Debug.WriteLine(string.Empty);

            for (int i = 0; i < Board.Length; i++)
            {
                Debug.Write($"{i} |");
                for (int j = 0; j < Board[i].Length; j++)
                    Debug.Write($" {Board[i][j]}");

                Debug.WriteLine(string.Empty);
            }
            Debug.WriteLine($"Remaining Spaces: {FreeSpaces}");
        }

        public int GetColumns()
        {
            return Columns;
        }

        public DropStatus MakeMove(int playerToken, int column)
        {
            if (Board[0][column] != 0) return DropStatus.COLUMN_FULL;   // COLUMN FULL

            var rowIndex = Rows - 1;
            while (Board[rowIndex][column] != 0)
                rowIndex--;

            Board[rowIndex][column] = playerToken;
            FreeSpaces--;

            Print();    // DEBUG

            if (CheckVerticalWin(rowIndex, column, playerToken) ||
                CheckHorizontalWin(rowIndex, column, playerToken) ||
                CheckDiagonalWin(rowIndex, column, playerToken) ||
                CheckAntiDiagonalWin(rowIndex, column, playerToken))
            {
                return DropStatus.WIN;
            }

            if (FreeSpaces == 0) return DropStatus.DRAW;   // no win detected. no more room.

            return DropStatus.PLACED;
        }

        // direction: |
        private bool CheckVerticalWin(int row, int column, int playerToken)
        {
            var row0 = row;
            var row1 = row;
            while (row0 > 0 && Board[row0 - 1][column] == playerToken) row0--;  // check up
            while (row1 < Board.Length - 1 && Board[row1 + 1][column] == playerToken) row1++; // check down

            return (row1 - row0 + 1 == WIN_LENGTH);
        }
        
        // direction: --
        private bool CheckHorizontalWin(int row, int column, int playerToken)
        {
            var col0 = column;
            var col1 = column;
            while (col0 > 0 && Board[row][col0 - 1] == playerToken) col0--;  // check left
            while (col1 < Board.Length - 1 && Board[row][col1 + 1] == playerToken) col1++; // check right
            
            return (col1 - col0 + 1 == WIN_LENGTH);
        }

        // direction: /
        private bool CheckDiagonalWin(int row, int column, int playerToken)
        {
            var col0 = column;
            var row0 = row;
            var col1 = column;
            var row1 = row;
            while (col0 > 0 && row0 < Board.Length - 1 && Board[row0 + 1][col0 - 1] == playerToken) { col0--; row0++; }  // down-left
            while (row1 > 0 && col1 < Board[row1].Length - 1 && Board[row1 - 1][col1 + 1] == playerToken) { col1++; row1--; }  // up-right

            return (col1 - col0 + 1 == WIN_LENGTH);  // i can only move diagonal. so if im able to get to x. that means the path is good.
        }

        // direction: \
        private bool CheckAntiDiagonalWin(int row, int column, int playerToken)
        {
            var col0 = column;
            var row0 = row;
            var col1 = column;
            var row1 = row;
            while (row0 < Board.Length - 1 && col0 < Board[row0].Length - 1 && Board[row0 + 1][col0 + 1] == playerToken) { col0++; row0++; }  // down-right
            while (row1 > 0 && col1 > 0 && Board[row1 - 1][col1 - 1] == playerToken) { col1--; row1--; }  // up-left
            
            return (col1 - col0 + 1 == WIN_LENGTH);  // i can only move diagonal. so if im able to get to x. that means the path is good.
        }
    }
}
