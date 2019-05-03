using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Logic
    {

        public static int prevRow = 0;
        public static int prevCol = 0;

        /// <summary>
        /// Main logic to solve the sudoku puzzle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Solve(bool step)
        {
            //(row, col, possible numbers)
            bool done = false;
            int row = 0;
            int col = 0;


            Cell.SetColour(prevRow, prevCol, false);

            //write the values of the board to the table
            Board.ReadTextboxs();
            
            Board.checkPossibleNums(row, col);
            int curFilledCells = Board.filledCells;

            ////loop until game is done
            while (done == false)
            {
                
                //check possible numbers of the current square
                for (row = 0; row < Board.kRowMax; ++row)
                {
                    for (col = 0; col < Board.kColMax; ++col)
                    {
                        if (Board.table[row, col, 0] == 0)
                        {
                            Board.checkPossibleNums(row, col);
                            Board.FillSquare(row, col);
                            if (curFilledCells < Board.filledCells && step)
                            {
                                Cell.SetColour(row, col, true);
                                prevRow = row;
                                prevCol = col;
                                done = true;
                                break;
                            }
                        }
                    }
                    if (done == true)
                        break;
                }
                
                //fill in answer if its the only possible number
                if (Board.filledCells >= 81)
                {
                     
                    done = true;
                }
            }
        }
    }
}
