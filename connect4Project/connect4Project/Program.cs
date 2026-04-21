using System;
namespace connect4Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.StartGame();
        }
    }

    class Controller
    {
        private Model model = new Model();
        private View view = new View();
        private Player player1;
        private Player player2;

        public void StartGame()
        {
            model.InitializeBoard();

            player1 = new HumanPlayer { Name = "Player 1", Symbol = 'X' };
            player2 = new HumanPlayer { Name = "Player 2", Symbol = 'O' };

            Player currentPlayer = player1;
            bool gameOver = false;

            while (!gameOver)
            {
                view.DisplayBoard(model.Board);

                Console.WriteLine($"{currentPlayer.Name}'s turn");

                int column = currentPlayer.GetMove() - 1;

                if (!model.DropPiece(column, currentPlayer.Symbol))
                {
                    Console.WriteLine("Column full or invalid! Try again.");
                    continue;
                }

                if (model.CheckWin(currentPlayer.Symbol))
                {
                    view.DisplayBoard(model.Board);
                    Console.WriteLine($"{currentPlayer.Name} wins!");
                    gameOver = true;
                }
                else if (model.IsBoardFull())
                {
                    view.DisplayBoard(model.Board);
                    Console.WriteLine("It's a draw!");
                    gameOver = true;
                }

                currentPlayer = (currentPlayer == player1) ? player2 : player1;
            }
        }
    }

    class Model
    {
        public char[,] Board = new char[6, 7];

        public void InitializeBoard()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                    Board[i, j] = '-';
        }

        public bool DropPiece(int column, char symbol)
        {
            if (column < 0 || column >= 7) return false;

            for (int row = 5; row >= 0; row--)
            {
                if (Board[row, column] == '-')
                {
                    Board[row, column] = symbol;
                    return true;
                }
            }
            return false;
        }

        public bool IsBoardFull()
        {
            for (int col = 0; col < 7; col++)
            {
                if (Board[0, col] == '-')
                    return false;
            }
            return true;
        }

        public bool CheckWin(char symbol)
        {
            // Horizontal
            for (int row = 0; row < 6; row++)
                for (int col = 0; col < 4; col++)
                    if (Board[row, col] == symbol &&
                        Board[row, col + 1] == symbol &&
                        Board[row, col + 2] == symbol &&
                        Board[row, col + 3] == symbol)
                        return true;

            // Vertical
            for (int col = 0; col < 7; col++)
                for (int row = 0; row < 3; row++)
                    if (Board[row, col] == symbol &&
                        Board[row + 1, col] == symbol &&
                        Board[row + 2, col] == symbol &&
                        Board[row + 3, col] == symbol)
                        return true;

            // Diagonal (down-right)
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 4; col++)
                    if (Board[row, col] == symbol &&
                        Board[row + 1, col + 1] == symbol &&
                        Board[row + 2, col + 2] == symbol &&
                        Board[row + 3, col + 3] == symbol)
                        return true;

            // Diagonal (up-right)
            for (int row = 3; row < 6; row++)
                for (int col = 0; col < 4; col++)
                    if (Board[row, col] == symbol &&
                        Board[row - 1, col + 1] == symbol &&
                        Board[row - 2, col + 2] == symbol &&
                        Board[row - 3, col + 3] == symbol)
                        return true;

            return false;
        }
    }

    abstract class Player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }

        public abstract int GetMove();
    }

    class HumanPlayer : Player
    {
        public override int GetMove()
        {
            while (true)
            {
                Console.Write("Enter column (1-7): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int column) && column >= 1 && column <= 7)
                {
                    return column;
                }

                Console.WriteLine("Invalid input. Try again.");
            }
        }
    }

    class View
    {
        public void DisplayBoard(char[,] board)
        {
            Console.WriteLine();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("1 2 3 4 5 6 7");
            Console.WriteLine();
        }
    }
}
