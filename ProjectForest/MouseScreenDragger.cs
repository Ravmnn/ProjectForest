using SFML.Window;
using SFML.Graphics;

using Latte.Core;
using Latte.Application;


namespace ProjectForest;


public class MouseScreenDragger(View view) : IUpdateable
{
    public View View { get; set; } = view;
    public float ZoomOutFactor { get; set; } = 1.2f;
    public float ZoomInFactor { get; set; } = 0.7f;

    public event EventHandler? UpdateEvent;


    public void Update()
    {
        if (Mouse.IsButtonPressed(Mouse.Button.Left))
            View.Move(-MouseInput.PositionDelta);

        if (MouseInput.ScrollDelta != 0)
            View.Zoom(MouseInput.ScrollDelta < 0 ? ZoomOutFactor : ZoomInFactor);

        UpdateEvent?.Invoke(this, EventArgs.Empty);
    }
}
