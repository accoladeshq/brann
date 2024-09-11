namespace Accolades.Brann.Plugins.ChatGpt;

public class ChatGptPlugin : Plugin
{
    public ChatGptPlugin() : base("ChatGPT")
    {
    }

    /// <inheritdoc cref="Plugin.Search"/>
    public override Task<IEnumerable<ISuggestion>> Search(string search, CancellationToken cancellationToken)
    {
        if (!search.Contains("/ask"))
        {
            return Task.FromResult(Enumerable.Empty<ISuggestion>());
        }

        var suggestions = new List<ISuggestion> {new AskSuggestion() };
        return Task.FromResult(suggestions.AsEnumerable());
    }

    /// <inheritdoc cref="Plugin.Initialize"/>
    public override Task Initialize()
    {
        return Task.CompletedTask;
    }
}