using Microsoft.Extensions.DependencyInjection;
using TypeOfShape.Application.TypeOfShape.Triangle;

namespace TypeOfShape.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ITypeOfTriangleService, TypeOfTriangleService>();
    }
}