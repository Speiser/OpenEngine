namespace OpenEngine
{
    public class Vector2D
    {
        public Vector2D() : this(0, 0) { }

        public Vector2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static Vector2D Zero => new Vector2D();
        public static Vector2D One => new Vector2D(1, 1);
    }
}
