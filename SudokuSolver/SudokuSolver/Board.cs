using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Board : Form
    {
        public static TextBox[] test = new TextBox[80];
        public static TextBox[,] txtBox = new TextBox[kRowMax, kColMax];
        List<string> testSpel = new List<string>();

        public static int[,,] table = new int[9, 9, 10];
        public static int[] boxLocation = new int[2];
        public static double[] box = new double[2];
        public const int kAwnser = 0;
        public const int kRowMax = 9;
        public const int kColMax = 9;
        public const int kBoxHigh = 50;
        public const int kBoxWdth = 50;
        public const int kBoxSize = 3;
        public const int BOARD_OFFSET = 50;
        public const int HEIGHT_OFFSET = 50;
        public static int filledCells = 0;

        //bool error = false;
        //private object tErrorCheck;

        /// <summary>
        /// Initializes the form and starts the error checking thread
        /// </summary>
        public Board()
        {
            InitializeComponent();
            AddTextBoxes(); //create board
            //Thread tErrorCheck = new Thread(errorCheck(txtBox));
            //tErrorCheck.Start();
            //tErrorCheck.Join();
        }


        /// <summary>
        /// Creates the matrix of boxes/cells
        /// </summary>
        void AddTextBoxes()
        {

            int x = 50;
            int y = 50;
            int boxCount = 0;
            int col;
            // Width
            for (int row = 0; row < kRowMax; row++)
            {
                // Height
                for (col = 0; col < kColMax; col++)
                {
                    txtBox[row, col] = new TextBox();

                    txtBox[row, col].Name = "txtBox" + boxCount;
                    txtBox[row, col].TextAlign = HorizontalAlignment.Center;
                    //txtBox[row, col].BackColor = Color.Transparent;
                    txtBox[row, col].MaxLength = 1;
                    txtBox[row, col].Font = new Font(txtBox[row, col].Font.FontFamily, 28);
                    txtBox[row, col].Size = new Size(50, 50);
                    txtBox[row, col].Location = new Point(x, y);
                    txtBox[row, col].KeyPress += new KeyPressEventHandler(txtBoxKeyDown);

                    this.Controls.Add(txtBox[row, col]);
                    txtBox[row, col].SendToBack();
                    boxCount += 1;
                    if (col == 2 || col == 5)
                    {
                        x += 5;
                    }
                    x += kBoxWdth;
                }
                x = 50;
                col = 0;
                y += kBoxHigh;
                if (row == 2 || row == 5)
                {
                    y += 5;
                }
            }
        }
        /// <summary>
        /// Once the form loads, create the sudoku board by adding the matrix of boxes/cells
        /// and fill them with the default testing values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //test function to add hard coded numbers
            testFunction();
        }



        /// <summary>
        /// Traverse through the text of each text box to fill the table with numbers.
        /// The number of filled squares are tracked to determine when the puzzle is solved.
        /// </summary>
        public static void ReadTextboxs()
        {
            int col;
            int row;
            //length
            for (row = 0; row < kRowMax; row++)
            {
                // Height
                for (col = 0; col < kColMax; col++)
                {
                    try
                    {
                        table[row, col, 0] = Int32.Parse(txtBox[row, col].Text);
                        filledCells += 1;
                    }
                    catch (FormatException)
                    {
                        table[row, col, 0] = 0;
                    }
                }
                col = 0;
            }
        }


        /// <summary>
        /// In each square, all possible numbers for the square is checked. 
        /// If there is only one possible number left, that number is filled in the table
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public static bool FillSquare(int row, int col)
        {
            bool filled = false;
            int tally = 0;
            int buffer = 0;
            for (int i = 0; i < 10; ++i)
            {
                if (table[row, col, i] == 0)
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
                filledCells += 1;
                table[row, col, 0] = buffer;
                txtBox[row, col].Text = buffer.ToString();
                filled = true;
            }
            return filled;
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
            for (int i = 0; i < kRowMax; ++i)
            {
                if (table[i, curCol, 0] != 0)
                    table[curRow, curCol, table[i, curCol, 0]] = 1;
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
            for (int i = 0; i < kRowMax; ++i)
            {
                if (table[curRow, i, 0] != 0)
                    table[curRow, curCol, table[curRow, i, 0]] = 1;
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
            for (int i = boxLocation[0]; i < (kBoxSize + boxLocation[0]); ++i)
            {
                for (int j = boxLocation[1]; j < (kBoxSize + boxLocation[1]); ++j)
                {
                    if (table[i, j, 0] != 0)
                        table[curRow, curCol, table[i, j, 0]] = 1;
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
            box[0] = curRow / 3;
            box[1] = curCol / 3;
            for (int i = 0; i < 2; ++i)
            {
                if (box[i] < 1)
                {
                    boxLocation[i] = 0;
                }
                else if (box[i] >= 1 && box[i] < 2)
                {
                    boxLocation[i] = 3;
                }
                else if (box[i] >= 2)
                {
                    boxLocation[i] = 6;
                }
            }
        }



        public static bool CellStatus(int row, int col)
        {
            bool retCode = false;
            if (table[row, col, 0] == 0)
            {
                retCode = true;
            }
            return retCode;
        }




        /// <summary>
        /// Handler for when the text in a textbox changes.
        /// If a number other than 0 is not pressed then the
        /// number is filled out in the text box. If tab is pressed
        /// then the cursor goes to the next position
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBoxKeyDown(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || e.KeyChar == 48)
            {
                e.Handled = true;
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        /// Clear the board and fill the cells with default testing values
        /// </summary>
        void testFunction()
        {

            txtBox[0, 0].Text = "1";
            txtBox[0, 1].Text = "2";
            txtBox[0, 2].Text = "3";

            txtBox[0, 3].Text = "6";
            txtBox[0, 4].Text = "";
            txtBox[0, 5].Text = "8";

            txtBox[0, 6].Text = "";
            txtBox[0, 7].Text = "";
            txtBox[0, 8].Text = "";


            txtBox[1, 0].Text = "5";
            txtBox[1, 1].Text = "8";
            txtBox[1, 2].Text = "";

            txtBox[1, 3].Text = "";
            txtBox[1, 4].Text = "";
            txtBox[1, 5].Text = "9";

            txtBox[1, 6].Text = "7";
            txtBox[1, 7].Text = "";
            txtBox[1, 8].Text = "";


            txtBox[2, 0].Text = "";
            txtBox[2, 1].Text = "";
            txtBox[2, 2].Text = "";

            txtBox[2, 3].Text = "";
            txtBox[2, 4].Text = "4";
            txtBox[2, 5].Text = "";

            txtBox[2, 6].Text = "";
            txtBox[2, 7].Text = "";
            txtBox[2, 8].Text = "";


            txtBox[3, 0].Text = "3";
            txtBox[3, 1].Text = "7";
            txtBox[3, 2].Text = "";

            txtBox[3, 3].Text = "";
            txtBox[3, 4].Text = "";
            txtBox[3, 5].Text = "";

            txtBox[3, 6].Text = "5";
            txtBox[3, 7].Text = "";
            txtBox[3, 8].Text = "";


            txtBox[4, 0].Text = "6";
            txtBox[4, 1].Text = "";
            txtBox[4, 2].Text = "";

            txtBox[4, 3].Text = "5";
            txtBox[4, 4].Text = "";
            txtBox[4, 5].Text = "";

            txtBox[4, 6].Text = "";
            txtBox[4, 7].Text = "";
            txtBox[4, 8].Text = "4";


            txtBox[5, 0].Text = "";
            txtBox[5, 1].Text = "5";
            txtBox[5, 2].Text = "8";

            txtBox[5, 3].Text = "";
            txtBox[5, 4].Text = "";
            txtBox[5, 5].Text = "";

            txtBox[5, 6].Text = "";
            txtBox[5, 7].Text = "1";
            txtBox[5, 8].Text = "3";


            txtBox[6, 0].Text = "";
            txtBox[6, 1].Text = "";
            txtBox[6, 2].Text = "";

            txtBox[6, 3].Text = "";
            txtBox[6, 4].Text = "2";
            txtBox[6, 5].Text = "";

            txtBox[6, 6].Text = "";
            txtBox[6, 7].Text = "";
            txtBox[6, 8].Text = "";


            txtBox[7, 0].Text = "";
            txtBox[7, 1].Text = "";
            txtBox[7, 2].Text = "9";

            txtBox[7, 3].Text = "8";
            txtBox[7, 4].Text = "";
            txtBox[7, 5].Text = "";

            txtBox[7, 6].Text = "";
            txtBox[7, 7].Text = "3";
            txtBox[7, 8].Text = "6";


            txtBox[8, 0].Text = "7";
            txtBox[8, 1].Text = "";
            txtBox[8, 2].Text = "5";

            txtBox[8, 3].Text = "3";
            txtBox[8, 4].Text = "";
            txtBox[8, 5].Text = "6";

            txtBox[8, 6].Text = "";
            txtBox[8, 7].Text = "9";
            txtBox[8, 8].Text = "";

        }

        /// <summary>
        /// Display the lines between the groups of boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics line = e.Graphics;
            Pen pen = new Pen(Color.Black, 5);

            line.DrawLine(pen, 202, 50, 202, 510);
            line.DrawLine(pen, 357, 50, 357, 510);


            line.DrawLine(pen, 50, 202, 510, 202);
            line.DrawLine(pen, 50, 357, 510, 357);
        }


        /// <summary>
        /// Quit the program when the button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Allow the user to play again by clearing the board
        /// and temporarily filling it with test values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            //clearBoard();
            testFunction();
        }


        /// <summary>
        /// Allows the user to get a hint solving the sudoku puzzle
        /// by only filling out the next step of the puzzle instead of
        /// the whole puzzle being solved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStep_Click(object sender, EventArgs e)
        {
            Logic.Solve(true);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            Logic.Solve(false);
        }
    }
}
