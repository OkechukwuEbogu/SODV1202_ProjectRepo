
using System;
namespace connect4Project
{
    using System;

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

        public void StartGame()
        {
            model.InitializeBoard();
            view.DisplayBoard(model.Board);
        }
    }

    class Model
    {
        public char[,] Board = new char[6, 7];

        public void InitializeBoard()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Board[i, j] = '-';
                }
            }
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
            Console.Write("Enter column (1-7): ");
            return int.Parse(Console.ReadLine());
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
