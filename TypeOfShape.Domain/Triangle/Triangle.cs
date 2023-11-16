using TypeOfShape.Domain.Errors;

namespace TypeOfShape.Domain.Triangle;

public class Triangle : IShape<TriangleTypes>
{
    private const float Tolerance = 0.00001f;
    private readonly float[] _sides;

    private Triangle(float[] sides)
    {
        _sides = sides;
    }

    public new TriangleTypes GetType()
    {
        if (Math.Abs(_sides[0] - _sides[1]) < Tolerance
            && Math.Abs(_sides[1] - _sides[2]) < Tolerance)
            return TriangleTypes.Equilateral;

        if (Math.Abs(_sides[0] - _sides[1]) < Tolerance
            || Math.Abs(_sides[1] - _sides[2]) < Tolerance
            || Math.Abs(_sides[2] - _sides[0]) < Tolerance)
            return TriangleTypes.Isosceles;

        return TriangleTypes.Scalene;
    }

    public static ErrorOr<Triangle> CreateFromSides(float[] sides)
    {
        if (sides.Length != 3) return CommonErrors.ToFewSidesError;

        if (sides.Any(s => s <= 0)) return CommonErrors.ZeroOrNegativeSideError;

        return new Triangle(sides);
    }
}