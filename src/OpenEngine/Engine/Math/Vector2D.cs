namespace OpenEngine
{
    public struct Vector2D
    {
        public Vector2D(float x = 0, float y = 0)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static Vector2D Zero => new Vector2D();
        public static Vector2D One => new Vector2D(1, 1);

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
        public static Vector2D operator *(Vector2D a, float b) => new Vector2D(a.X + b, a.Y + b);
    }
}
