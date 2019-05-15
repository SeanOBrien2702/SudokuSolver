using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudokuSolver
{
    public class Cell
    {
        private bool[] canidate = new bool[9];



        const int cNumSIze = 9;
        //private bool[] number;

        private int answer;
        private string name;
        public const int kRowMax = 9;
        public const int kColMax = 9;
        public const int kBoxHigh = 50;
        public const int kBoxWdth = 50;
        public const int kBoxSize = 3;
        public static int[] boxLocation = new int[2];
        public static double[] box = new double[2];
        

        //defult contructor
        public Cell()
        {



        }


        //contructor
        public Cell(string newName)
        {
            answer = 0;
            name = newName;
        }


        public void SetCanidate(int whichCanidate, bool result)
        {
            //TODO: the number are 1-9 but the bounderies for the array are 0-8
            canidate[whichCanidate - 1] = result;
        }

        public bool GetCanidate(int whichCanidate)
        {
            bool result = false;
            if(canidate[whichCanidate] == true)
            {
                result = true;
            }
            return result;
        }

        public int GetAnswer()
        {
            return answer;
        }


        public void SetAnswer(int result)
        {
            if (result != 0)
            {
                answer = result;
            }
        }


        public static void SetColour(int row, int col, bool newNum)
        {
            if (newNum)
            {
                Board.txtBox[row, col].ForeColor = Color.Red;
            }
            else
            {
                Board.txtBox[row, col].ForeColor = Color.Black;
            }
        }



        /// <summary>
        /// In each square, all possible numbers for the Cell is checked. 
        /// If there is only one possible number left, that number is filled in the table
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public bool FillCell(int row, int col)
        {
            bool filled = false;
            int tally = 0;
            int buffer = 0;
            for (int i = 0; i < 9; ++i)
            {
                if (GetCanidate(i) == false)
                {
                    buffer = i;
                }
                else
                {
                    tally += 1;
                }
            }
            if (tally >= 8)
            {
                SetAnswer(buffer + 1);
                //filledCells += 1;
                //Board.table[row, col, 0] = answer;
                Board.txtBox[row, col].Text = answer.ToString();
                filled = true;
            }
            return filled;
        }




    }



}
