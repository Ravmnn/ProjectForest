using SFML.Window;

using Latte.Core;
using Latte.Application;
using Latte.Core.Type;

using Milkway;


namespace ProjectForest;


public class MouseScreenDragger(Camera camera) : IUpdateable
{
    public Camera Camera { get; set; } = camera;
    public float ZoomOutFactor { get; set; } = 1.2f;
    public float ZoomInFactor { get; set; } = 0.7f;

    public event EventHandler? UpdateEvent;


    public void Update()
    {
        var cameraScale = (Vec2f)Camera.View.Size / (Vec2f)(Vec2u)Camera.Target.Size;

        if (Mouse.IsButtonPressed(Mouse.Button.Left))
            Camera.View.Move(-(Vec2f)MouseInput.PositionDelta * cameraScale);

        if (MouseInput.ScrollDelta != 0)
            Camera.View.Zoom(MouseInput.ScrollDelta < 0 ? ZoomOutFactor : ZoomInFactor);

        UpdateEvent?.Invoke(this, EventArgs.Empty);
    }
}
