namespace OpenEngine
{
    public abstract class GameBehaviour
    {
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
