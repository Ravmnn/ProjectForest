using System.Reflection;

using SFML.Window;
using SFML.Graphics;

using Latte.Core;
using Latte.Core.Type;
using Latte.Application;

using DotTiled.Serialization;

using Milkway;
using Milkway.Tiles;
using Milkway.Tiles.Tiled;


namespace ProjectForest;


public class Game
{
    private bool _initialized;


    public static Vec2u Resolution => AspectRatio * 15;
    public static Vec2u AspectRatio => new Vec2u(16, 9);

    public static Vec2f Scale =>
        new Vec2f((float)App.Window.WindowRect.Size.X / Resolution.X, (float)App.Window.WindowRect.Size.Y / Resolution.Y);

    public PixelatedRenderer Renderer { get; private set; }
    public RenderTexture RenderTexture => Renderer.RenderTexture;

    public Camera Camera { get; private set; }
    public MouseScreenDragger MouseScreenDragger { get; private set; }


    public World World { get; private set; }


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
        App.ManualObjectDraw = true;

        EmbeddedResourceLoader.ResourcesPath = "ProjectForest.Resources";
        EmbeddedResourceLoader.SourceAssembly = Assembly.GetExecutingAssembly();
    }


    private void SetupGame()
    {
        Camera = new Camera(App.Window);
        MouseScreenDragger = new MouseScreenDragger(Camera);

        Renderer = new PixelatedRenderer(new RenderTexture(Resolution.X, Resolution.Y));


        var loader = Loader.DefaultWith(resourceReader: new TiledEmbeddedResourceReader("Maps.Cave"));

        var map = loader.LoadMap("Cave.tmx");
        var tileSet = new TileSet(EmbeddedResourceLoader.LoadImage("Sprites.Tilesets.GrayboxingTileset.png"), 8);

        World = new World(Camera, map, tileSet);


        KeyboardInput.KeyReleasedEvent += (_, args) =>
        {
            if (args.Scancode == Keyboard.Scancode.Left)
            {
                World.LoadRoomByIndex(World.CurrentRoomIndex - 1);
                Console.WriteLine(World.CurrentRoomIndex);
            }

            if (args.Scancode == Keyboard.Scancode.Right)
            {
                World.LoadRoomByIndex(World.CurrentRoomIndex + 1);
                Console.WriteLine(World.CurrentRoomIndex);
            }
        };
    }


    private void Setup()
    {
        if (_initialized)
            return;

        _initialized = true;
    }


    public void Update()
    {
        App.Update();

        Setup();


        Camera.Update();
        MouseScreenDragger.Update();

        World.Update();


        Renderer.Scale = Scale;
        RenderTexture.SetView(App.Window.GetView());
    }


    public void Draw()
    {
        RenderTexture.Clear();
        App.Draw(Renderer);

        World.Draw(Renderer);

        RenderTexture.Display();


        App.Window.Clear();
        App.Window.Draw(Renderer.RenderTextureSprite);
        App.Window.Display();
    }
}
