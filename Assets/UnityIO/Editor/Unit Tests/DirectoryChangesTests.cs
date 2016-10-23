using NUnit.Framework;
using UnityEditor;
using UnityIO;
using UnityIO.Exceptions;

public class DirectoryChangesTests
{
    [Test]
    [Description("Tests to see if a a directory with one sub directory can be copied and get auto renamed")]
    public void DuplicateDirectory()
    {
        // Create our first folder
        var newFolder = IO.Root.CreateDirectory("DD");
        // Add a sub folder
        newFolder.CreateDirectory("Sub Directory");
        // duplicate our first one
        newFolder.Duplicate();
        // Make sure the original and it's sub directory are still in tack.
        Assert.True(IO.Root.SubDirectoryExists("DD"), "The original folder should be in the same place");
        // And it's sub folder
        Assert.True(IO.Root.SubDirectoryExists("DD/Sub Directory"), "The sub folder should be in the same place");
        // Make sure the original and it's sub directory are still in tack.
        Assert.True(IO.Root.SubDirectoryExists("DD 1"), "The original folder should be in the same place");
        // And it's sub folder
        Assert.True(IO.Root.SubDirectoryExists("DD 1/Sub Directory"), "The sub folder should be in the same place");
    }

    [Test]
    [Description("Tests to see if a a directory with one sub directory can be copied and get a new named assigned by the user")]
    public void DuplicateDirectoryWithName()
    {
        // Create our first folder
        var newFolder = IO.Root.CreateDirectory("DDWN");
        // Add a sub folder
        newFolder.CreateDirectory("Sub");
        // duplicate our first one
        newFolder.Duplicate(IO.Root.Path + "/New DDWN");
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
        var newFolder = IO.Root.CreateDirectory("RNT");
        // Create a sub
        newFolder.CreateDirectory("Sub");
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
        var rwc = IO.Root.CreateDirectory("RWC");
        // Create a second one
        var rwc2 = IO.Root.CreateDirectory("RWC2");
        // Rename the second one to cause an exception since that directory already exists.
        rwc2.Rename("RWC");
    }

    [Test]
    [Sequential]
    [Description("Checks to see if an exception is thrown when we try to rename a directory and the name has invalid characters.")]
    public void RenameWithInvalidName([Values("/", "\\", "<", ">", ":", "|", "\"")] string charactersToTest)
    {
        // The exception that was thrown.
        System.Exception thorwnException = null;
        // Create a working directory
        var rwc = IO.Root.CreateDirectory("RWIN");
        // Create a file to rename
        var newDir = rwc.CreateDirectory("Awesome");
        // Rename it with invalid characters.
        try
        {
            newDir.Rename(charactersToTest);
        }
        catch (System.Exception e)
        {
            thorwnException = e;
        }

        if (thorwnException is InvalidNameException)
        {
            Assert.Pass("The correct exception was thrown for the invalid character '" + charactersToTest + "'.");
        }
        else
        {
            if(thorwnException != null)
            {
                Assert.Fail("The expected exception was not captured for the invalid character '" + charactersToTest + "'. The Exception thrown was " + thorwnException.ToString());
            }
            else
            {
                Assert.Fail("No exception was thrown for invalid character '" + charactersToTest + "'");
            }
        }
    }

    [Test]
    [Sequential]
    [Description("Checks to see if an exception is thrown when we try to rename a directory and the name has invalid characters.")]
    public void MoveWithInvalidName([Values("/", "\\", "<", ">", ":", "|", "\"")] string charactersToTest)
    {
        // The exception that was thrown.
        System.Exception thorwnException = null;
        // Create a working directory
        var rwc = IO.Root.CreateDirectory("RWIN");
        // Create a file to rename
        var newDir = rwc.CreateDirectory("Awesome");
        // Rename it with invalid characters.
        try
        {
            newDir.Move(rwc.Path + "/" + charactersToTest);
        }
        catch (System.Exception e)
        {
            thorwnException = e;
        }

        if (thorwnException is InvalidNameException)
        {
            Assert.Pass("The correct exception was thrown for the invalid character '" + charactersToTest + "'.");
        }
        else
        {
            if (thorwnException != null)
            {
                Assert.Fail("The expected exception was not captured for the invalid character '" + charactersToTest + "'. The Exception thrown was " + thorwnException.ToString());
            }
            else
            {
                Assert.Fail("No exception was thrown for invalid character '" + charactersToTest + "'");
            }
        }
    }



    [TearDown]
    public void TearDown()
    {
        IO.Root.IfSubDirectoryExists("DD").Delete();
        IO.Root.IfSubDirectoryExists("DD 1").Delete();
        IO.Root.IfSubDirectoryExists("DDWN").Delete();
        IO.Root.IfSubDirectoryExists("New DDWN").Delete();
        IO.Root.IfSubDirectoryExists("RNT").Delete();
        IO.Root.IfSubDirectoryExists("RNT Renamed").Delete();
        IO.Root.IfSubDirectoryExists("RWC").Delete();
        IO.Root.IfSubDirectoryExists("RWC2").Delete();
        IO.Root.IfSubDirectoryExists("RWIN").Delete();
    }
}
