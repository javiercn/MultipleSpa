using Yarp.ReverseProxy.Configuration;

internal static class YarpExtensions
{
    public static IReverseProxyBuilder AddSpas(this IReverseProxyBuilder builder, Span<Spa> apps)
    {
        var routeConfigs = new List<RouteConfig>();
        var clusterConfigs = new List<ClusterConfig>();

        foreach (var app in apps)
        {
            routeConfigs.Add(new RouteConfig
            {
                Match = new RouteMatch { Path = $"/{app.Name}/{{**catch-all}}" },
                RouteId = app.Name,
                ClusterId = app.Name,
            });

            clusterConfigs.Add(new ClusterConfig
            {
                ClusterId = app.Name,
                Destinations = new Dictionary<string, DestinationConfig>()
                {
                    { app.Name, new DestinationConfig { Address = app.Url } }
                }
            });
        }

        builder.LoadFromMemory(routeConfigs, clusterConfigs);

        return builder;
    }
}

internal record struct Spa(string Name, string Url);