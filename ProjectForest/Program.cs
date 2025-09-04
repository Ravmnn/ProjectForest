using Latte.Core.Type;
using Latte.Application;
using Latte.UI.Elements;
using SFML.Graphics;
using Sprite = Milkway.Sprite;


namespace ProjectForest;


class Program
{
    private static void Main(string[] args)
    {
        Game.Init();


        var tile = new Sprite(EmbeddedResources.LoadTextureFromSprites("Tiles.TestTile.png"))
        {
            Scale = Game.Scale
        };

        tile.UpdateEvent += (_, _) => tile.Position = MouseInput.PositionInObjectView;


        App.AddObject(tile);

        while (!App.ShouldQuit)
        {
            Game.Update();
            Game.Draw();
        }
    }
}
