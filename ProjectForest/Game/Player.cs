using SFML.Window;

using Latte.Core.Type;

using Milkway.Physics;


namespace ProjectForest.Game;




public class Player : RectangleBody
{
    private bool _canJump;


    public Vec2f Speed { get; set; }
    public float JumpForce { get; set; }




    public Player(Vec2f position) : base(position, new Vec2f(8, 8))
    {
        _canJump = true;

        Static = false;
        Speed = new Vec2f(100f, 100f);
        JumpForce = 160f;

        Color = SFML.Graphics.Color.Red;
    }




    public override void Update()
    {
        if (_canJump && Keyboard.IsKeyPressed(Keyboard.Key.Space))
        {
            Velocity.Y -= JumpForce;
            _canJump = false;
        }

        else if (!_canJump && !Keyboard.IsKeyPressed(Keyboard.Key.Space))
            _canJump = true;


        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            Velocity.X = -Speed.X;

        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            Velocity.X = Speed.X;


        base.Update();
    }
}
