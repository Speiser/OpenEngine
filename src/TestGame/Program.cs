using OpenEngine;

namespace TestGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Debug.HideConsole();
            var g = new Game(1280, 720, "TestGame", new MyGameBehaviour());
            g.Run(60, 60);
        }
    }
}
