using System.Reflection;

using SFML.Graphics;

using Latte.Core;


namespace ProjectForest;


public static partial class EmbeddedResources
{
    public static Assembly GameAssembly => typeof(EmbeddedResources).Assembly;

    public static string GlobalNamespace => GameAssembly.GetName().Name!;
    public static string SpritePath => $"{GlobalNamespace}.Resources.Sprites";


    public static Texture LoadTextureFromSprites(string resourceName)
        => GameAssembly.LoadTexture($"{SpritePath}.{resourceName}");
}
