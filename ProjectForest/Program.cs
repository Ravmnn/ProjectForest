using Latte.Application;


namespace ProjectForest;


class Program
{
    private static void Main(string[] args)
    {
        var game = new Game();


        while (!App.ShouldQuit)
        {
            game.Update();
            game.Draw();
        }
    }
}
