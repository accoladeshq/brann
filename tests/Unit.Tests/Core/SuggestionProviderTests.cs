using System.ComponentModel;
using System.Reactive.Concurrency;
using Accolades.Brann.Core;
using Accolades.Brann.Core.Internals;
using Accolades.Brann.Plugins;
using Accolades.Brann.Unit.Tests.Internals;
using FluentAssertions;
using ReactiveUI;

namespace Accolades.Brann.Unit.Tests.Core;

[TestClass]
public class SuggestionProviderTests
{
    [TestMethod]
    public void Should_CreateProvider_When_Default()
    {
        // ARRANGE
        SuggestionProvider sut = new SuggestionProviderBuilder();

        // ASSERT
        sut.Should().NotBeNull();
        sut.SearchTerm.Should().BeEmpty();
        sut.Suggestions.Should().BeEmpty();
    }

    [TestMethod]
    public async Task Should_Initialize_When_FirstTime()
    {
        // ARRANGE
        SuggestionProvider sut = new SuggestionProviderBuilder();
        using var monitor = sut.Monitor();

        // ACT
        await sut.Initialize();

        // ASSERT
        monitor.Should()
            .Raise("PropertyChanged")
            .WithSender(sut)
            .WithArgs<PropertyChangedEventArgs>(a => a.PropertyName == "IsInitialized");
    }

    [TestMethod]
    public void Should_RaiseException_When_InitializeTwoTimes()
    {
        // ARRANGE
        SuggestionProvider sut = new SuggestionProviderBuilder();

        // ACT
        void InitializeFlow() => Task.WaitAll(sut.Initialize(), sut.Initialize());

        // ASSERT
        Assert.ThrowsException<InvalidOperationException>(InitializeFlow);
    }

    [TestMethod]
    public async Task Should_InitializedPlugins_When_Initialized()
    {
        // ARRANGE
        const string pluginName = "System";
        SuggestionProvider sut = new SuggestionProviderBuilder()
            .WithPlugin(p =>
                p.WithName(pluginName)
            );
        using var monitor = sut.Monitor();

        // ACT
        await sut.Initialize();

        // ASSERT
        monitor.Should().Raise("PluginInitialized")
            .WithArgs<PluginInitializedEventArgs>(p => p.Plugin.Name == pluginName);
    }

    [TestMethod]
    public async Task Should_HaveDefaultSuggestions_When_AtLeastOnePlugin()
    {
        // ARRANGE
        const string pluginName = "System";
        SuggestionProvider sut = new SuggestionProviderBuilder()
            .WithPlugin(p =>
                p.WithName(pluginName)
            );

        // ACT
        await sut.Initialize();
        await RxApp.TaskpoolScheduler.Sleep(TimeSpan.FromSeconds(0.5));
        sut.SearchTerm = "Brann";
        await RxApp.TaskpoolScheduler.Sleep(TimeSpan.FromSeconds(0.5));

        // ASSERT
        sut.Suggestions.Should().HaveCount(1);
        sut.Suggestions.Should().ContainSingle(s => s.Type == SuggestionType.Application);
    }
    
}