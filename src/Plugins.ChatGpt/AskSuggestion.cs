namespace Accolades.Brann.Plugins.ChatGpt;

public class AskSuggestion : ISuggestion
{
    public SuggestionType Type => SuggestionType.Command;
    public string Name => "Ask ChatGPT";
    public bool IsEnabled => true;
    public Task Execute()
    {
        return Task.CompletedTask;
    }
}