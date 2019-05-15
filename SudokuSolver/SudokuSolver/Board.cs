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
        public static Cell[,] cell = new Cell[kRowMax, kColMax];
        List<string> testSpel = new List<string>();

        //public static int[,,] table = new int[9, 9, 10];
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
        const int kNumBoxes = 81;
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

                    txtBox[row, col].Name = "txtBox" + col + row;
                    txtBox[row, col].TextAlign = HorizontalAlignment.Center;
                    //txtBox[row, col].BackColor = Color.Transparent;
                    txtBox[row, col].MaxLength = 1;
                    txtBox[row, col].Font = new Font(txtBox[row, col].Font.FontFamily, 28);
                    txtBox[row, col].Size = new Size(50, 50);
                    txtBox[row, col].Location = new Point(x, y);
                    txtBox[row, col].KeyPress += new KeyPressEventHandler(txtBoxKeyDown);
                    txtBox[row, col].TextChanged += new EventHandler(TxtBox_TextChanged);
                    this.Controls.Add(txtBox[row, col]);
                    txtBox[row, col].SendToBack();

                    if (col == 2 || col == 5)
                    {
                        x += 5;
                    }

                    x += kBoxWdth;

                    cell[row, col] = new Cell("Cell " + row + "," + col);

                    boxCount += 1;

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

        private void TxtBox_TextChanged(object sender, EventArgs e)
        {
            //TextBox t = (TextBox)sender;
            //// t is the textbox you referred
            //MessageBox.Show(t.Name);
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
            RadomizeBoard();
        }



        /// <summary>
        /// Traverse through the text of each text box to fill the table with numbers.
        /// The number of filled Cells are tracked to determine when the puzzle is solved.
        /// </summary>
        public void ReadTextBoxes()
        {
            int col;
            int row;
            //length
            row = 0;
            for (row = 0; row < kRowMax; row++)
            {
                // Height
                for (col = 0; col < kColMax; col++)
                {
                    try
                    {
                        cell[row, col].SetAnswer(Int32.Parse(txtBox[row, col].Text));
                        //table[row, col, 0] = ;
                        filledCells += 1;
                    }
                    catch (FormatException)
                    {
                        //table[row, col, 0] = 0;
                    }
                }
                col = 0;
            }
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
            if (e.KeyChar == 32)//9 == tab
            {

                ((TextBox)sender).Text = "";
            
                SendKeys.Send("{TAB}");
            }
            else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || e.KeyChar == 48) //48 = 0
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
        void RadomizeBoard()
        {
            //Random rnd = new Random();

            //for(int i = 0; i < kColMax; ++i)
            //{
            //    for(int j = 0; j < kRowMax; ++j)
            //    {
            //        if (rnd.Next(3) == 1)
            //        {
            //            txtBox[i, j].Text = j.ToString();
            //        }
            //        else
            //        {
            //            txtBox[i, j].Text = "";
            //        }

            //    }
            //}
            //txtBox[0, 0].Text = "1";
            //txtBox[0, 1].Text = "2";
            //txtBox[0, 2].Text = "3";

            //txtBox[0, 3].Text = "6";
            //txtBox[0, 4].Text = "";
            //txtBox[0, 5].Text = "8";

            //txtBox[0, 6].Text = "";
            //txtBox[0, 7].Text = "";
            //txtBox[0, 8].Text = "";


            //txtBox[1, 0].Text = "5";
            //txtBox[1, 1].Text = "8";
            //txtBox[1, 2].Text = "";

            //txtBox[1, 3].Text = "";
            //txtBox[1, 4].Text = "";
            //txtBox[1, 5].Text = "9";

            //txtBox[1, 6].Text = "7";
            //txtBox[1, 7].Text = "";
            //txtBox[1, 8].Text = "";


            //txtBox[2, 0].Text = "";
            //txtBox[2, 1].Text = "";
            //txtBox[2, 2].Text = "";

            //txtBox[2, 3].Text = "";
            //txtBox[2, 4].Text = "4";
            //txtBox[2, 5].Text = "";

            //txtBox[2, 6].Text = "";
            //txtBox[2, 7].Text = "";
            //txtBox[2, 8].Text = "";


            //txtBox[3, 0].Text = "3";
            //txtBox[3, 1].Text = "7";
            //txtBox[3, 2].Text = "";

            //txtBox[3, 3].Text = "";
            //txtBox[3, 4].Text = "";
            //txtBox[3, 5].Text = "";

            //txtBox[3, 6].Text = "5";
            //txtBox[3, 7].Text = "";
            //txtBox[3, 8].Text = "";


            //txtBox[4, 0].Text = "6";
            //txtBox[4, 1].Text = "";
            //txtBox[4, 2].Text = "";

            //txtBox[4, 3].Text = "5";
            //txtBox[4, 4].Text = "";
            //txtBox[4, 5].Text = "";

            //txtBox[4, 6].Text = "";
            //txtBox[4, 7].Text = "";
            //txtBox[4, 8].Text = "4";


            //txtBox[5, 0].Text = "";
            //txtBox[5, 1].Text = "5";
            //txtBox[5, 2].Text = "8";

            //txtBox[5, 3].Text = "";
            //txtBox[5, 4].Text = "";
            //txtBox[5, 5].Text = "";

            //txtBox[5, 6].Text = "";
            //txtBox[5, 7].Text = "1";
            //txtBox[5, 8].Text = "3";


            //txtBox[6, 0].Text = "";
            //txtBox[6, 1].Text = "";
            //txtBox[6, 2].Text = "";

            //txtBox[6, 3].Text = "";
            //txtBox[6, 4].Text = "2";
            //txtBox[6, 5].Text = "";

            //txtBox[6, 6].Text = "";
            //txtBox[6, 7].Text = "";
            //txtBox[6, 8].Text = "";


            //txtBox[7, 0].Text = "";
            //txtBox[7, 1].Text = "";
            //txtBox[7, 2].Text = "9";

            //txtBox[7, 3].Text = "8";
            //txtBox[7, 4].Text = "";
            //txtBox[7, 5].Text = "";

            //txtBox[7, 6].Text = "";
            //txtBox[7, 7].Text = "3";
            //txtBox[7, 8].Text = "6";


            //txtBox[8, 0].Text = "7";
            //txtBox[8, 1].Text = "";
            //txtBox[8, 2].Text = "5";

            //txtBox[8, 3].Text = "3";
            //txtBox[8, 4].Text = "";
            //txtBox[8, 5].Text = "6";

            //txtBox[8, 6].Text = "";
            //txtBox[8, 7].Text = "9";
            //txtBox[8, 8].Text = "";





            txtBox[0, 0].Text = "8";
            txtBox[0, 1].Text = "";
            txtBox[0, 2].Text = "";

            txtBox[0, 3].Text = "";
            txtBox[0, 4].Text = "9";
            txtBox[0, 5].Text = "";

            txtBox[0, 6].Text = "";
            txtBox[0, 7].Text = "";
            txtBox[0, 8].Text = "3";


            txtBox[1, 0].Text = "";
            txtBox[1, 1].Text = "";
            txtBox[1, 2].Text = "6";

            txtBox[1, 3].Text = "";
            txtBox[1, 4].Text = "";
            txtBox[1, 5].Text = "";

            txtBox[1, 6].Text = "1";
            txtBox[1, 7].Text = "";
            txtBox[1, 8].Text = "";


            txtBox[2, 0].Text = "";
            txtBox[2, 1].Text = "5";
            txtBox[2, 2].Text = "9";

            txtBox[2, 3].Text = "8";
            txtBox[2, 4].Text = "";
            txtBox[2, 5].Text = "4";

            txtBox[2, 6].Text = "7";
            txtBox[2, 7].Text = "6";
            txtBox[2, 8].Text = "";


            txtBox[3, 0].Text = "";
            txtBox[3, 1].Text = "";
            txtBox[3, 2].Text = "1";

            txtBox[3, 3].Text = "";
            txtBox[3, 4].Text = "8";
            txtBox[3, 5].Text = "";

            txtBox[3, 6].Text = "5";
            txtBox[3, 7].Text = "";
            txtBox[3, 8].Text = "";


            txtBox[4, 0].Text = "4";
            txtBox[4, 1].Text = "";
            txtBox[4, 2].Text = "";

            txtBox[4, 3].Text = "3";
            txtBox[4, 4].Text = "";
            txtBox[4, 5].Text = "1";

            txtBox[4, 6].Text = "";
            txtBox[4, 7].Text = "";
            txtBox[4, 8].Text = "7";


            txtBox[5, 0].Text = "";
            txtBox[5, 1].Text = "";
            txtBox[5, 2].Text = "8";

            txtBox[5, 3].Text = "";
            txtBox[5, 4].Text = "6";
            txtBox[5, 5].Text = "";

            txtBox[5, 6].Text = "3";
            txtBox[5, 7].Text = "";
            txtBox[5, 8].Text = "";


            txtBox[6, 0].Text = "";
            txtBox[6, 1].Text = "8";
            txtBox[6, 2].Text = "3";

            txtBox[6, 3].Text = "9";
            txtBox[6, 4].Text = "";
            txtBox[6, 5].Text = "6";

            txtBox[6, 6].Text = "2";
            txtBox[6, 7].Text = "1";
            txtBox[6, 8].Text = "";


            txtBox[7, 0].Text = "";
            txtBox[7, 1].Text = "";
            txtBox[7, 2].Text = "7";

            txtBox[7, 3].Text = "";
            txtBox[7, 4].Text = "";
            txtBox[7, 5].Text = "";

            txtBox[7, 6].Text = "8";
            txtBox[7, 7].Text = "";
            txtBox[7, 8].Text = "";


            txtBox[8, 0].Text = "1";
            txtBox[8, 1].Text = "";
            txtBox[8, 2].Text = "";

            txtBox[8, 3].Text = "";
            txtBox[8, 4].Text = "3";
            txtBox[8, 5].Text = "";

            txtBox[8, 6].Text = "";
            txtBox[8, 7].Text = "";
            txtBox[8, 8].Text = "9";


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
        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Allow the user to play again by clearing the board
        /// and temporarily filling it with test values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            //clearBoard();
            RadomizeBoard();
        }


        /// <summary>
        /// Allows the user to get a hint solving the sudoku puzzle
        /// by only filling out the next step of the puzzle instead of
        /// the whole puzzle being solved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStep_Click(object sender, EventArgs e)
        {
            ReadTextBoxes();
            Logic.Solve(true);
        }

        private void BtnSolve_Click(object sender, EventArgs e)
        {
            //write the values of the board to the table
            ReadTextBoxes();
            Logic.Solve(false);
        }
    }
}
