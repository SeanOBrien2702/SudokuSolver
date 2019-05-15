using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            int safety = 0;
            int row = 0;
            int col = 0;
            

            Cell.SetColour(prevRow, prevCol, false);

            
            //Cell.checkPossibleNums(row, col);
            int curFilledCells = Board.filledCells;

            ////loop until game is done
            while (done == false && safety < 1000)
            {
                
                //check possible numbers of the current Cell
                for (row = 0; row < Board.kRowMax; ++row)
                {
                    for (col = 0; col < Board.kColMax; ++col)
                    {
                        if (Board.cell[row, col].GetAnswer() == 0)
                        {

                            checkPossibleNums(row, col);
                            if (Board.cell[row, col].FillCell(row, col) == true)
                            {
                                Board.filledCells++;
                            }

                            //Exit from the function when one step has been calculated
                            if (curFilledCells != Board.filledCells && step)
                            {
                                //Cell.SetColour(row, col, true);
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
                ++safety;
            }
            MessageBox.Show("Number of pass throughs: "+ safety);
        }


        /// <summary>
        /// Check the box, row and column of the current cell check for
        /// possible candidates
        /// </summary>
        /// <param name="curRow"></param>
        /// <param name="curCol"></param>
        public static void checkPossibleNums(int curRow, int curCol)
        {
            CheckRow(curRow, curCol);
            CheckCol(curRow, curCol);
            CheckBox(curRow, curCol);
        }


        /// <summary>
        /// determine if a candidate for the current cell can be found in 
        /// another cell in the column
        /// </summary>
        /// <param name="curRow"></param>
        /// <param name="curCol"></param>
        private static void CheckCol(int curRow, int curCol)
        {
            for (int i = 0; i < Board.kRowMax; ++i)
            {
                if (Board.cell[i, curCol].GetAnswer() != 0)
                    Board.cell[curRow, curCol].SetCanidate(Board.cell[i, curCol].GetAnswer(), true);
            }
        }



        /// <summary>
        /// determine if a candidate for the current cell can be found in 
        /// another cell in the row
        /// </summary>
        /// <param name="curRow"></param>
        /// <param name="curCol"></param>
        private static void CheckRow(int curRow, int curCol)
        {
            for (int i = 0; i < Board.kRowMax; ++i)
            {
                if (Board.cell[curRow, i].GetAnswer() != 0)
                    Board.cell[curRow, curCol].SetCanidate(Board.cell[curRow, i].GetAnswer(), true);
            }
        }


        /// <summary>
        /// determine if a candidate for the current cell can be found in 
        /// another cell in the box
        /// </summary>
        /// <param name="curRow"></param>
        /// <param name="curCol"></param>
        private static void CheckBox(int curRow, int curCol)
        {
            whichBox(curRow, curCol);
            for (int i = Board.boxLocation[0]; i < (Board.kBoxSize + Board.boxLocation[0]); ++i)
            {
                for (int j = Board.boxLocation[1]; j < (Board.kBoxSize + Board.boxLocation[1]); ++j)
                {
                    if (Board.cell[i, j].GetAnswer() != 0)
                        Board.cell[curRow, curCol].SetCanidate(Board.cell[i, j].GetAnswer(), true);
                }
            }
        }


        /// <summary>
        /// Determine which box contains the current cell 
        /// </summary>
        /// <param name="curRow">current row</param>
        /// <param name="curCol">current column</param>
        private static void whichBox(double curRow, double curCol)
        {
            Board.box[0] = curRow / 3;
            Board.box[1] = curCol / 3;
            for (int i = 0; i < 2; ++i)
            {
                if (Board.box[i] < 1)
                {
                    Board.boxLocation[i] = 0;
                }
                else if (Board.box[i] >= 1 && Board.box[i] < 2)
                {
                    Board.boxLocation[i] = 3;
                }
                else if (Board.box[i] >= 2)
                {
                    Board.boxLocation[i] = 6;
                }
            }
        }

    }
}
