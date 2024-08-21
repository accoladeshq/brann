using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

[Serializable]
public class WixFile
{
    public WixFile()
    {
    }

    public WixFile(string fileName)
    {
        Name = Path.GetFileName(fileName);
        Id = WixDir.PrepareID("File_", Name);
    }

    [XmlAttribute]
    public string Name { get; set; }

    [XmlAttribute]
    public string Id { get; set; }



}
