using Latte.Core.Type;
using Latte.UI;
using Latte.UI.Elements;
using Latte.Tweening;


namespace ProjectForest.UI;




public class MainMenuButtonWidget : ButtonElement
{
    private const double AnimationTime = 0.1;


    private FloatsTweenAnimation? _alphaAnimation;

    private readonly float _normalAlpha = 200f;
    private readonly float _hoverAlpha = 255f;



    private FloatsTweenAnimation? _displacementAnimation;

    private readonly Vec2f _normalDisplacement = new Vec2f();
    private readonly Vec2f _hoverDisplacement = new Vec2f(-10);
    private readonly Vec2f _pressDisplacement = new Vec2f(-6);




    public new TextElement Text => base.Text!;





    public MainMenuButtonWidget(Element? parent, Vec2f? position, string text)
        : base(parent, position, new Vec2f(), text)
    {
        Text.Color = new ColorRGBA(255, 255, 255, (byte)_normalAlpha);
        Text.Size = 35;
        Text.SizePolicy = SizePolicy.None;
        Text.ClipLayerIndexOffset = -1;

        Color = SFML.Graphics.Color.Transparent;
        BorderColor = SFML.Graphics.Color.Transparent;
        BorderSize = 0;
    }


    public override void Setup()
    {
        Text.UpdateSfmlProperties();
        Size = Text.GetBounds().Size;

        base.Setup();
    }


    public override void Update()
    {
        UpdateAnimations();

        base.Update();
    }


    private void UpdateAnimations()
    {
        _alphaAnimation?.Update();
        _displacementAnimation?.Update();

        if (_alphaAnimation is not null)
            Text.Color = Text.Color with { A = (byte)_alphaAnimation.CurrentValues[0] };

        if (_displacementAnimation is not null)
            Text.AlignmentMargin.ModifyFrom(_displacementAnimation.CurrentValues);
    }




    private void NormalAnimation()
    {
        _alphaAnimation = Tween.New(Text.Color.A, _normalAlpha, AnimationTime, Easing.EaseOutQuad);
        _displacementAnimation = Tween.New(Text.AlignmentMargin, _normalDisplacement, AnimationTime, Easing.EaseOutQuad);
    }

    private void HoverAnimation()
    {
        _alphaAnimation = Tween.New(Text.Color.A, _hoverAlpha, AnimationTime, Easing.EaseOutQuad);
        _displacementAnimation = Tween.New(Text.AlignmentMargin, _hoverDisplacement, AnimationTime, Easing.EaseOutQuad);
    }

    private void PressAnimation()
    {
        _displacementAnimation = Tween.New(Text.AlignmentMargin, _pressDisplacement, AnimationTime, Easing.EaseOutQuad);
    }


    public override void OnMouseEnter()
    {
        HoverAnimation();
        base.OnMouseHover();
    }


    public override void OnMouseLeave()
    {
        NormalAnimation();
        base.OnMouseLeave();
    }


    public override void OnMouseDown()
    {
        PressAnimation();
        base.OnMouseDown();
    }


    public override void OnMouseUp()
    {
        if (MouseState.IsMouseHover)
            HoverAnimation();
        else
            NormalAnimation();

        base.OnMouseUp();
    }
}
