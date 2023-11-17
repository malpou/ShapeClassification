using ShapeClassification.Domain;
using ShapeClassification.Domain.Errors;
using ShapeClassification.Domain.Shapes;
using ShapeClassification.Domain.Shapes.Errors;

namespace ShapeClassification.Application;

public class ShapeFactory : IShapeFactory
{
    public ErrorOr<IShape> CreateShape(double[] sides)
    {
        return sides.Length switch
        {
            3 => Triangle.CreateFromSides(sides),
            4 => QuadrantErrors.QuadrantNotSupported,
            _ => CommonErrors.WrongAmountOfSides
        };
    }
}