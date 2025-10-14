using SFML.Graphics;

using Latte.Core;
using Latte.Core.Objects;
using Latte.Core.Type;
using Latte.Rendering;

using Milkway;
using Milkway.Physics;
using Milkway.Tiles;

using DotTiled;


namespace ProjectForest.Game;




public class World : IUpdateable, IDrawable
{
    private const string RoomsLayerName = "Rooms";




    public Map Map { get; }
    public TileSet TileSet { get; }




    public Camera Camera { get; }


    public GameParallax? CurrentRoom { get; private set; }
    public uint CurrentRoomIndex { get; private set; }




    public PhysicsWorld Physics { get; private set; }

    public Player Player { get; private set; }


    // TODO: create player logic
    // TODO: create tile collision logic


    public event EventHandler? UpdateEvent;
    public event EventHandler? DrawEvent;




    public World(Camera camera, Map map, TileSet tileSet, uint roomIndex = 0)
    {
        Map = map;
        TileSet = tileSet;

        Camera = camera;
        CurrentRoom = null;


        Physics = new PhysicsWorld
        {
            Gravity = new Vec2f(0f, 0f),
            Drag = new Vec2f(4f, 4f)
        };

        Player = new Player(new Vec2f(10, 150));

        Physics.AddBody(Player);


        LoadRoomByIndex(roomIndex);
    }




    public void Update()
    {
        Physics.Update();
        Player.UpdateObject();
        CurrentRoom?.Update();


        UpdateEvent?.Invoke(this, EventArgs.Empty);
    }




    public void Draw(IRenderer renderer)
    {
        CurrentRoom?.Draw(renderer);
        Player.DrawObject(renderer);


        DrawEvent?.Invoke(this, EventArgs.Empty);
    }




    public void LoadRoomByIndex(uint index)
    {
        if (GetRoomsLayer() is not { } objectLayer || GetRoomByIndex(objectLayer, index) is not { } roomBounds)
            return;

        var tileSize = (int)Map.TileWidth;
        var roomArea = new IntRect((int)roomBounds.X / tileSize, (int)roomBounds.Y / tileSize, (int)roomBounds.Width / tileSize, (int)roomBounds.Height / tileSize);

        CurrentRoomIndex = index;
        CurrentRoom = new GameParallax(Camera, TileSet, Map, roomArea);
        CurrentRoom.Main.TileMap.AddStaticCollisionBodies(Physics, TileMapCollisionMethod.All);
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
