using TypeOfShape.Core.Domain.Errors;
using TypeOfShape.Core.Domain.Exceptions;

namespace TypeOfShape.Core.Domain.Triangle;

public class Triangle : IShape<TriangleTypes>
{
    private readonly int[] _sides;

    private Triangle(int[] sides)
    {
        _sides = sides;
    }
    
    public static ErrorOr<Triangle> CreateFromSides(int[] sides)
    {
        if (sides.Length != 3)
        {
            return CommonErrors.ToFewSidesError;
        }
        
        if (sides.Any(s => s <= 0))
        {
            return CommonErrors.ZeroOrNegativeSideError;
        }
        
        return new Triangle(sides);
    }
    
    public new TriangleTypes GetType()
    {
        if (_sides[0] == _sides[1] && _sides[1] == _sides[2])
        {
            return TriangleTypes.Equilateral;
        }
        
        if (_sides[0] == _sides[1] || _sides[1] == _sides[2] || _sides[2] == _sides[0])
        {
            return TriangleTypes.Isosceles;
        }
        
        return TriangleTypes.Scalene;
    }
}

