using SFML.Window;

using Latte.Core.Type;

using Milkway.Physics;


namespace ProjectForest.Game;




public class Player : RectangleBody
{
    public Vec2f Speed { get; set; }




    public Player(Vec2f position) : base(position, new Vec2f(8, 8))
    {
        Static = false;
        Speed = new Vec2f(0.15f, 0.15f);

        Color = SFML.Graphics.Color.Red;
    }




    public override void Update()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            Acceleration.Y -= Speed.Y;

        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            Acceleration.Y += Speed.Y;

        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            Acceleration.X -= Speed.X;

        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            Acceleration.X += Speed.X;


        base.Update();
    }
}
