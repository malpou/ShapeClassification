using ShapeClassification.Domain.Errors;
using ShapeClassification.Domain.Shapes.Classifications;
using ShapeClassification.Domain.Shapes.Errors;

namespace ShapeClassification.Domain.Shapes;

public class Triangle : IShape
{
    private readonly double[] _sides;

    private Triangle(double[] sides)
    {
        _sides = sides;
    }

    public string GetClassification()
    {
        // a triangle is scalene if all sides are different
        if (_sides[0] < _sides[1] && _sides[1] < _sides[2])
        {
            return TriangleClassifications.Scalene.ToString();
        }

        // a triangle is isosceles if two sides are equal
        if (!(_sides[0] < _sides[1] && _sides[0] > _sides[1]) && _sides[1] < _sides[2]
            || !(_sides[1] < _sides[2] && _sides[1] > _sides[2]) && _sides[0] < _sides[1]
            || !(_sides[0] < _sides[2] && _sides[0] > _sides[2]) && _sides[1] < _sides[2])
        {
            return TriangleClassifications.Isosceles.ToString();
        }

        // a triangle is equilateral if all sides are equal
        return TriangleClassifications.Equilateral.ToString();
    }
    
    public static ErrorOr<IShape> CreateFromSides(double[] sides)
    {
        // a triangle is invalid if it does not have 3 sides
        if (sides.Length != 3)
        {
            return CommonErrors.WrongAmountOfSides;
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
    
    public string GetShapeType() => Shapes.Triangle.ToString();
}