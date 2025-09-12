using SFML.Graphics;

using DotTiled;

using Milkway;
using Milkway.Tiles;


namespace ProjectForest;


public sealed class GameParallax : TileMapParallax
{
    public TileMapParallaxLayer Main => (Layers[1] as TileMapParallaxLayer)!;
    public TileMapParallaxLayer Background => (Layers[0] as TileMapParallaxLayer)!;


    public GameParallax(Camera camera, TileSet tileSet, Map map, IntRect? area = null)
        : base(camera, tileSet, map, area)
    {
        Main.Depth = 0;
        Main.RelativePriority = 1;

        Background.Depth = 0;
        Background.ExtraShade = 0.5f;
    }
}
