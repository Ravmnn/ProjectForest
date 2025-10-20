using SFML.Window;

using Latte.Core;
using Latte.Core.Objects;
using Latte.Core.Type;
using Latte.Rendering;
using Latte.Application;

using Milkway.Physics;
using Milkway.Tiles;

using DotTiled;


using Color = SFML.Graphics.Color;


namespace ProjectForest.Game;



// TODO: add in-game room loading, that is, the room is loaded based on the player's position
// TODO: add camera box limits, it shouldn't move outside the box.



public class GameManager : Section
{
    public GameCamera Camera { get; private set; }

    public World World { get; private set; }


    public GameObjectHandler GameObjectHandler { get; }




    public bool DebugShowStaticBodyBoxes { get; set; }




    public GameManager(Map map, TileSet tileSet)
    {
        Camera = new GameCamera();
        World = new World(Camera, map, tileSet, 0);

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


        if (KeyboardInput.ReleasedKeyCode == Keyboard.Scancode.Z)
            DebugShowStaticBodyBoxes = !DebugShowStaticBodyBoxes;


        base.Update();
    }




    public override void Draw(IRenderer renderer)
    {
        Camera.RenderTexture.Clear();
        World.Draw(renderer);

        DebugDrawCollisionBoxes();

        Camera.RenderTexture.Display();


        base.Draw(renderer);
    }


    private void DebugDrawCollisionBoxes()
    {
        if (!DebugShowStaticBodyBoxes)
            return;

        foreach (var tile in World.CurrentRoom!.Main.TileMap.Tiles)
            if (tile is IBoxBody { Static: true } body)
                Latte.Debugging.Draw.LineRect(App.Renderer, body.BoundingBox().ShrinkRect(1f), Color.Green);
    }




    private void OnAppDrawEnd(object? _, EventArgs __)
    {
        App.Window.Draw(Camera.RenderTextureSprite);
        DrawFPS();
    }


    private static void DrawFPS()
    {
        var fpsString = $"{((int)DeltaTime.FramesPerSecond).ToString()} FPS -- {DeltaTime.Seconds:F5} DeltaTime";
        Latte.Debugging.Draw.Text(App.Window.Renderer, new Vec2f(), fpsString, 30, Color.White);
    }
}
