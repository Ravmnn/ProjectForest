using SFML.Graphics;

using Latte.Core.Type;
using Latte.Rendering;
using Latte.Application;

using DotTiled;

using Milkway;
using Milkway.Tiles;


namespace ProjectForest;


public class Game : Section
{
    public static Vec2u Resolution => AspectRatio * 15;
    public static Vec2u AspectRatio => new Vec2u(16, 9);

    public static Vec2f Scale =>
        new Vec2f((float)App.Window.WindowRect.Size.X / Resolution.X, (float)App.Window.WindowRect.Size.Y / Resolution.Y);


    public PixelatedRenderer Renderer { get; private set; }
    public RenderTexture RenderTexture => Renderer.RenderTexture;

    public Camera Camera { get; private set; }


    public World World { get; private set; }


    public Game(Map map, TileSet tileSet)
    {
        Camera = new Camera(App.Window);
        Renderer = new PixelatedRenderer(new RenderTexture(Resolution.X, Resolution.Y));

        World = new World(Camera, map, tileSet);
    }




    public override void Initialize()
    {
        App.Renderer = Renderer;
        App.DrawEndedEvent += OnAppDrawEnd;

        base.Initialize();
    }


    public override void Deinitialize()
    {
        App.DrawEndedEvent -= OnAppDrawEnd;

        base.Deinitialize();
    }




    public override void Update()
    {
        Camera.Update();
        World.Update();


        Renderer.Scale = Scale;
        RenderTexture.SetView(App.Window.GetView());

        base.Update();
    }


    public override void Draw(IRenderer renderer)
    {
        RenderTexture.Clear();

        World.Draw(renderer);

        RenderTexture.Display();


        base.Draw(renderer);
    }




    private void OnAppDrawEnd(object? _, EventArgs __)
    {
        App.Window.Draw(Renderer.RenderTextureSprite);
    }
}
