using System.Linq;
using OpenTK;

namespace OpenEngine
{
    /// <summary>
    /// The <see cref="Behaviour"/> base class.
    /// </summary>
    public abstract class Behaviour : IComponent
    {
        private Texture2D texture;

        /// <summary>
        /// The behaviour´s game object.
        /// </summary>
        protected readonly GameObject GameObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="Behaviour"/> class.
        /// </summary>
        protected Behaviour() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Behaviour"/> class.
        /// </summary>
        /// <param name="obj">The behaviour´s game object.</param>
        protected Behaviour(GameObject obj)
        {
            this.GameObject = obj;
        }

        public T FindObjectOfType<T>()
            => Game.Instance.GameObjects.OfType<T>().FirstOrDefault();

        public T[] FindObjectsOfType<T>()
            => Game.Instance.GameObjects.OfType<T>().ToArray();

        public void Instantiate(GameObject obj)
        {
            Game.Instance.GameObjects.Add(obj);
        }

        /// <summary>
        /// Called when added to the game object using
        /// <see cref="OpenEngine.GameObject.AddComponent(IComponent)"/>.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Called at each new frame.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called on frame render.
        /// </summary>
        /// <DesignDesicion>
        /// Behaviour.Render instead of the GameObject.Render!
        /// This desicion was made since I want that the engine's user
        /// has the ability to change (override) the render logic. This
        /// would not be possible if rendering would happen in GameObject,
        /// since it cannot be overwritten (so easily as in Behaviour).
        /// </DesignDesicion>
        public virtual void Render()
        {
            if (this.GameObject == null || !this.GameObject.IsEnabled) return;

            this.GameObject.Texture?.Draw(
                this.GameObject.Transform.Scale,
                new Vector2(
                    -this.GameObject.Transform.Position.X,
                    -this.GameObject.Transform.Position.Y));
        }
    }
}
