using Latte.Core.Type;


namespace ProjectForest;




public static class TexelMath
{
    public static Vec2f ToHalfDecimal(this Vec2f source)
        => new Vec2f(ToHalfDecimal(source.X), ToHalfDecimal(source.Y));


    public static float ToHalfDecimal(this float source)
    {
        var result = (float)(int)source;
        result += 0.5f;

        return result;
    }
}
