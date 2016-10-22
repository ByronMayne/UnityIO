using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityIO;
using Object = UnityEngine.Object;

public class ConditionalProgressTests
{
    [Test]
    public void ConditionFolderRoot()
    {
        // Only create Sub Directory if Conditional Progress exists.
        IO.Root.IfSubDirectoryExists("Conditional Progress").CreateDirectory("Sub Directory");
        // It should not exists
        Assert.False(IO.Root.SubDirectoryExists("Conditional Progress/Sub Directory"));
        // Then really create it
        IO.Root.CreateDirectory("Conditional Progress");
        // Then try conditional again
        IO.Root.IfSubDirectoryExists("Conditional Progress").CreateDirectory("Sub Directory");
        // It should not exists since we created the parent directory
        Assert.True(IO.Root.SubDirectoryExists("Conditional Progress/Sub Directory"));
        // Cleanup
        IO.Root.IfSubDirectoryExists("Conditional Progress").Delete();
    }

    [Test]
    public void EmptyCheck()
    {
        // Create a new directory
        IO.Root.CreateDirectory("If Empty");
        // Check if it's empty
        Assert.True(IO.Root["If Empty"].IsEmpty(assetsOnly: false), "This should empty");
        // Add a sub folder
        IO.Root["If Empty"].CreateDirectory("Sub Directory");
        // This should be false since we say we want to include sub folders.
        Assert.False(IO.Root["If Empty"].IsEmpty(assetsOnly: false), "This should have passed because we have one folder.");
        // This should be false because we only care about Assets. 
        Assert.False(IO.Root["If Empty"].IsEmpty(assetsOnly: true), "This should empty");
        // Cleanup
        IO.Root["If Empty"].Delete();
    }

    [Test]
    public void ConditionEmptyTest()
    {
        // Create a new directory
        IO.Root.CreateDirectory("Conditional Empty").CreateDirectory("Sub Directory");
        // Destroy it if it's empty (it's not).
        IO.Root["Conditional Empty"].IfEmpty(assetsOnly: false).Delete();
        // Check if it was deleted (it should not have been).
        Assert.True(IO.Root.SubDirectoryExists("Conditional Empty"), "The folder should not have been deleted");
        // Delete the next level instead this should work.
        IO.Root["Conditional Empty/Sub Directory"].IfEmpty(assetsOnly: false).Delete();
        // Check if it was deleted (it should have been).
        bool directroyStillExists = IO.Root.SubDirectoryExists("Conditional Empty/Sub Directory");
        // Clean up if the test failed
        IO.Root["Conditional Empty"].Delete();
        // Finish Test
        Assert.False(directroyStillExists);

    }
}
