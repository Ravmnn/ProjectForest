using Latte.Core.Objects;
using Latte.Rendering;


namespace ProjectForest.Game;




public sealed class GameObjectHandler(GameCamera camera) : DefaultObjectHandler
{
    public GameCamera Camera { get; } = camera;




    public override void Draw(BaseObject @object, IRenderer renderer)
    {
        var bounds = @object.GetBounds();
        var viewport = Camera.GetBounds();

        if (!bounds.Intersects(viewport))
            return;

        base.Draw(@object, renderer);
    }
}
