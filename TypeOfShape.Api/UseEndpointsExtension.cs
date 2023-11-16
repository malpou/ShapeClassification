using System.Reflection;
using TypeOfShape.Api.Endpoints;

namespace TypeOfShape.Api;

public static class UseEndpointsExtension
{
    public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
    {
        UseEndpoints(app, typeof(TMarker));
    }

    private static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (var endpointType in endpointTypes)
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                .Invoke(null, new object[] {app});
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
    {
        var endpointTypes = typeMarker.Assembly.DefinedTypes
            .Where(x => x is {IsAbstract: false, IsInterface: false} && typeof(IEndpoints).IsAssignableFrom(x));
        return endpointTypes;
    }
}