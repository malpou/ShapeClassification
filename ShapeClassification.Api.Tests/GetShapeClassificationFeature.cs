using System.Globalization;
using System.Net.Http.Json;
using ShapeClassification.Contracts;
using ShapeClassification.Domain.Errors;
using ShapeClassification.Domain.Shapes.Errors;

namespace ShapeClassification.Api.Tests;

public class GetShapeClassificationFeature(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    private static string BasePath(string sides)
    {
        return $"shape?sides={sides}";
    }

    [Theory]
    [InlineData("1,1,1", "Equilateral")]
    [InlineData("1,2,2", "Isosceles")]
    [InlineData("2.2,2.5,1", "Scalene")]
    [InlineData("0.3,0.3,0.3", "Equilateral")]
    [InlineData("0.00000002,0.00000002,0.00000002", "Equilateral")]
    [InlineData("2000000,2000001,2000000", "Isosceles")]
    [InlineData("2000000,2000000.00000002,2000000", "Isosceles")]
    [InlineData("2000000,2000000.00000002,2000000.00000005", "Scalene")]
    [InlineData("2000000,2000000.00000002,2000001.00000005", "Scalene")]
    [InlineData("09.3, 7, 7", "Isosceles")]
    [InlineData("2.00, 2, 2", "Equilateral")]
    public async Task Given_valid_sides_return_type_of_triangle(string sides, string expectedType)
    {
        const string expectedShape = "Triangle";
        
        var content = await ExecuteTriangleRequest(sides);
        content.Should().NotBeNull();
        content?.Value?.Classification.Should().Be(expectedType);
        content?.Value?.Type.Should().Be(expectedShape);
        content?.Value?.Sides.Should().BeEquivalentTo(sides.Split(',').Select(s => double.Parse(s, CultureInfo.InvariantCulture)));
    }

    [Fact]
    public async Task Given_invalid_triangle_return_error()
    {
        var content = await ExecuteErrorRequest("1,1,3");
        content.Should().NotBeNull();
        content?.Error?.Code.Should().Be(TriangleErrors.InvalidTriangleError.Code);
        content?.Error?.Message.Should().Be(TriangleErrors.InvalidTriangleError.Description);
    }

    [Fact]
    public async Task Given_flat_triangle_return_flat_triangle_error()
    {
        var content = await ExecuteErrorRequest("1,1,2");
        content.Should().NotBeNull();
        content?.Error?.Code.Should().Be(TriangleErrors.FlatTriangleError.Code);
        content?.Error?.Message.Should().Be(TriangleErrors.FlatTriangleError.Description);
    }

    [Theory]
    [InlineData("-1,1,1")]
    [InlineData("1,0,1")]
    [InlineData("NaN,2,4.5")]
    [InlineData("Infinity,2,4.5")]
    public async Task Given_negative_and_zero_values_in_sides_return_zero_or_negative_error(string sides)
    {
        var content = await ExecuteErrorRequest(sides);
        content.Should().NotBeNull();
        content?.Error?.Code.Should().Be(CommonErrors.ZeroOrNegativeSideError.Code);
        content?.Error?.Message.Should().Be(CommonErrors.ZeroOrNegativeSideError.Description);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("1,1")]
    [InlineData("1,1,1,1,1")]
    public async Task Given_invalid_number_of_sides_return_wrong_amount_of_sides_error(string sides)
    {
        var content = await ExecuteErrorRequest(sides);
        content.Should().NotBeNull();
        content?.Error?.Code.Should().Be(CommonErrors.WrongAmountOfSides.Code);
        content?.Error?.Message.Should().Be(CommonErrors.WrongAmountOfSides.Description);
    }

    [Fact]
    public async Task Given_four_sides_return_quadrant_error()
    {
        var content = await ExecuteErrorRequest("1,1,1,1");
        content.Should().NotBeNull();
        content?.Error?.Code.Should().Be(QuadrantErrors.QuadrantNotSupported.Code);
        content?.Error?.Message.Should().Be(QuadrantErrors.QuadrantNotSupported.Description);
    }

    [Theory]
    [InlineData("1,1,1,")]
    [InlineData("NotNumber,1,1")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("0.3.4, 1.3, 1.4")]
    public async Task Given_invalid_sides_return_error(string sides)
    {
        var content = await ExecuteErrorRequest(sides);
        content.Should().NotBeNull();
        content?.Error?.Code.Should().Be("Api.IncorrectSidesFormat");
        content?.Error?.Message.Should().Be("Invalid format, correct format is: sides=2,2,3 or sides=2.5,1,2.3 etc.");
    }
    
    private async Task<BaseResponse<ShapeClassificationResponse>?> ExecuteTriangleRequest(string sides)
    {
        var response = await ExecuteRequest(sides);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        return await response.Content.ReadFromJsonAsync<BaseResponse<ShapeClassificationResponse>>();
    }
    
    private async Task<BaseResponse?> ExecuteErrorRequest(string sides)
    {
        var response = await ExecuteRequest(sides);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        return await response.Content.ReadFromJsonAsync<BaseResponse>();
    }

    private async Task<HttpResponseMessage> ExecuteRequest(string sides)
    {
        var response = await _client.GetAsync(BasePath(sides));
        response.Should().NotBeNull();
        return response;
    }
}