using SFML.System;
using SFML.Graphics;

using Latte.Core.Type;
using Latte.Rendering;


namespace ProjectForest;


public class PixelatedRenderer(RenderTexture renderTexture, Vec2f? scale = null) : TextureRenderer(renderTexture)
{
    private Vec2f _scale = scale ?? new Vec2f(1, 1);
    public Vec2f Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            RenderTextureSprite.Scale = _scale;
        }
    }


    public override void Render(Drawable drawable, Effect? drawableEffect = null)
    {
        var transformable = drawable as Transformable;
        var oldScale = transformable?.Scale;
        var oldPosition = transformable?.Position;

        if (transformable is not null)
        {
            var newScale = new Vector2f(transformable.Scale.X * Scale.X, transformable.Scale.Y * Scale.Y);

            transformable.Scale = newScale;
            transformable.Position = (Vec2f)transformable.Position * Scale;
        }

        base.Render(drawable, drawableEffect);

        if (transformable is not null)
        {
            transformable.Scale = oldScale!.Value;
            transformable.Position = oldPosition!.Value;
        }
    }
}
