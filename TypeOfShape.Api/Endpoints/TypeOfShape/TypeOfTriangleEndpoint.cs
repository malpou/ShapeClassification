using System.Globalization;
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
        double[] sidesArray;

        try
        {
            sidesArray = sides.Split(',')
                .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                .ToArray();
        }
        catch (Exception)
        {
            return Results.BadRequest(new BaseResponse(new Error("Api.IncorrectSidesFormat",
                "Invalid format, correct format is: 2,2,3 or 2.5,1,2.3 etc.")));
        }

        var typeOfTriangle = typeOfTriangleService.Handle(sidesArray);

        if (!typeOfTriangle.IsError)
            return Results.Ok(
                new BaseResponse<TypeOfShapeResponse>(new TypeOfShapeResponse(typeOfTriangle.Value.ToString())));
        var error = typeOfTriangle.FirstError;

        return Results.BadRequest(new BaseResponse(new Error(error.Code, error.Description)));
    }
}