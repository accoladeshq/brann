using System.Reactive.Concurrency;
using Accolades.Brann.Core;
using Accolades.Brann.Core.Internals;
using Accolades.Brann.Plugins;
using Accolades.Brann.Plugins.Windows;
using FluentAssertions;
using ReactiveUI;

namespace Accolades.Brann.Integration.Tests.Core;

[TestClass]
public class SuggestionProviderTests
{
    [TestMethod]
    public async Task TestMethod1()
    {
        // ARRANGE
        var plugins = new List<IPlugin> { new WindowsPlugin() };
        var sut = new SuggestionProvider(plugins);
        
        // ACT
        await sut.Initialize();
        await RxApp.TaskpoolScheduler.Sleep(TimeSpan.FromSeconds(0.5));
        
        // ASSERT
        sut.Suggestions.Should().HaveCount(1);
    }
}