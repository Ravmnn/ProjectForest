using Latte.Core.Objects;
using Latte.Core.Type;
using Latte.Rendering;
using Latte.Application;

using Milkway.Tiles;

using DotTiled;


using Color = SFML.Graphics.Color;


namespace ProjectForest.Game;




public class GameManager : Section
{
    public GameCamera Camera { get; private set; }

    public World World { get; private set; }


    public GameObjectHandler GameObjectHandler { get; }




    public GameManager(Map map, TileSet tileSet)
    {
        Camera = new GameCamera();
        World = new World(Camera, map, tileSet);

        GameObjectHandler = new GameObjectHandler(Camera);


        Camera.Follow = World.Player;
        Camera.SoftFollowAmount = new Vec2f(5f, 5f);
    }




    public override void Initialize()
    {
        App.Renderer = Camera;
        App.DrawEndedEvent += OnAppDrawEnd;

        GlobalObjectHandler = GameObjectHandler;
    }


    public override void Deinitialize()
    {
        App.Renderer = App.Window.Renderer;
        App.DrawEndedEvent -= OnAppDrawEnd;

        GlobalObjectHandler = new DefaultObjectHandler();
    }




    public override void Update()
    {
        Camera.Update();
        World.Update();

        GameObjectHandler.Update();


        base.Update();
    }




    public override void Draw(IRenderer renderer)
    {
        Camera.RenderTexture.Clear();
        World.Draw(renderer);



        Camera.RenderTexture.Display();


        base.Draw(renderer);
    }




    private void OnAppDrawEnd(object? _, EventArgs __)
    {
        App.Window.Draw(Camera.RenderTextureSprite);
        DrawFPS();
    }


    private static void DrawFPS()
    {
        var fpsString = $"{((int)DeltaTime.FramesPerSecond).ToString()} FPS";
        Latte.Debugging.Draw.Text(App.Window.Renderer, new Vec2f(), fpsString, 30, Color.White);
    }
}
