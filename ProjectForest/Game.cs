using System.Diagnostics;
using System.Reflection;

using SFML.Window;
using SFML.Graphics;

using Latte.Core;
using Latte.Core.Type;
using Latte.Application;

using DotTiled.Serialization;

using Milkway.Tiles;
using Milkway.Tiles.Tiled;


using Tile = Milkway.Tiles.Tile;


namespace ProjectForest;


public class Game
{
    private bool _initialized;


    public static Vec2u Resolution => AspectRatio * 40;
    public static Vec2u AspectRatio => new Vec2u(16, 9);

    public static Vec2f Scale =>
        new Vec2f((float)App.Window.WindowRect.Size.X / Resolution.X, (float)App.Window.WindowRect.Size.Y / Resolution.Y);

    public PixelatedRenderer Renderer { get; private set; }
    public RenderTexture RenderTexture => Renderer.RenderTexture;
    public Sprite RenderTextureSprite { get; private set; }
    public View RenderTextureView { get; private set; }

    public MouseScreenDragger MouseScreenDragger { get; private set; }

    public TileMap TileMap { get; private set; }


    public Game()
    {
        _initialized = false;

        SetupApplication();
        SetupGame();
    }


    private void SetupApplication()
    {
        App.Init(VideoMode.FullscreenModes[0], "Project Forest", null,
            Styles.Fullscreen, Latte.Application.Window.DefaultSettings with { AntialiasingLevel = 0 });

        App.Debugger!.EnableKeyShortcuts = true;
        App.ManualClearDisplayProcess = true;

        EmbeddedResourceLoader.ResourcesPath = "ProjectForest.Resources";
        EmbeddedResourceLoader.SourceAssembly = Assembly.GetExecutingAssembly();
    }


    private void SetupGame()
    {
        var loader = Loader.DefaultWith(resourceReader: new TiledEmbeddedResourceReader("Maps.Cave"));

        var map = loader.LoadMap("Test.tmx");
        var tileSet = new TileSet(EmbeddedResourceLoader.LoadImage("Sprites.Tilesets.GrayboxingTileset.png"), 8);

        var stopwatch = Stopwatch.StartNew();

        // TODO: optimize tile rendering and updating cycle
        TileMap = new TileMap(tileSet, map);

        stopwatch.Stop();

        Console.WriteLine($"Loading tiles took {stopwatch.ElapsedMilliseconds}ms");

        Renderer = new PixelatedRenderer(new RenderTexture(Resolution.X, Resolution.Y));
        RenderTextureSprite = new Sprite(RenderTexture.Texture);
        RenderTextureView = App.Window.GetView();

        MouseScreenDragger = new MouseScreenDragger(RenderTextureView);
    }


    private void Setup()
    {
        if (_initialized)
            return;

        App.AddObjects(TileMap.Tiles.Cast<Tile>());

        _initialized = true;
    }


    public void Update()
    {
        App.Update();

        Setup();

        MouseScreenDragger.Update();

        Renderer.Scale = Scale;
        RenderTextureSprite.Scale = Scale;

        RenderTexture.SetView(RenderTextureView);
    }


    public void Draw()
    {
        RenderTexture.Clear();
        App.Draw(Renderer);
        RenderTexture.Display();

        App.Window.Clear();
        App.Window.Draw(RenderTextureSprite);
        App.Window.Display();
    }
}
