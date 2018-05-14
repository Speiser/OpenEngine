using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenEngine
{
    public class Camera : GameObject
    {
        public Camera(Vector2 postion, float scale = 1.0f, float rotation = 0, float zNear = 0, float zFar = 1)
        {
            this.Transform.Position = postion;
            this.Transform.Scale = new Vector2(scale);
            this.Rotation = rotation;
            this.ZNear = zNear;
            this.ZFar = zFar;
        }

        public Color SkyboxColor { get; set; } = Color.DimGray;
        public float Rotation { get; set; }
        public float ZNear { get; set; }
        public float ZFar { get; set; }

        public Vector2 ScreenToWorldCoordinates(Vector2 rawInput)
        {
            rawInput /= this.Transform.Scale.X;

            var dX = new Vector2(
                (float)Math.Cos(this.Rotation),
                (float)Math.Sin(this.Rotation));

            var dY = new Vector2(
                (float)Math.Cos(this.Rotation + MathHelper.PiOver2),
                (float)Math.Sin(this.Rotation + MathHelper.PiOver2));

            return this.Transform.Position + dX * rawInput.X + dY * rawInput.Y;
        }

        public new void Update()
        {
            base.Update();
        }

        public void Move(Vector2 vec)
        {
            this.Transform.Position += vec;
        }

        internal void ApplyTransform()
        {
            var transform = Matrix4.Identity;

            // Position
            transform = Matrix4.Mult(
                transform,
                Matrix4.CreateTranslation(
                    -this.Transform.Position.X,
                    -this.Transform.Position.Y,
                    0));

            // Rotation
            transform = Matrix4.Mult(
                transform,
                Matrix4.CreateRotationZ(-this.Rotation));

            // Scale
            transform = Matrix4.Mult(
                transform,
                Matrix4.CreateScale(
                    this.Transform.Scale.X,
                    this.Transform.Scale.Y,
                    1));

            GL.MultMatrix(ref transform);
        }
    }
}
