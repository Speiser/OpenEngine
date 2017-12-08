namespace OpenEngine
{
    public class Transform
    {
        public Vector2D Position { get; set; } = Vector2D.Zero;
        public Vector2D Scale { get; set; } = Vector2D.One;

        public Transform Clone()
        {
            return new Transform
            {
                Position = new Vector2D(this.Position.X, this.Position.Y),
                Scale = new Vector2D(this.Scale.X, this.Scale.Y)
            };
        }
    }
}
