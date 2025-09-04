using SFML.Window;
using SFML.Graphics;

using Latte.Core.Type;
using Latte.Application;


namespace ProjectForest;


public static class Game
{
    public static Vec2u Resolution => AspectRatio * 25;
    public static Vec2u AspectRatio => new Vec2u(16, 9);

    public static RenderTexture RenderTexture { get; private set; }
    public static Sprite RenderTextureSprite { get; private set; }

    public static Vec2f Scale =>
        new Vec2f((float)App.Window.WindowRect.Size.X / Resolution.X, (float)App.Window.WindowRect.Size.Y / Resolution.Y);


    static Game()
    {
        RenderTexture = new RenderTexture(Resolution.X, Resolution.Y);
        RenderTextureSprite = new Sprite(RenderTexture.Texture);
    }


    public static void Init()
    {
        App.Init(VideoMode.DesktopMode, "Project Forest", Latte.Core.EmbeddedResources.DefaultFont());
        App.Debugger!.EnableKeyShortcuts = true;
        App.ManualClearDisplayProcess = true;

        RenderTexture.SetView(App.MainView);
    }


    public static void Update()
    {
        // TODO: switching to fullscreen breaks the coordinate system a little bit

        RenderTextureSprite.Scale = Scale;

        App.Update();
    }


    public static void Draw()
    {
        RenderTexture.Clear();
        App.Draw(RenderTexture);
        RenderTexture.Display();

        App.Window.Clear();
        App.Window.Draw(RenderTextureSprite);
        App.Window.Display();
    }
}
