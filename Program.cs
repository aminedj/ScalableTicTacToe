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
            bool full = (fuller.Contains(" ")) ? false : true;
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
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                goto start;
                throw;
            }
            var newgame = new tiktaktoe(dimension);
            System.Console.WriteLine("YOUR BOARD:");
            Char[] choice;
        nextturn:
            newgame.displayBoard();
            System.Console.WriteLine("PLayer {0} turn", newgame.turn);
            System.Console.WriteLine("INPUT YOUR X Y");
            choice = Console.ReadLine().ToCharArray();
            if (choice[0] == 'e' & choice.Length == 1) goto reask;
            try
            {
                newgame.Writevalue(Convert.ToInt32((choice[0]).ToString()), Convert.ToInt32((choice[1]).ToString()), newgame.turn);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                goto nextturn;
            }
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
            playagain = Convert.ToChar(Console.ReadLine());
            if (playagain == 'y' | playagain == 'Y') goto start;
        }
    }
}