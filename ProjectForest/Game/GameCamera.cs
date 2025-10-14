using SFML.Window;
using SFML.Graphics;

using Latte.Core.Type;
using Latte.Application;

using Milkway;


using SfSprite = SFML.Graphics.Sprite;


namespace ProjectForest.Game;




public class GameCamera : Camera
{
    public static Vec2u Resolution => AspectRatio * 20;
    public static Vec2u AspectRatio => new Vec2u(16, 9);

    public static Vec2f Scale =>
        new Vec2f((float)App.Window.WindowRect.Size.X / Resolution.X, (float)App.Window.WindowRect.Size.Y / Resolution.Y);




    public RenderTexture RenderTexture => (RenderTarget as RenderTexture)!;
    public SfSprite RenderTextureSprite { get; }




    public GameCamera() : base(new RenderTexture(Resolution.X, Resolution.Y))
    {
        RenderTextureSprite = new SfSprite(RenderTexture.Texture);
    }




    public override void Update()
    {
        RenderTextureSprite.Scale = Scale;


        if (KeyboardInput.PressedKeyCode == Keyboard.Scancode.NumpadPlus)
            Size -= AspectRatio * 2;

        if (KeyboardInput.PressedKeyCode == Keyboard.Scancode.NumpadMinus)
            Size += AspectRatio * 2;


        base.Update();
    }




    public override Texture GetContent()
        => RenderTexture.Texture;
}
