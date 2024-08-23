namespace Accolades.Brann.Updater.GitHub.Exceptions;

public class UpdaterGitHubException : Exception
{
    /// <summary>
    /// Initialize a new <see cref="UpdaterGitHubException"/>
    /// </summary>
    /// <param name="message">The exception message.</param>
    public UpdaterGitHubException(string message): base(message)
    {
    }
}