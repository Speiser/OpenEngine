namespace OpenEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const double updateCalls = 60;
            const double fps = 60;
            new Game(1280, 720, "Game")
                .Run(updateCalls, fps);
        }
    }
}
