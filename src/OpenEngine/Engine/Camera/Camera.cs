using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenEngine
{
    public class Camera : ICamera
    {
        public Camera(Vector2D postion, float startZoom = 1.0f, float startRotation = 0, float zNear = 0, float zFar = 1)
        {
            this.Position = postion;
            this.Zoom = startZoom;
            this.Rotation = startRotation;
            this.ZNear = zNear;
            this.ZFar = zFar;
        }

        public Color SkyboxColor { get; set; } = Color.DimGray;
        public Vector2D Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public float ZNear { get; set; }
        public float ZFar { get; set; }

        public void ApplyTransform()
        {
            var transform = Matrix4.Identity;
            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-this.Position.X, -this.Position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-this.Rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale(this.Zoom, this.Zoom, 1));

            GL.MultMatrix(ref transform);
        }

        public Vector2 ScreenToWorldCoordinates(Vector2 rawInput)
        {
            rawInput /= this.Zoom;
            var dX = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
            var dY = new Vector2((float)Math.Cos(this.Rotation + MathHelper.PiOver2), (float)Math.Sin(this.Rotation + MathHelper.PiOver2));

            return (this.Position.ToVector2() + dX * rawInput.X + dY * rawInput.Y);
        }

        public void Update()
        {
        }

        public void Move(Vector2D vec)
        {
            this.Position += vec;
        }
    }
}
