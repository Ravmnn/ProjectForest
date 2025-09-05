using SFML.Window;
using SFML.Graphics;

using Latte.Core.Type;
using Latte.Application;


namespace ProjectForest;


public class Game
{
    private bool _initialized;


    public static Vec2u Resolution => AspectRatio * 20;
    public static Vec2u AspectRatio => new Vec2u(16, 9);

    public static Vec2f Scale =>
        new Vec2f((float)App.Window.WindowRect.Size.X / Resolution.X, (float)App.Window.WindowRect.Size.Y / Resolution.Y);

    public PixelatedRenderer Renderer { get; private set; }
    public RenderTexture RenderTexture => Renderer.RenderTexture;
    public Sprite RenderTextureSprite { get; private set; }

    public CircleShape CircleShape { get; private set; }


    public Game()
    {
        _initialized = false;


        App.Init(VideoMode.FullscreenModes[0], "Project Forest", Latte.Core.EmbeddedResources.DefaultFont(),
            Styles.Fullscreen, Latte.Application.Window.DefaultSettings with { AntialiasingLevel = 0 });

        App.Debugger!.EnableKeyShortcuts = true;
        App.ManualClearDisplayProcess = true;
    }


    private void Setup()
    {
        if (_initialized)
            return;

        Renderer = new PixelatedRenderer(new RenderTexture(Resolution.X, Resolution.Y));
        RenderTextureSprite = new Sprite(RenderTexture.Texture);

        CircleShape = new CircleShape(8f);

        _initialized = true;
    }


    public void Update()
    {
        App.Update();

        Setup();

        // TODO: switching to fullscreen breaks the coordinate system a little bit
        Renderer.Scale = Scale;
        RenderTextureSprite.Scale = Scale;

        RenderTexture.SetView(App.Window.GetView());
    }


    public void Draw()
    {
        RenderTexture.Clear();
        App.Draw(Renderer);
        Renderer.Render(CircleShape);
        RenderTexture.Display();

        App.Window.Clear();
        App.Window.Draw(RenderTextureSprite);
        App.Window.Display();
    }
}
