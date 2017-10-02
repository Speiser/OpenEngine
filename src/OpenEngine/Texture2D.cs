using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SDPixelFormat = System.Drawing.Imaging.PixelFormat;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace OpenEngine
{
    public struct Texture2D
    {
        public Texture2D(int id, int width, int height)
        {
            this.ID = id;
            this.Width = width;
            this.Height = height;
        }

        public int ID { get; }
        public int Width { get; }
        public int Height { get; }

        public void Draw(Vector2 offset)
        {
            this.Draw(Vector2.Zero, Vector2.One, offset);
        }

        public void Draw(Vector2 position, Vector2 scale, Vector2 offset)
        {
            this.Draw(position, scale, offset, Color.Transparent);
        }

        public void Draw(Vector2 position, Vector2 scale, Vector2 offset, Color color)
        {
            var vertices = new[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };

            GL.BindTexture(TextureTarget.Texture2D, this.ID);
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(color);

            for (var i = 0; i < vertices.Length; i++)
            {
                GL.TexCoord2(vertices[i]);
                vertices[i].X *= this.Width;
                vertices[i].Y *= this.Height;
                vertices[i] -= offset;
                vertices[i] *= scale;
                vertices[i] += position;

                GL.Vertex2(vertices[i]);
            }

            GL.End();
        }

        public static Texture2D LoadTexture(string file)
        {
            if (!File.Exists("Textures/" + file))
                throw new FileNotFoundException("No texture found at /Textures/" + file + ".");

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            var bmp = new Bitmap("Textures/" + file);
            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height), 
                ImageLockMode.ReadOnly,
                SDPixelFormat.Format32bppArgb);

            GL.TexImage2D(
                TextureTarget.Texture2D, 
                0, 
                PixelInternalFormat.Rgba, 
                data.Width, 
                data.Height, 
                0, 
                GLPixelFormat.Bgra, 
                PixelType.UnsignedByte, 
                data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return new Texture2D(id, bmp.Width, bmp.Height);
        }
    }
}
