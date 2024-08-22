namespace Accolades.Brann.WixGenerator;

public class WixComponentRef
{
    /// <summary>
    /// Initialize a new <see cref="WixComponentRef"/>.
    /// </summary>
    /// <param name="id">The identifier.</param>
    public WixComponentRef(string id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Gets the component ref. identifier.
    /// </summary>
    public string Id { get; }
}