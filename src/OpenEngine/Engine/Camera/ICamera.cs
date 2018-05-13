using System.Drawing;
using OpenTK;

namespace OpenEngine
{
    public interface ICamera
    {
        Color SkyboxColor { get; set; }
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        float Zoom { get; set; }
        float ZNear { get; set; }
        float ZFar { get; set; }

        void ApplyTransform();
        Vector2 ScreenToWorldCoordinates(Vector2 rawInput);
        void Update();
        void Move(Vector2 vec);
    }
}
