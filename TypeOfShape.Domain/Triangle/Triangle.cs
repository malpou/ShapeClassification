using TypeOfShape.Domain.Errors;
using TypeOfShape.Domain.Triangle.Errors;

namespace TypeOfShape.Domain.Triangle;

public class Triangle : IShape<TriangleTypes>
{
    private readonly double[] _sides;

    private Triangle(double[] sides)
    {
        _sides = sides;
    }

    public new TriangleTypes GetType()
    {
        // a triangle is scalene if all sides are different
        if (_sides[0] < _sides[1] && _sides[1] < _sides[2])
        {
            return TriangleTypes.Scalene;
        }

        // a triangle is isosceles if two sides are equal
        if (!(_sides[0] < _sides[1] && _sides[0] > _sides[1]) && _sides[1] < _sides[2]
            || !(_sides[1] < _sides[2] && _sides[1] > _sides[2]) && _sides[0] < _sides[1]
            || !(_sides[0] < _sides[2] && _sides[0] > _sides[2]) && _sides[1] < _sides[2])
        {
            return TriangleTypes.Isosceles;
        }

        // a triangle is equilateral if all sides are equal
        return TriangleTypes.Equilateral;
    }

    public static ErrorOr<Triangle> CreateFromSides(double[] sides)
    {
        // a triangle is invalid if it does not have 3 sides
        if (sides.Length != 3)
        {
            return CommonErrors.ToFewSidesError;
        }

        // a triangle is invalid if any of the sides are less than or equal to zero
        if (sides.Any(s => s <= 0 || double.IsNaN(s) || double.IsInfinity(s)))
        {
            return CommonErrors.ZeroOrNegativeSideError;
        }

        var sortedSides = sides.OrderBy(s => s).ToArray();

        // a triangle is invalid if the sum of two sides is less than the third side
        if (sortedSides[0] + sortedSides[1] < sortedSides[2])
        {
            return TriangleErrors.InvalidTriangleError;
        }

        // a triangle is flat if the longest side is equal to the sum of the two remaining ones
        if (!(sortedSides[0] + sortedSides[1] > sortedSides[2]))
        {
            return TriangleErrors.FlatTriangleError;
        }
        
        return new Triangle(sortedSides);
    }
}