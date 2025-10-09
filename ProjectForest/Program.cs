using System.Reflection;

using Latte.Core;
using Latte.Application;

using Milkway;


namespace ProjectForest;




class Program
{
    private static void Main(string[] args)
    {
        var settings = AppInitializationSettings.Default;
        var contextSettings = settings.ContextSettings with { AntialiasingLevel = 0 };

        settings = settings with { ContextSettings = contextSettings };


        Engine.InitFullScreen("Project Forest", settings);

        App.Debugger!.EnableKeyShortcuts = true;
        App.ManualClearDisplayProcess = false;
        App.ManualObjectDraw = false;

        EmbeddedResourceLoader.ResourcesPath = "ProjectForest.Resources";
        EmbeddedResourceLoader.SourceAssembly = Assembly.GetExecutingAssembly();


        App.Section = new MainMenu();


        while (!App.ShouldQuit)
        {
            App.Update();
            App.Draw();
        }


        App.Deinit();
    }
}
