using ShapeClassification.Domain.Errors;
using ShapeClassification.Domain.Shapes;
using ShapeClassification.Domain.Shapes.Errors;

namespace ShapeClassification.Domain.Tests;

public class TriangleSpecification
{
    [Fact]
    public void Constructor_should_create_triangle_when_valid_sides()
    {
        var sides = new double[] {1, 1, 1};

        var triangle = Triangle.CreateFromSides(sides).Value;

        triangle.Should().BeOfType<Triangle>();
    }

    [Fact]
    public void Constructor_should_return_error_when_to_few_sides()
    {
        var sides = new double[] {1, 1};

        var result = Triangle.CreateFromSides(sides);

        result.IsError.Should().BeTrue();
        result.Errors.First().Should().Be(CommonErrors.WrongAmountOfSides);
    }

    [Theory]
    [InlineData(new double[] {0, 1, 1})]
    [InlineData(new double[] {-1, 1, 1})]
    public void Constructor_should_return_error_when_zero_or_negative_side(double[] sides)
    {
        var result = Triangle.CreateFromSides(sides);

        result.IsError.Should().BeTrue();
        result.Errors.First().Should().Be(CommonErrors.ZeroOrNegativeSideError);
    }

    [Fact]
    public void Constructor_should_return_error_when_invalid_triangle()
    {
        var result = Triangle.CreateFromSides(new double[] {1, 1, 10});

        result.IsError.Should().BeTrue();
        result.Errors.First().Should().Be(TriangleErrors.InvalidTriangleError);
    }

    [Fact]
    public void Constructor_should_return_error_when_flat_triangle()
    {
        var result = Triangle.CreateFromSides(new double[] {1, 1, 2});

        result.IsError.Should().BeTrue();
        result.Errors.First().Should().Be(TriangleErrors.FlatTriangleError);
    }

    [Theory]
    [InlineData(new double[] {1, 1, 1}, "Equilateral")]
    [InlineData(new double[] {2, 2, 3}, "Isosceles")]
    [InlineData(new double[] {3, 4, 5}, "Scalene")]
    public void Get_classification_should_return_triangle_type_when_valid_parameters(double[] sides, string expected)
    {
        var triangle = Triangle.CreateFromSides(sides).Value;

        var triangleType = triangle.GetClassification();

        triangleType.Should().Be(expected);
    }

    [Fact]
    public void Get_shape_type_should_return_triangle()
    {
        var triangle = Triangle.CreateFromSides(new double[] {1, 1, 1}).Value;
        
        var shapeType = triangle.GetShapeType();
        
        shapeType.Should().Be(Shapes.Shapes.Triangle.ToString());
    }
}