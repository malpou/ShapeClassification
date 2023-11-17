namespace ShapeClassification.Domain;

public interface IShape
{
    string GetClassification();
    string GetShapeType();
}