using Microsoft.Extensions.DependencyInjection;

namespace ShapeClassification.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IShapeFactory, ShapeFactory>();
    }
}