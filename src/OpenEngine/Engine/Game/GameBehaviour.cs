namespace OpenEngine
{
    public abstract class GameBehaviour
    {
        /// <summary>
        /// Gets a reference to the <see cref="Game"/>.
        /// </summary>
        public Game Game { get; internal set; }

        /// <summary>
        /// Called on load.
        /// </summary>
        public virtual void Start() { }
        /// <summary>
        /// Called on frame update.
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// Called on frame render.
        /// </summary>
        public virtual void Render() { }
    }
}
