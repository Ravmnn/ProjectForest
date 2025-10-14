using SFML.Window;
using SFML.Graphics;

using Latte.Core;
using Latte.Core.Objects;
using Latte.Rendering;
using Latte.Application;

using Milkway.Physics;


namespace ProjectForest.Game;




public sealed class GameObjectHandler(GameCamera camera) : DefaultObjectHandler, IUpdateable
{
    public GameCamera Camera { get; } = camera;


    public bool DebugShowStaticBodyBoxes { get; set; }


    public event EventHandler? UpdateEvent;




    public void Update()
    {
        if (KeyboardInput.ReleasedKeyCode == Keyboard.Scancode.Z)
            DebugShowStaticBodyBoxes = !DebugShowStaticBodyBoxes;

        UpdateEvent?.Invoke(this, EventArgs.Empty);
    }




    public override void Draw(BaseObject @object, IRenderer renderer)
    {
        var bounds = @object.GetBounds();
        var viewport = Camera.GetBounds();

        if (!bounds.Intersects(viewport))
            return;

        base.Draw(@object, renderer);

        DrawDebugAnnotations(@object, renderer);
    }


    private void DrawDebugAnnotations(BaseObject @object, IRenderer renderer)
    {
        if (DebugShowStaticBodyBoxes && @object is IBoxBody { Static: true } body)
            Latte.Debugging.Draw.LineRect(renderer, body.BoundingBox().ShrinkRect(1f), Color.Green);
    }
}
