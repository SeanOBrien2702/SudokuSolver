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
        const int cNumSIze = 9;
        //private bool[] number;

        private int answer;


        //defult contructor
        public Cell()
        {
            

            answer = 0;
        }


        //contructor
        public Cell(int result)
        {
            answer = result;
        }

        public bool GetOption(string option)
        {
            bool result = false;
            switch(option)
            {
                case "one":
                    
                    break;
                case "two":

                    break;
                case "three":

                    break;
                case "four":

                    break;
                case "five":

                    break;
                case "six":

                    break;
                case "seven":

                    break;
                case "eight":

                    break;
                case "nine":

                    break;

            }
            return result;
        }
        public bool SetOption(string option)
        {
            bool result = false;
            switch (option)
            {
                case "one":

                    break;
                case "two":

                    break;
                case "three":

                    break;
                case "four":

                    break;
                case "five":

                    break;
                case "six":

                    break;
                case "seven":

                    break;
                case "eight":

                    break;
                case "nine":

                    break;

            }
            return result;
        }

        public int GetAnswer()
        {
            return answer;
        }


        void SetAnswer(int result)
        {
            if (result == 0)
            {
                
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
    }



}
