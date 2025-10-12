using SFML.Graphics;

using Latte.Core.Type;
using Latte.Rendering;
using Latte.Application;

using DotTiled;

using Milkway;
using Milkway.Tiles;


namespace ProjectForest.Game;




public class GameManager : Section
{
    public static Vec2u Resolution => AspectRatio * 20;
    public static Vec2u AspectRatio => new Vec2u(16, 9);

    public static Vec2f Scale =>
        new Vec2f((float)App.Window.WindowRect.Size.X / Resolution.X, (float)App.Window.WindowRect.Size.Y / Resolution.Y);




    public PixelatedRenderer Renderer { get; private set; }
    public RenderTexture RenderTexture => Renderer.RenderTexture;

    public GameCamera Camera { get; private set; }




    public World World { get; private set; }




    public GameManager(Map map, TileSet tileSet)
    {
        Renderer = new PixelatedRenderer(new RenderTexture(Resolution.X, Resolution.Y));
        Camera = new GameCamera(RenderTexture);

        World = new World(Camera, map, tileSet);

        Camera.Follow = World.Player;
        Camera.SoftFollowAmount = new Vec2f(5f, 5f);
    }




    public override void Initialize()
    {
        App.Renderer = Renderer;
        App.DrawEndedEvent += OnAppDrawEnd;
    }


    public override void Deinitialize()
    {
        App.DrawEndedEvent -= OnAppDrawEnd;
    }




    public override void Update()
    {
        Renderer.Scale = Scale;

        //RenderTexture.SetView(App.Window.GetView());


        Camera.Update();
        World.Update();

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
