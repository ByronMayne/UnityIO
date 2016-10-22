using NUnit.Framework;
using UnityIO;

public class CreatingDirectory
{

    [Test]
    public void CreateRootLevelDirectory()
    {
        IO.Root.CreateDirectory("CreateRootLevelDirectory");
        Assert.True(IO.Root.DirectoryExists("CreateRootLevelDirectory"));
        IO.Root.DeleteSubDirectory("CreateRootLevelDirectory");
    }

    [Test]
    public void CreateNestedDirectoryOneStep()
    {
        IO.Root.CreateDirectory("CreateNestedDirectoryOneStep/Folder One");
        Assert.True(IO.Root.DirectoryExists("CreateNestedDirectoryOneStep/Folder One"));
        IO.Root.DeleteSubDirectory("CreateNestedDirectoryOneStep");
    }

    [Test]
    public void CreateNestedDirectoryMultiStep()
    {
        IO.Root.CreateDirectory("CreateNestedDirectoryMultiStep").CreateDirectory("Folder One").CreateDirectory("Folder Two");
        Assert.True(IO.Root.DirectoryExists("CreateNestedDirectoryMultiStep/Folder One"));
        Assert.True(IO.Root.DirectoryExists("CreateNestedDirectoryMultiStep/Folder One/Folder Two"));
        IO.Root.DeleteSubDirectory("CreateNestedDirectoryMultiStep");
    }
}
