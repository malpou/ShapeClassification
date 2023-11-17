using ShapeClassification.Domain;

namespace ShapeClassification.Application;

public interface IShapeFactory
{
    ErrorOr<IShape> CreateShape(double[] sides);
}