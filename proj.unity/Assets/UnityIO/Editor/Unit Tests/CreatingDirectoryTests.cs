/*>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
UnityIO was released with an MIT License.
Arther: Byron Mayne
Twitter: @ByMayne


MIT License

Copyright(c) 2016 Byron Mayne

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>*/

// Disable unused warning
#pragma warning disable 0168
using NUnit.Framework;
using UnityIO;

public class CreatingDirectory : UnityIOTestBase
{
    [Test]
    [TestCase(true, TestName = "Unity")]
    [TestCase(false, TestName = "System")]
    public void CreateRootLevelDirectory(bool isUnity)
    {
        var root = GetRoot(isUnity);
        root.CreateSubDirectory("CreateRootLevelDirectory");
        Assert.True(root.SubDirectoryExists("CreateRootLevelDirectory"));
        root.DeleteSubDirectory("CreateRootLevelDirectory");
    }

    [Test]
    [TestCase(true, TestName = "Unity")]
    [TestCase(false, TestName = "System")]
    public void CreateNestedDirectoryOneStep(bool isUnity)
    {
        var root = GetRoot(isUnity);
        root.CreateSubDirectory("CreateNestedDirectoryOneStep/Folder One");
        Assert.True(root.SubDirectoryExists("CreateNestedDirectoryOneStep/Folder One"));
        root.DeleteSubDirectory("CreateNestedDirectoryOneStep");
    }

    [Test]
    [TestCase(true, TestName = "Unity")]
    [TestCase(false, TestName = "System")]
    public void CreateNestedDirectoryMultiStep(bool isUnity)
    {
        var root = GetRoot(isUnity);
        root.CreateSubDirectory("CreateNestedDirectoryMultiStep").CreateSubDirectory("Folder One").CreateSubDirectory("Folder Two");
        Assert.True(root.SubDirectoryExists("CreateNestedDirectoryMultiStep/Folder One"));
        Assert.True(root.SubDirectoryExists("CreateNestedDirectoryMultiStep/Folder One/Folder Two"));
        root.DeleteSubDirectory("CreateNestedDirectoryMultiStep");
    }

    [Test]
    [TestCase(true, TestName = "Unity")]
    [TestCase(false, TestName = "System")]
    public void CreateNestedPreExistingRoot(bool isUnity)
    {
        var root = GetRoot(isUnity);
        // Create the root by itself. 
        root.CreateSubDirectory("CreateNestedDirectoryMultiStep");
        // Create it again with a child 
        root.CreateSubDirectory("CreateNestedDirectoryMultiStep").CreateSubDirectory("MultiFolder_Temp");
        // Check if the child exists at the root
        bool directroyExistsInRoot = root.SubDirectoryExists("MultiFolder_Temp");
        // Clean up the root folder
        root["CreateNestedDirectoryMultiStep"].Delete();
        // If the test failed this folder will exist so we want to cleanup 
        root.IfSubDirectoryExists("CreateNestedDirectoryMultiStep").Delete();
        // Fail or pass the test.
        Assert.False(directroyExistsInRoot);
    }
}
