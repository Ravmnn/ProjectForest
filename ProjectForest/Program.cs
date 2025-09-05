using Latte.Application;
using Latte.Core.Type;
using Sprite = Milkway.Sprite;


namespace ProjectForest;


class Program
{
    private static void Main(string[] args)
    {
        var game = new Game();


        var tile = new Sprite(EmbeddedResources.LoadTextureFromSprites("Tiles.TestTile.png"));

        // +0.5f solves the bad pixel scalling problem... study more about it
        tile.UpdateEvent += (_, _) => tile.Position = (Vec2f)MouseInput.Position + new Vec2f(0, 0.5f);


        App.AddObject(tile);

        while (!App.ShouldQuit)
        {
            Console.WriteLine($"mouse position: {MouseInput.Position}");
            Console.WriteLine($"tile position: {tile.Position}");
            Console.WriteLine($"scale: {Game.Scale}");

            game.Update();
            game.Draw();

            Console.WriteLine();
        }
    }
}
