using SFML.Graphics;

using Latte.Core.Type;
using Latte.Application;


namespace ProjectForest;


public class PixelatedRenderer(RenderTexture renderTexture, Vec2f? scale = null) : DefaultRenderer(renderTexture)
{
    public RenderTexture RenderTexture => (RenderTarget as RenderTexture)!;
    public Vec2f Scale { get; set; } = scale ?? new Vec2f(1, 1);


    public override void Render(Drawable drawable)
    {
        var transformable = drawable as Transformable;
        var oldScale = transformable?.Scale;

        if (transformable is not null)
            transformable.Scale = Scale;

        RenderTexture.Draw(drawable);

        if (transformable is not null)
            transformable.Scale = oldScale!.Value;
    }
}
