using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

[Serializable]
[XmlRoot("Directory")]
public class WixDir
{
    public WixDir(string folderName, WixComponentGroup mainComponent)
    {
        var dir = new DirectoryInfo(folderName);
        Name = dir.Name;
        Id = PrepareID("Dir_", Name);

        //получить файлы на этом уровне
        string[] files = Directory.GetFiles(folderName).Where(f => !f.Contains(".exe") && !f.Contains(".pdb")).ToArray();
        if (files != null && files.Length > 0)
        {
            WixComponent comp = new WixComponent(files, PrepareID("Comp_", Name));
            Component = comp;

            WixComponentRef compRef = new WixComponentRef();
            compRef.Id = comp.Id;

            mainComponent.Add(compRef);
        }

        var subDirs = dir.GetDirectories();
        if (subDirs != null && subDirs.Length > 0)
        {
            WixDir[] dirs = new WixDir[subDirs.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                dirs[i] = new WixDir(subDirs[i].FullName, mainComponent);
            }

            Directories = dirs;
        }
    }

    public static string PrepareID(string prefix, string name)
    {
        //34 символа на размер общего идентификатора
        int maxLen = 34;
        string uniqPart = System.Guid.NewGuid().ToString().ToUpper();
        uniqPart = uniqPart.Substring(uniqPart.Length - 12, 12);

        name = name.Replace("@", "");
        string prepName = name.Replace('.', '_').Replace('-', '_').Replace('{', '_').Replace('}', '_').Trim('"');
        string result = prefix + prepName + uniqPart;
        if (result.Length > 34)
        {
            int cutOff = result.Length - maxLen;//эту часть нужно отрезать
            string namePart = prefix + prepName;
            namePart = namePart.Substring(0, namePart.Length - cutOff);

            result = namePart + uniqPart;
        }

        return result;
    }

    public WixDir()
    {
    }


    [System.Xml.Serialization.XmlElementAttribute("Directory")]
    public WixDir[] Directories
    {
        get;
        set;
    }

    public WixComponent Component
    {
        get;
        set;
    }

    [XmlAttribute]
    public string Name
    {
        get;
        set;
    }

    [XmlAttribute]
    public string Id
    {
        get;
        set;
    }

    [XmlAttribute]
    public string FileSource
    {
        get;
        set;
    }

    [XmlAttribute]
    public string DiskId
    {
        get;
        set;
    }

}
