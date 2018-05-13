using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SDPixelFormat = System.Drawing.Imaging.PixelFormat;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace OpenEngine
{
    /// <summary>
    /// The <see cref="Texture2D"/> class.
    /// </summary>
    public struct Texture2D : IComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Texture2D"/> class.
        /// </summary>
        /// <param name="id">ID of the texture.</param>
        /// <param name="width">Width of the texture.</param>
        /// <param name="height">Height of the texture.</param>
        public Texture2D(int id, int width, int height)
        {
            this.ID = id;
            this.Width = width;
            this.Height = height;
            this.Color = Color.Transparent;
        }

        /// <summary>
        /// Gets the ID of the texture.
        /// </summary>
        public int ID { get; }
        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Gets the height of the texture.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Gets or sets the color of the texture (color overlay),
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Draws the texture with no offset and normal scale.
        /// </summary>
        public void Draw()
        {
            this.Draw(Vector2.Zero);
        }
        /// <summary>
        /// Draws the texture.
        /// </summary>
        /// <param name="offset">Offset/Position of the texture.</param>
        public void Draw(Vector2 offset)
        {
            this.Draw(Vector2.One, offset);
        }
        /// <summary>
        /// Draws the texture.
        /// </summary>
        /// <param name="scale">Scale of the texture.</param>
        /// <param name="offset">Offset/Position of the texture.</param>
        public void Draw(Vector2 scale, Vector2 offset)
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

            GL.Color3(this.Color);

            for (var i = 0; i < vertices.Length; i++)
            {
                GL.TexCoord2(vertices[i]);
                vertices[i].X *= this.Width;
                vertices[i].Y *= this.Height;
                vertices[i] -= offset;
                vertices[i] *= scale;

                GL.Vertex2(vertices[i]);
            }

            GL.End();
        }

        /// <summary>
        /// Loads a texture from a specified file and returns it.
        /// </summary>
        /// <param name="file">Specified file.</param>
        /// <returns>A texture from a specified file.</returns>
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
