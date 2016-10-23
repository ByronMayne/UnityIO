using NUnit.Framework;
using UnityIO;

public class CreatingDirectory
{

    [Test]
    public void CreateRootLevelDirectory()
    {
        IO.Root.CreateDirectory("CreateRootLevelDirectory");
        Assert.True(IO.Root.SubDirectoryExists("CreateRootLevelDirectory"));
        IO.Root.DeleteSubDirectory("CreateRootLevelDirectory");
    }

    [Test]
    public void CreateNestedDirectoryOneStep()
    {
        IO.Root.CreateDirectory("CreateNestedDirectoryOneStep/Folder One");
        Assert.True(IO.Root.SubDirectoryExists("CreateNestedDirectoryOneStep/Folder One"));
        IO.Root.DeleteSubDirectory("CreateNestedDirectoryOneStep");
    }

    [Test]
    public void CreateNestedDirectoryMultiStep()
    {
        IO.Root.CreateDirectory("CreateNestedDirectoryMultiStep").CreateDirectory("Folder One").CreateDirectory("Folder Two");
        Assert.True(IO.Root.SubDirectoryExists("CreateNestedDirectoryMultiStep/Folder One"));
        Assert.True(IO.Root.SubDirectoryExists("CreateNestedDirectoryMultiStep/Folder One/Folder Two"));
        IO.Root.DeleteSubDirectory("CreateNestedDirectoryMultiStep");
    }

    [Test]
    public void CreateNestedPreExistingRoot()
    {
        // Create the root by itself. 
        IO.Root.CreateDirectory("CreateNestedDirectoryMultiStep");
        // Create it again with a child 
        IO.Root.CreateDirectory("CreateNestedDirectoryMultiStep").CreateDirectory("MultiFolder_Temp");
        // Check if the child exists at the root
        bool directroyExistsInRoot = IO.Root.SubDirectoryExists("MultiFolder_Temp");
        // Clean up the root folder
        IO.Root["CreateNestedDirectoryMultiStep"].Delete();
        // If the test failed this folder will exist so we want to cleanup 
        IO.Root.IfSubDirectoryExists("CreateNestedDirectoryMultiStep").Delete();
        // Fail or pass the test.
        Assert.False(directroyExistsInRoot);
    }

}
