using System.Text;
using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

[Serializable]
[XmlRoot("Wix", Namespace = "http://schemas.microsoft.com/wix/2006/wi")]
public class WixRoot
{
    public const string SOURCE_DIRECTORY_VARIABLE = "SourceDirectory";

    public WixRoot()
    {
    }

    public WixRoot(string folder, string[] includes)
    {
        WixFragment fr = new WixFragment();
        WixComponentGroup gr = new WixComponentGroup();

        fr.DirectoryRef = new WixDir(folder, gr);

        gr.PrepareToSerialization();
        fr.ComponentGroup = gr;

        Fragment = fr;

        foreach (var include in includes)
        {
            string publishDir = $"\"$(sys.CURRENTDIR){include}\"";
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            Instruction = doc.CreateProcessingInstruction("include", publishDir);
        }
    }

    [XmlAnyElement]
    public System.Xml.XmlProcessingInstruction Instruction
    {
        get;
        set;
    }

    public WixFragment Fragment
    {
        get;
        set;
    }

    static XmlSerializer _Serializer = new XmlSerializer(typeof(WixRoot));

    /// <summary>
    /// Важно! Если вы хотите использовать этот метод, обратить внимание на реализацию 
    /// см.перегрузку Serialize там UTF8Encoding, именно так вы должны сделать.
    /// </summary>
    /// <param name="destinationStream"></param>
    public void Serialize(System.IO.TextWriter destinationStream)
    {
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("", "http://schemas.microsoft.com/wix/2006/wi");

        _Serializer.Serialize(destinationStream, this, ns);
    }


    public void Serialize(string fileName)
    {
        using (var fs = new StreamWriter(fileName, false, new UTF8Encoding(true)))
        {
            Serialize(fs);
        }
    }


}
