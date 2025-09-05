using Latte.Core.Type;
using Latte.Application;


using Sprite = Milkway.Sprite;


namespace ProjectForest;


class Program
{
    private static void Main(string[] args)
    {
        var game = new Game();


        var tile = new Sprite(EmbeddedResources.LoadTextureFromSprites("Tiles.TestTile.png"));

        tile.UpdateEvent += (_, _) =>
            tile.Position = ((Vec2f)MouseInput.Position).ToHalfDecimal() - (Vec2f)tile.GetBounds().Size / 2f * Game.Scale;


        App.AddObject(tile);

        while (!App.ShouldQuit)
        {
            game.Update();
            game.Draw();
        }
    }
}
