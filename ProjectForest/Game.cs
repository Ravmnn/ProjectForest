using System.Reflection;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

using Latte.Core;
using Latte.Core.Type;
using Latte.Application;

using DotTiled.Serialization;

using Milkway;
using Milkway.Tiles;
using Milkway.Tiles.Tiled;


using Sprite = SFML.Graphics.Sprite;


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

    public CircleShape TestCircle { get; private set; }

    public Sprite Layer1 { get; private set; }
    public Sprite Layer2 { get; private set; }
    public Sprite Layer3 { get; private set; }
    public Sprite Layer4 { get; private set; }
    public Sprite Layer5 { get; private set; }
    public Sprite Layer6 { get; private set; }
    public Parallax Parallax { get; private set; }


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

        EmbeddedResourceLoader.ResourcesPath = "ProjectForest.Resources";
        EmbeddedResourceLoader.SourceAssembly = Assembly.GetExecutingAssembly();
    }


    private void SetupGame()
    {
        Camera = new Camera(App.Window);
        MouseScreenDragger = new MouseScreenDragger(Camera);

        TestCircle = new CircleShape(10f) { Position = new Vector2f(20, 20) };

        Layer1 = new Sprite(ColorTexture.FromColor(32, 32, Color.Red)) { Position = new Vec2f(100, 50) };
        Layer2 = new Sprite(ColorTexture.FromColor(32, 32, Color.Green)) { Position = new Vec2f(100, 50) };
        Layer3 = new Sprite(ColorTexture.FromColor(32, 32, Color.Blue)) { Position = new Vec2f(100, 50) };
        Layer4 = new Sprite(ColorTexture.FromColor(32, 32, Color.Cyan)) { Position = new Vec2f(100, 50) };
        Layer5 = new Sprite(ColorTexture.FromColor(32, 32, Color.Magenta)) { Position = new Vec2f(100, 50) };
        Layer6 = new Sprite(ColorTexture.FromColor(32, 32, Color.Yellow)) { Position = new Vec2f(100, 50) };

        Parallax = new Parallax(Camera);
        Parallax.Layers.Add(new ParallaxLayer(Layer1, 0f));
        Parallax.Layers.Add(new ParallaxLayer(Layer2, 0.5f));
        Parallax.Layers.Add(new ParallaxLayer(Layer3, 2f));
        Parallax.Layers.Add(new ParallaxLayer(Layer4, 3f));
        Parallax.Layers.Add(new ParallaxLayer(Layer5, 4f));
        Parallax.Layers.Add(new ParallaxLayer(Layer6, 8f));

        Renderer = new PixelatedRenderer(new RenderTexture(Resolution.X, Resolution.Y));


        // var loader = Loader.DefaultWith(resourceReader: new TiledEmbeddedResourceReader("Maps.Cave"));
        //
        // var map = loader.LoadMap("Cave.tmx");
        // var tileSet = new TileSet(EmbeddedResourceLoader.LoadImage("Sprites.Tilesets.GrayboxingTileset.png"), 8);
        //
        // // TODO: optimize tile rendering and updating cycle
        // World = new World(map, tileSet);
        //
        //
        // KeyboardInput.KeyReleasedEvent += (_, args) =>
        // {
        //     if (args.Scancode == Keyboard.Scancode.Left)
        //     {
        //         World.LoadRoomByIndex(World.CurrentRoomIndex - 1);
        //         Console.WriteLine(World.CurrentRoomIndex);
        //     }
        //
        //     if (args.Scancode == Keyboard.Scancode.Right)
        //     {
        //         World.LoadRoomByIndex(World.CurrentRoomIndex + 1);
        //         Console.WriteLine(World.CurrentRoomIndex);
        //     }
        // };
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


        Parallax.Update();
        Parallax.Active = !Mouse.IsButtonPressed(Mouse.Button.Right);

        Camera.Update();
        MouseScreenDragger.Update();


        Renderer.Scale = Scale;
        RenderTexture.SetView(App.Window.GetView());
    }


    public void Draw()
    {
        RenderTexture.Clear();
        Parallax.Draw(Renderer);
        App.Draw(Renderer);
        RenderTexture.Display();

        App.Window.Clear();
        App.Window.Draw(Renderer.RenderTextureSprite);
        App.Window.Display();
    }
}
