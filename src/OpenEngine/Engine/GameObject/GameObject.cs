using System.Collections.Generic;
using OpenEngine.Engine.Components;

namespace OpenEngine
{
    /// <summary>
    /// The <see cref="GameObject"/> class.
    /// </summary>
    public class GameObject
    {
        private readonly List<IComponent> components;

        /// <summary>
        /// Cached behavior for "better" performance.
        /// </summary>
        private Behaviour behaviour;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        public GameObject()
        {
            this.components = new List<IComponent>();
            this.Transform = new Transform();
            this.IsEnabled = true;
            this.Texture = null;
            this.behaviour = new DefaultBehaviour(this);
        }
        /// <summary>
        /// Internal ctor for cloning.
        /// </summary>
        private GameObject(List<IComponent> components, Behaviour behaviour)
        {
            this.components = components;
            this.behaviour = behaviour ?? new DefaultBehaviour(this);
        }

        /// <summary>
        /// If this is set and <see cref="IsEnabled"/> is true,
        /// the texture will be rendered by <see cref="Behaviour.Render()"/>.
        /// </summary>
        internal Texture2D? Texture { get; set; }

        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets the name of the game object.
        /// </summary>
        public string Name { get; set; } = "GameObject";
        /// <summary>
        /// Gets or sets the transform of the game object.
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// Adds a component to the game object.
        /// </summary>
        /// <param name="component">A component.</param>
        public void AddComponent(IComponent component)
        {
            this.components.Add(component);

            // ReSharper disable once InconsistentNaming
            if (component is Behaviour behaviour_)
            {
                this.behaviour = behaviour_;
                this.behaviour.Start();
            }
            else if (component is Texture2D texture)
            {
                this.Texture = texture;
            }
        }

        /// <summary>
        /// Creates a copy of the game object and returns it.
        /// </summary>
        /// <returns>A copy of the game object.</returns>
        public GameObject Clone()
        {
            if (this.behaviour is DefaultBehaviour)
            {
                this.behaviour = null;
            }

            return new GameObject(this.components, this.behaviour)
            {
                Name = this.Name,
                Transform = this.Transform.Clone(),
                Texture = this.Texture,
                IsEnabled = this.IsEnabled
            };
        } 
        /// <summary>
        /// Gets a component of the specified type T.
        /// Returns the first component found of the type T.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <returns>A component of the specified type T.</returns>
        /// <exception cref="ComponentNotFoundException">
        /// Thrown if the game object doesn´t contain a component of type T.
        /// </exception>
        public T GetComponent<T>() where T : IComponent
        {
            foreach (var component in this.components)
            {
                // ReSharper disable once MergeCastWithTypeCheck
                if (component is T foundComponent)
                    return foundComponent;
            }
            
            throw new ComponentNotFoundException($"The specified component of type {typeof(T)}" + 
                                                 $"could not be found in GameObject: {this.Name}.");
        }
        /// <summary>
        /// Invoked once every frame.
        /// </summary>
        internal void Update()
        {
            this.behaviour?.Update();
            this.behaviour?.Render();
        }
    }
}
