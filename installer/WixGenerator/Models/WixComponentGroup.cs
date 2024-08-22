// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Accolades.Brann.WixGenerator;

public class WixComponentGroup
{
    /// <summary>
    /// Initialize a new <see cref="WixComponentGroup"/>
    /// </summary>
    /// <param name="id">The component group identifier.</param>
    public WixComponentGroup(string id)
    {
        _components = new List<WixComponentRef>();
        Id = id;
    }
    
    /// <summary>
    /// Gets the component group identifier.
    /// </summary>
    public string Id { get; }


    private readonly List<WixComponentRef> _components;
    /// <summary>
    /// Gets the components.
    /// </summary>
    public WixComponentRef[] Components => _components.ToArray();

    public void AddReference(string reference)
    {
        if (_components.SingleOrDefault(c => c.Id == reference) != null)
        {
            return;
        }
        
        _components.Add(new WixComponentRef(reference));
    }
}