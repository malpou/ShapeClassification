using Microsoft.AspNetCore.Mvc;
using TypeOfShape.Application.TypeOfShape.Triangle;
using TypeOfShape.Contracts;

namespace TypeOfShape.Api.Endpoints.TypeOfShape;

public static class TypeOfTriangleEndpoint
{
    public static IResult Handle(
        [FromQuery] string sides,
        ITypeOfTriangleService typeOfTriangleService)
    {
        float[] sidesArray;

        try
        {
            sidesArray = sides.Split(',').Select(float.Parse).ToArray();
        }
        catch (Exception)
        {
            return Results.BadRequest(new BaseResponse(new Error("Api.InvalidSides",
                "Invalid sides values, correct format is: 1,2,3")));
        }

        var typeOfTriangle = typeOfTriangleService.Handle(sidesArray);

        if (!typeOfTriangle.IsError)
            return Results.Ok(
                new BaseResponse<TypeOfShapeResponse>(new TypeOfShapeResponse(typeOfTriangle.Value.ToString())));
        var error = typeOfTriangle.FirstError;

        return Results.BadRequest(new BaseResponse(new Error(error.Code, error.Description)));
    }
}