namespace OpenEngine
{
    public abstract class GameBehaviour : Behaviour
    {
        /// <summary>
        /// Gets a reference to the <see cref="Game"/>.
        /// </summary>
        public Game Game { get; internal set; }
    }
}
