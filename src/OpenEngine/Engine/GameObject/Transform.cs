using OpenTK;

namespace OpenEngine
{
    public class Transform
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;

        public Transform Clone()
            => new Transform
            {
                Position = new Vector2(this.Position.X, this.Position.Y),
                Scale = new Vector2(this.Scale.X, this.Scale.Y)
            };
    }
}
