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
using UnityIO.Exceptions;

public class DirectoryChangesTests
{
    [Test]
    [Description("Tests to see if a a directory with one sub directory can be copied and get auto renamed")]
    public void DuplicateDirectory()
    {
        // Create our first folder
        var newFolder = IO.Root.CreateSubDirectory("DD");
        // Add a sub folder
        newFolder.CreateSubDirectory("Sub Directory");
        // duplicate our first one
        newFolder.Duplicate();
        // Make sure the original and it's sub directory are still in tack.
        Assert.True(IO.Root.SubDirectoryExists("DD"), "The original folder should be in the same place");
        // And it's sub folder
        Assert.True(IO.Root.SubDirectoryExists("DD/Sub Directory"), "The sub folder should be in the same place");
        // Make sure the original and it's sub directory are still in tack.
        Assert.True(IO.Root.SubDirectoryExists("DD_01"), "The original folder should be in the same place");
        // And it's sub folder
        Assert.True(IO.Root.SubDirectoryExists("DD_01/Sub Directory"), "The sub folder should be in the same place");
    }

    [Test]
    [Description("Tests to see if a a directory with one sub directory can be copied and get a new named assigned by the user")]
    public void DuplicateDirectoryWithName()
    {
        // Create our first folder
        var newFolder = IO.Root.CreateSubDirectory("DDWN");
        // Add a sub folder
        newFolder.CreateSubDirectory("Sub");
        // duplicate our first one
        newFolder.Duplicate("New DDWN");
        // Make sure the original and it's sub directory are still in tack.
        Assert.True(IO.Root.SubDirectoryExists("DDWN"), "The original folder should be in the same place");
        // And it's sub folder
        Assert.True(IO.Root.SubDirectoryExists("DDWN/Sub"), "The sub folder should be in the same place");
        // Make sure the original and it's sub directory are still in tack.
        Assert.True(IO.Root.SubDirectoryExists("New DDWN"), "The original folder should be in the same place");
        // And it's sub folder
        Assert.True(IO.Root.SubDirectoryExists("New DDWN/Sub"), "The sub folder should be in the same place");
    }

    [Test]
    [Description("Checks to see if a directory can be renamed")]
    public void Rename()
    {
        // Create a folder
        var newFolder = IO.Root.CreateSubDirectory("RNT");
        // Create a sub
        newFolder.CreateSubDirectory("Sub");
        // Rename it
        newFolder.Rename("RNT Renamed");
        // Old one should not exist
        Assert.False(IO.Root.SubDirectoryExists("RNT"), "The original folder should not exist anymore");
        // Check if the rename is there
        Assert.True(IO.Root.SubDirectoryExists("RNT Renamed"), "The original folder should not exist anymore");
    }

    [Test]
    [ExpectedException(ExpectedException = typeof(DirectroyAlreadyExistsException))]
    [Description("Checks to see if an exception is thrown when we try to rename a directory and one already exists with that name")]
    public void RenameWithConflict()
    {
        // Create a folder
        var rwc = IO.Root.CreateSubDirectory("RWC");
        // Create a second one
        var rwc2 = IO.Root.CreateSubDirectory("RWC2");
        // Rename the second one to cause an exception since that directory already exists.
        rwc2.Rename("RWC");
    }

    [Test]
    [Sequential]
    [ExpectedException(typeof(MoveException))]
    [Description("Checks to see if an exception is thrown when we try to rename a directory and the name has invalid characters.")]
    public void RenameWithInvalidName([Values("/", "\\", "<", ">", ":", "|", "\"")] string charactersToTest)
    {
        // Create a working directory
        var rwc = IO.Root.CreateSubDirectory("RWIN");
        // Create a file to rename
        var newDir = rwc.CreateSubDirectory("Awesome");
        // Rename it with invalid characters.
        newDir.Rename(charactersToTest);
    }

    [Test]
    [Sequential]
    [ExpectedException(typeof(MoveException))]
    [Description("Checks to see if an exception is thrown when we try to rename a directory and the name has invalid characters.")]
    public void MoveWithInvalidName([Values("\\", "<", ">", ":", "|", "\"")] string charactersToTest)
    {
        // Create a working directory
        var rwc = IO.Root.CreateSubDirectory("RWIN");
        // Create a file to rename
        var newDir = rwc.CreateSubDirectory("Awesome");
        // Rename it with invalid characters.
        newDir.Move(rwc.path + "/" + charactersToTest);
    }


    [TearDown]
    public void TearDown()
    {
        IO.Root.IfSubDirectoryExists("DD").Delete();
        IO.Root.IfSubDirectoryExists("DD_01").Delete();
        //IO.Root.IfSubDirectoryExists("DDWN").Delete();
        //IO.Root.IfSubDirectoryExists("New DDWN").Delete();
        IO.Root.IfSubDirectoryExists("RNT").Delete();
        IO.Root.IfSubDirectoryExists("RNT Renamed").Delete();
        IO.Root.IfSubDirectoryExists("RWC").Delete();
        IO.Root.IfSubDirectoryExists("RWC2").Delete();
        IO.Root.IfSubDirectoryExists("RWIN").Delete();
    }
}
