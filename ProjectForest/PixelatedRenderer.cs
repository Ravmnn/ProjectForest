using SFML.Graphics;

using Latte.Core.Type;
using Latte.Application;


namespace ProjectForest;


public class PixelatedRenderer : DefaultRenderer
{
    private Vec2f _scale;


    public RenderTexture RenderTexture => (RenderTarget as RenderTexture)!;
    public Sprite RenderTextureSprite { get; }

    public Vec2f Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            RenderTextureSprite.Scale = _scale;
        }
    }


    public PixelatedRenderer(RenderTexture renderTexture, Vec2f? scale = null) : base(renderTexture)
    {
        _scale = scale ?? new Vec2f(1, 1);
        RenderTextureSprite = new Sprite(RenderTexture.Texture);
    }


    public override void Render(Drawable drawable)
    {
        var transformable = drawable as Transformable;
        var oldScale = transformable?.Scale;
        var oldPosition = transformable?.Position;

        if (transformable is not null)
        {
            transformable.Scale = Scale;
            transformable.Position = (Vec2f)transformable.Position * Scale;
        }

        RenderTexture.Draw(drawable);

        if (transformable is not null)
        {
            transformable.Scale = oldScale!.Value;
            transformable.Position = oldPosition!.Value;
        }
    }
}
