using TypeOfShape.Core.Domain.Errors;
using TypeOfShape.Core.Domain.Triangle;

namespace TypeOfShape.Core.Tests.Domain;

public class TriangleSpecification
{
    [Fact]
    public void Constructor_should_create_triangle_when_valid_sides()
    {
        var sides = new[] {1, 1, 1};
        
        var triangle = Triangle.CreateFromSides(sides).Value;
        
        triangle.Should().BeOfType<Triangle>();
    }
    
    [Fact]
    public void Constructor_should_throw_exception_when_to_few_sides()
    {
        var sides = new[] {1, 1};
        
        var result = Triangle.CreateFromSides(sides);
        
        result.IsError.Should().BeTrue();
        result.Errors.First().Should().Be(CommonErrors.ToFewSidesError);
    }
    
    [Theory]
    [InlineData(new[] {0, 1, 1})]
    [InlineData(new[] {-1, 1, 1})]
    public void Constructor_should_throw_exception_when_zero_or_negative_side(int[] sides)
    {
        var result = Triangle.CreateFromSides(sides);
        
        result.IsError.Should().BeTrue();
        result.Errors.First().Should().Be(CommonErrors.ZeroOrNegativeSideError);
    }
    
    [Theory]
    [InlineData(new[] {1, 1, 1}, TriangleTypes.Equilateral)]
    [InlineData(new[] {2, 2, 3}, TriangleTypes.Isosceles)]
    [InlineData(new[] {3, 4, 5}, TriangleTypes.Scalene)]
    public void Get_type_should_return_triangle_type_when_valid_parameters(int[] sides, TriangleTypes expected)
    {
        var triangle = Triangle.CreateFromSides(sides).Value;
        
        var triangleType = triangle.GetType();
        
        triangleType.Should().Be(expected);
    }
}