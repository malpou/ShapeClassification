using System.Net.Http.Json;
using TypeOfShape.Contracts;

namespace TypeOfShape.AcceptanceTests;

public class GetTriangleTypeFeature(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Theory]
    [InlineData(new[] {1, 1, 1}, "Equilateral")]
    [InlineData(new [] {1 ,1 ,2} ,"Isosceles")]
    [InlineData(new[] {1 ,2 ,3}, "Scalene")]
    public async Task Given_triangle_When_get_type_Then_return_type(int[] sides, string expectedType)
    {
        var response = await _client.GetAsync($"type/triangle?sides={string.Join(',', sides)}");
        
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadFromJsonAsync<BaseResponse<ShapeTypeResponse>>();

        content.Should().NotBeNull();
        content?.Error.Should().BeNull();
        content?.Value?.Type.Should().Be(expectedType);
    }
    
    [Theory]
    [InlineData(new[] {1, 1}, "ToFewSides")]
    [InlineData(new[] {0, 1, 1}, "ZeroOrNegativeSide")]
    [InlineData(new[] {-1, 1, 1}, "ZeroOrNegativeSide")]
    public async Task Given_invalid_triangle_When_get_type_Then_return_error(int[] sides, string expectedErrorCode)
    {
        var response = await _client.GetAsync($"type/triangle?sides={string.Join(',', sides)}");
        
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var content = await response.Content.ReadFromJsonAsync<BaseResponse<ShapeTypeResponse>>();

        content.Should().NotBeNull();
        content?.Error?.Code.Should().Be(expectedErrorCode);
        content?.Value.Should().BeNull();
    }
    
    
    
}