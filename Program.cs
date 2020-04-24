using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class tiktaktoe
    {
        private bool won { get; set; }
        public int turn { get; set; }
        public int dim { get; set; }
        public string[,] table { get; set; }
        public tiktaktoe(int dimension)
        {
            var table1 = new string[dimension, dimension];
            for (int i = 0; i < dimension; ++i)
            {
                for (int j = 0; j < dimension; ++j)
                {
                    table1[i, j] = " ";
                }
            }
            table = table1;
            dim = dimension;
            turn = 1;
            won = false;
        }
        public void Writevalue(int x, int y, int player)
        {
            if (x > dim | x > dim)
            {
                throw new System.IndexOutOfRangeException();
            }

            if (table[x, y] == " ")
            {
                table[x, y] = (player == 1) ? "X" : "O";
            }
            else
            {
                throw new Exception("Cell already filled");
            }
        }
        public void changeturn()
        {
            turn = (turn == 1) ? 2 : 1;
        }
        public bool isFull()
        {
            var fuller = new List<string>(dim);
            foreach (string item in table)
            {
                fuller.Add(item);
            }
            // for (int j = 0; j < dim; j++)
            // {
            //     for (int i = 0; i < dim; i++)
            //     {
            //        fuller.Add(Convert.ToString(table.GetValue(i, j))); 
            //     }
            //  } 
            bool full = (fuller.Contains(" ")) ? false : true;
            // if (fuller.Contains(" "))
            // {
            //     return false;
            // }
            // else
            // {
            //     return true;
            // }
            return full;
        }
        public void displayBoard()
        {
            string delimiter = "";
            string percolumn = "";
            for (int i = 0; i < dim; i++)
            {
                percolumn += "-";
            }
            for (int i = 0; i < dim; i++)
            {
                delimiter += "----";

            }
            delimiter += percolumn;
            for (int i = 0; i < dim; ++i)
            {
                System.Console.WriteLine("\r");
                System.Console.WriteLine(delimiter);
                System.Console.Write("|");


                for (int j = 0; j < dim; ++j)
                {

                    System.Console.Write(" " + table[i, j] + " | ");
                }
            }
            System.Console.WriteLine("\r");
            System.Console.WriteLine(delimiter);
        }

        public bool checkwin()
        {
            //check for first diagonal win
            var checker = new List<char>(dim);
            for (int i = 0; i < dim; i++)
            {
                checker.Add(Convert.ToChar(table[i, i]));
            }
            if (!(checker.Distinct().Skip(1).Any()) & !(checker.Contains(' ')))
            {
                won = true;
            }
            checker.Clear();
            //check for second diagonal win
            for (int i = 0; i < dim; i++)
            {
                checker.Add(Convert.ToChar(table[i, dim - 1 - i]));
            }
            if (!(checker.Distinct().Skip(1).Any()) & !(checker.Contains(' ')))
            {
                won = true;
            }
            //check for horizontal win
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    checker.Add(Convert.ToChar(table[i, j]));
                }
                if (!(checker.Distinct().Skip(1).Any()) & !(checker.Contains(' ')))
                {
                    won = true;
                }
                checker.Clear();
            }
            //check for vertical win
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    checker.Add(Convert.ToChar(table[j, i]));
                }
                if (!(checker.Distinct().Skip(1).Any()) & !(checker.Contains(' ')))
                {
                    won = true;
                }
                checker.Clear();
            }
            return won;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
        start:
            System.Console.WriteLine("CHOOSE DIMENSIONS");
            int dimension;
            try
            {
                dimension = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                System.Console.WriteLine("DEMNESION IN NUMBERZZZZ");
                goto start;
                throw;
            }
            var newgame = new tiktaktoe(dimension);
            System.Console.WriteLine("YOUR BOARD:");
            newgame.displayBoard();
            int choicex;
            int choicey;
        nextturn:
            System.Console.WriteLine("PLayer {0} turn", newgame.turn);
            System.Console.WriteLine("INPUT YOUR X Y");
            try
            {
                System.Console.WriteLine("input x:");
                choicex = Convert.ToInt32(Console.ReadLine());
                System.Console.WriteLine("input y:");
                choicey = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                System.Console.WriteLine("NUMBERZZZ");
                goto nextturn;
            }

            try
            {
                newgame.Writevalue(choicex, choicey, newgame.turn);
            }
            catch (System.IndexOutOfRangeException)
            {
                System.Console.WriteLine("STAY WITHIN YOUR DIMENSIONS");
                goto nextturn;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                goto nextturn;
            }
            newgame.displayBoard();
            if (newgame.isFull())
            {
                System.Console.WriteLine("its a tie !");
                goto reask;
            }
            if (newgame.checkwin())
            {
                System.Console.WriteLine("player {0} has won !!!", newgame.turn);
                goto reask;
            }
            else
            {
                newgame.changeturn();
                goto nextturn;
            }
        reask:
            System.Console.WriteLine("play again? (Y/N)");
            char playagain;
            try
            {
                playagain = Convert.ToChar(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                System.Console.WriteLine("Y OR N BITCH");
                goto reask;
            }
            if (playagain == 'y' | playagain == 'Y')
            {
                goto start;
            }
        }
    }
}
