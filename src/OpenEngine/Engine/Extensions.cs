using OpenTK;

namespace OpenEngine
{
    public static class Extensions
    {
        public static string RemoveFileExtension(this string text)
        {
            return text.Replace(".jpg", string.Empty)
                       .Replace(".png", string.Empty);
        }

        public static Vector2 ToVector2(this Vector2D v)
            => new Vector2(v.X, v.Y);
    }
}
