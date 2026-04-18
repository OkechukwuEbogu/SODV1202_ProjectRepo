
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
        public void StartGame()
        {
            // Game flow will go here
        }
    }

    class Model
    {
        public char[,] Board = new char[6, 7];

        public void InitializeBoard()
        {
            // Initialize board
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
            return 0;
        }
    }

    class View
    {
        public void DisplayBoard(char[,] board)
        {
            // Display logic here
        }
    }
}
