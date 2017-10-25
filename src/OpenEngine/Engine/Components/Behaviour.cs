namespace OpenEngine
{
    /// <summary>
    /// The <see cref="Behaviour"/> base class.
    /// </summary>
    public abstract class Behaviour : IComponent
    {
        /// <summary>
        /// The behaviour´s game object.
        /// </summary>
        protected GameObject Object;

        /// <summary>
        /// Initializes a new instance of the <see cref="Behaviour"/> class.
        /// </summary>
        /// <param name="obj">The behaviour´s game object.</param>
        protected Behaviour(GameObject obj)
        {
            Object = obj;
        }

        /// <summary>
        /// Called when added to the game object using <see cref="GameObject.AddComponent(IComponent)"/>.
        /// </summary>
        public virtual void Start() { }
        /// <summary>
        /// Called at each new frame.
        /// </summary>
        public virtual void Update() { }
    }
}
