using Accolades.Brann.Plugins;
using Accolades.Brann.Plugins.Windows;

namespace Accolades.Brann.Unit.Tests.Internals;

public class MockPlugin : Plugin
{
    public MockPlugin(string name) : base(name)
    {
    }

    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task<IEnumerable<ISuggestion>> Search(string search, CancellationToken cancellationToken)
    {
        var s = new List<ISuggestion>();

        for (var i = 0; i < 5; i++)
        {
            s.Add(new AppSuggestion(i.ToString()));
        }

        return Task.FromResult<IEnumerable<ISuggestion>>(s);
    }
}