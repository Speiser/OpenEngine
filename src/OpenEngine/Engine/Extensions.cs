namespace OpenEngine
{
    public static class Extensions
    {
        public static string RemoveFileExtension(this string text)
        {
            return text.Replace(".jpg", string.Empty)
                       .Replace(".png", string.Empty);
        }
    }
}
