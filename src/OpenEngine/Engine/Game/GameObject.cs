using System;
using System.Collections.Generic;

namespace OpenEngine
{
    /// <summary>
    /// The <see cref="GameObject"/> class.
    /// </summary>
    public class GameObject
    {
        private readonly List<IComponent> _components;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        public GameObject()
        {
            _components = new List<IComponent>();
            this.Transform = new Transform();
        }
        /// <summary>
        /// Internal ctor for cloning.
        /// </summary>
        internal GameObject(List<IComponent> components)
        {
            _components = components;
        }

        /// <summary>
        /// Gets or sets the name of the game object.
        /// </summary>
        public string Name { get; set; } = "GameObject";
        /// <summary>
        /// Gets or sets the action that will be called once every second.
        /// </summary>
        public Action<GameObject> UpdateAction { get; set; }
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
            _components.Add(component);
        }
        /// <summary>
        /// Creates a copy of the game object and returns it.
        /// </summary>
        /// <returns>A copy of the game object.</returns>
        public GameObject Clone()
            => new GameObject(_components)
               {
                   Name = this.Name,
                   UpdateAction = this.UpdateAction,
                   Transform = this.Transform.Clone(),
               };
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
            foreach (var component in _components)
            {
                // ReSharper disable once MergeCastWithTypeCheck
                if (component is T)
                    return (T)component;
            }
            
            throw new ComponentNotFoundException($"The specified component of type {typeof(T)}" + 
                                                 $"could not be found in GameObject: {this.Name}.");
        }
        /// <summary>
        /// Invoked once every frame.
        /// Invokes <see cref="UpdateAction"/>.
        /// </summary>
        public void Update()
        {
            this.UpdateAction?.Invoke(this);
        }
    }
}
