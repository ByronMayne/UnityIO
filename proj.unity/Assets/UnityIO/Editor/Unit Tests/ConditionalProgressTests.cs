﻿/*>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
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
using UnityEngine;
using UnityEditor;
using UnityIO;
using Object = UnityEngine.Object;

public class ConditionalProgressTests : UnityIOTestBase
{
    [Test]
    [TestCase(true, TestName = "Unity")]
    [TestCase(false, TestName = "System")]
    public void ConditionFolderRoot(bool isUnity)
    {
        var root = GetRoot(isUnity);
        // Only create Sub Directory if Conditional Progress exists.
        root.IfSubDirectoryExists("Conditional Progress").CreateSubDirectory("Sub Directory");
        // It should not exists
        Assert.False(root.SubDirectoryExists("Conditional Progress/Sub Directory"));
        // Then really create it
        root.CreateSubDirectory("Conditional Progress");
        // Then try conditional again
        root.IfSubDirectoryExists("Conditional Progress").CreateSubDirectory("Sub Directory");
        // It should not exists since we created the parent directory
        Assert.True(root.SubDirectoryExists("Conditional Progress/Sub Directory"));
        // Cleanup
        root.IfSubDirectoryExists("Conditional Progress").Delete();
    }

    [Test]
    [TestCase(true, TestName = "Unity")]
    [TestCase(false, TestName = "System")]
    public void EmptyCheck(bool isUnity)
    {
        var root = GetRoot(isUnity);
        Debug.Log("Path: " + root);
        // Create a new directory
        root.CreateSubDirectory("If Empty");
        // Check if it's empty
        Assert.True(root["If Empty"].IsEmpty(directoriesCount: true), "This should empty");
        // Add a sub folder
        root["If Empty"].CreateSubDirectory("Sub Directory");
        // This should be false since we say we want to include sub folders.
        Assert.False(root["If Empty"].IsEmpty(directoriesCount: true), "This should have passed because we have one folder.");
        // This should be false because we only care about Assets. 
        Assert.False(root["If Empty"].IsEmpty(directoriesCount: false), "This should empty");
        // Cleanup
        root["If Empty"].Delete();
    }

    [Test]
    [TestCase(true, TestName="Unity")]
    [TestCase(false, TestName="System")]
    public void ConditionEmptyTest(bool isUnity)
    {
        var root = GetRoot(isUnity);
        // Create a new directory
        root.CreateSubDirectory("Conditional Empty").CreateSubDirectory("Sub Directory");
        // Destroy it if it's empty (it's not).
        root["Conditional Empty"].IfEmpty(directoriesCount: true).Delete();
        // Check if it was deleted (it should not have been).
        Assert.True(root.SubDirectoryExists("Conditional Empty"), "The folder should not have been deleted");
        // Delete the next level instead this should work.
        root["Conditional Empty/Sub Directory"].IfEmpty(directoriesCount: true).Delete();
        // Check if it was deleted (it should have been).
        bool directroyStillExists = root.SubDirectoryExists("Conditional Empty/Sub Directory");
        // Clean up if the test failed
        root["Conditional Empty"].Delete();
        // Finish Test
        Assert.False(directroyStillExists);
    }
}
