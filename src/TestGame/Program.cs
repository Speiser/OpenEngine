using OpenEngine;

namespace TestGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Debug.HideConsole();
            var g = new OpenEngineWindow(1280, 720, "TestGame", new MyGameBehaviour());
            g.Start();
        }
    }
}
