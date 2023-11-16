using TypeOfShape.Domain.Errors;
using TypeOfShape.Domain.Triangle.Errors;

namespace TypeOfShape.Domain.Triangle;

public class Triangle : IShape<TriangleTypes>
{
    private readonly double[] _sides;
    private readonly double _tolerance;

    private Triangle(double[] sides)
    {
        _sides = sides;
        _tolerance = CalculateTolerance(_sides);
    }

    public new TriangleTypes GetType()
    {
        if (Math.Abs(_sides[0] - _sides[1]) < _tolerance
            && Math.Abs(_sides[1] - _sides[2]) < _tolerance)
            return TriangleTypes.Equilateral;

        if (Math.Abs(_sides[0] - _sides[1]) < _tolerance
            || Math.Abs(_sides[1] - _sides[2]) < _tolerance
            || Math.Abs(_sides[2] - _sides[0]) < _tolerance)
            return TriangleTypes.Isosceles;

        return TriangleTypes.Scalene;
    }

    public static ErrorOr<Triangle> CreateFromSides(double[] sides)
    {
        if (sides.Length != 3) return CommonErrors.ToFewSidesError;

        if (sides.Any(s => s <= 0)) return CommonErrors.ZeroOrNegativeSideError;

        var sortedSides = sides.OrderBy(s => s).ToArray();

        if (sortedSides[0] + sortedSides[1] < sortedSides[2]) return TriangleErrors.InvalidTriangleError;

        var tolerance = CalculateTolerance(sortedSides);

        if (Math.Abs(sortedSides[0] + sortedSides[1] - sortedSides[2]) < tolerance)
            return TriangleErrors.FlatTriangleError;

        return new Triangle(sortedSides);
    }

    private static double CalculateTolerance(IReadOnlyList<double> sides)
    {
        const double tolerance = 0.001;

        var min = sides.Min();
        var middle = sides[1];
        var max = sides.Max();

        if (min < tolerance) return tolerance * min;

        if (max - min < tolerance && max > min) return tolerance * (max - min);
        if (middle - min < tolerance && middle > min) return tolerance * (middle - min);
        if (max - middle < tolerance && max > middle) return tolerance * (max - middle);

        return tolerance;
    }
}