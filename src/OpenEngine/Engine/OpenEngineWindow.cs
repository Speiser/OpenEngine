namespace OpenEngine
{
    public class OpenEngineWindow
    {
        private readonly Game _game;

        public OpenEngineWindow(Game game)
        {
            _game = game;
        }

        public OpenEngineWindow(int width, int height, string title, GameBehaviour behaviour)
        {
            _game = new Game(width, height, title, behaviour);
        }

        public void Start(int updatesPerSecond = 60, int framesPerSecond = 60)
        {
            _game.Run(updatesPerSecond, framesPerSecond);
        }
    }
}
