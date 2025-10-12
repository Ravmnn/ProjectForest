using SFML.Graphics;

using Latte.Application;
using Latte.Core.Type;
using Milkway;


namespace ProjectForest.Game;




public class GameCamera(RenderTarget target) : Camera(target)
{
    protected override void UpdateFollowing()
    {
        base.UpdateFollowing();
    }
}
