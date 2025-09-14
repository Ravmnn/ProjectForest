using SFML.Graphics;

using Latte.Core;

using DotTiled;

using Milkway;
using Milkway.Tiles;


namespace ProjectForest;


public class World : IUpdateable, IDrawable
{
    private const string RoomsLayerName = "Rooms";


    public Camera Camera { get; }

    public Map Map { get; }
    public TileSet TileSet { get; }

    public GameParallax? CurrentRoom { get; private set; }
    public uint CurrentRoomIndex { get; private set; }

    public event EventHandler? UpdateEvent;
    public event EventHandler? DrawEvent;


    public World(Camera camera, Map map, TileSet tileSet, uint roomIndex = 0)
    {
        Camera = camera;

        Map = map;
        TileSet = tileSet;

        CurrentRoom = null;

        LoadRoomByIndex(roomIndex);
    }


    public void Update()
    {
        CurrentRoom?.Update();

        UpdateEvent?.Invoke(this, EventArgs.Empty);
    }


    public void Draw(IRenderer target)
    {
        CurrentRoom?.Draw(target);

        DrawEvent?.Invoke(this, EventArgs.Empty);
    }


    public void LoadRoomByIndex(uint index)
    {
        if (GetRoomsLayer() is not { } objectLayer || GetRoomByIndex(objectLayer, index) is not { } roomBounds)
            return;

        CurrentRoom?.RemoveTilesFromApp();

        var tileSize = (int)Map.TileWidth;
        var roomArea = new IntRect((int)roomBounds.X / tileSize, (int)roomBounds.Y / tileSize, (int)roomBounds.Width / tileSize, (int)roomBounds.Height / tileSize);

        CurrentRoomIndex = index;
        CurrentRoom = new GameParallax(Camera, TileSet, Map, roomArea);

        CurrentRoom.AddTilesToApp();
    }


    private ObjectLayer? GetRoomsLayer()
    {
        foreach (var layer in Map.Layers)
            if (layer is ObjectLayer { Name: RoomsLayerName } objectLayer)
                return objectLayer;

        return null;
    }


    private static DotTiled.Object? GetRoomByIndex(ObjectLayer objectLayer, uint index)
    {
        foreach (var room in objectLayer.Objects)
            if (uint.Parse(room.Name) == index)
                return room;

        return null;
    }
}
