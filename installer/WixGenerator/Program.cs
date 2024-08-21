// See https://aka.ms/new-console-template for more information
using Accolades.Brann.WixGenerator;
using System.Xml.Serialization;

Console.WriteLine("Hello, World!");

var root = new WixRoot("D:\\Sources\\Repos\\GitHub\\brann\\artifacts\\binaries", "..\\..\\artifacts\\binaries");
root.Fragment.ComponentGroup.Id = "CoreGenerated";

var rootDirRef = root.Fragment.DirectoryRef;

rootDirRef.Id = "INSTALLFOLDER";
rootDirRef.Name = null;
rootDirRef.FileSource = string.Format("$(var.{0})", WixRoot.SOURCE_DIRECTORY_VARIABLE);
rootDirRef.DiskId = "1";

root.Serialize("D:\\Sources\\Repos\\GitHub\\brann\\installer\\BrannSetup\\Generated.wxs");