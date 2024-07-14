using Splat;

namespace Accolades.Brann.Avalonia;

internal static class LocatorExtensions
{
    /// <summary>
    /// Get a required service.
    /// </summary>
    /// <param name="dependencyResolver">The dependency resolver.</param>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">If the service is not required.</exception>
    public static T GetRequiredService<T>(this IReadonlyDependencyResolver dependencyResolver)
    {
        var service = dependencyResolver.GetService<T>();

        if (service is null)
        {
            throw new InvalidOperationException($"Service {typeof(T).Name} does not exists and is required by the application");
        }

        return service;
    }
}