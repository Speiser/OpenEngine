﻿using System.Collections.Generic;

namespace OpenEngine
{
    public class GameObject
    {
        public GameObject()
        {
            this.Components = new List<IComponent>();
            this.Transform = new Transform();
        }

        public string Name { get; set; } = "GameObject";
        public Transform Transform { get; set; }
        public List<IComponent> Components { get; private set; }

        public void AddComponent(IComponent component)
        {
            this.Components.Add(component);
        }

        public GameObject Clone()
        {
            return new GameObject
            {
                Name = this.Name,
                Transform = this.Transform,
                Components = this.Components
            };
        }

        public T GetComponent<T>() where T : IComponent
        {
            foreach (var component in this.Components)
            {
                if (component is T)
                    return (T)component;
            }
            return default(T);
        }
    }
}
