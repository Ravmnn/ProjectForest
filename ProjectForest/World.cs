using SFML.Graphics;

using DotTiled;

using Milkway.Tiles;


namespace ProjectForest;


public class World
{
    private const string RoomsLayerName = "Rooms";


    public Map Map { get; }
    public TileSet TileSet { get; }

    public TileMap? CurrentRoom { get; private set; }
    public uint CurrentRoomIndex { get; private set; }


    public World(Map map, TileSet tileSet, uint roomIndex = 0)
    {
        Map = map;
        TileSet = tileSet;

        CurrentRoom = null!;

        LoadRoomByIndex(roomIndex);
    }


    public void LoadRoomByIndex(uint index)
    {
        if (GetRoomsLayer() is not { } objectLayer || GetRoomByIndex(objectLayer, index) is not { } room)
            return;

        CurrentRoom?.RemoveTilesFromApp();

        var tileSize = (int)Map.TileWidth;
        var area = new IntRect((int)room.X / tileSize, (int)room.Y / tileSize, (int)room.Width / tileSize, (int)room.Height / tileSize);

        CurrentRoomIndex = index;
        CurrentRoom = new TileMap(TileSet, Map, area);
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
