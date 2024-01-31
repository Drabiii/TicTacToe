using System;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            Console.WriteLine("Play again ?? y/n ");
            string pAgain = Console.ReadLine();
            if (pAgain == "Y" || pAgain == "y")
            {
                game.Start();
            }
            else
            {
                System.Environment.Exit(0);
            }
        }
    }

    class Board
    {
        private char[,] board;
        public Board()
        {
            board = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        public void Draw()
        {
            Console.WriteLine("  1 2 3");
            Console.WriteLine(" ┌─────┐");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + 1 + "│");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j < 2)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("│");
            }
            Console.WriteLine(" └─────┘");
        }

        public bool IsFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsWinner(char symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol)
                {
                    return true;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == symbol && board[1, j] == symbol && board[2, j] == symbol)
                {
                    return true;
                }
            }

            if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
            {
                return true;
            }

            if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
            {
                return true;
            }

            return false;
        }
        public bool IsFree(int row, int col)
        {
            return board[row, col] == ' ';
        }
        public void SetSymbol(int row, int col, char symbol)
        {
            board[row, col] = symbol;
        }
    }

    class Player
    {
        private char symbol;
        public Player(char symbol)
        {
            this.symbol = symbol;
        }
        public char GetSymbol()
        {
            return symbol;
        }
        public void MakeMove(Board board)
        {
            Console.WriteLine("Player " + symbol + ", enter the coordinates of the move (row column):");
            string input = Console.ReadLine();
            if (input.Length != 2 || input[0] < '1' || input[0] > '3' || input[1] < '1' || input[1] > '3')
            {
                Console.WriteLine("Invalid coordinates, try again.");
                MakeMove(board);
                return;
            }
            int row = input[0] - '1';
            int col = input[1] - '1';
            if (!board.IsFree(row, col))
            {
                Console.WriteLine("The field is occupied, try again.");
                MakeMove(board);
                return;
            }
            board.SetSymbol(row, col, symbol);
        }
    }
    class Game
    {
        private Board board;
        private Player playerX;
        private Player playerO;
        private Player currentPlayer;
        public Game()
        {
            board = new Board();
            playerX = new Player('X');
            playerO = new Player('O');
            currentPlayer = playerX;
        }
        public void Start()
        {
            Console.WriteLine("Welcome to the tic-tac-toe game!");

            while (true)
            {
                board.Draw();
                currentPlayer.MakeMove(board);

                if (board.IsWinner(currentPlayer.GetSymbol()))
                {
                    Console.WriteLine("Congratulations, player " + currentPlayer.GetSymbol() + " won!");
                    break;
                }

                if (board.IsFull())
                {
                    Console.WriteLine("The board is full, draw!");
                    break;
                }
                
                if (currentPlayer == playerX)
                {
                    currentPlayer = playerO;
                }
                else
                {
                    currentPlayer = playerX;
                }
            }
            board.Draw();
        }
    }
}