using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityIO;
using UnityIO.Interfaces;

public class GetFilesTests
{
    public const string UNIT_TEST_LOADING_TEST_ASSETS = "UnityIO/Editor/Unit Tests/Loading Assets";


    private IDirectory SetupTest()
    {
        // We can only test if our testing directory exists
        Assume.That(IO.Root.SubDirectoryExists(UNIT_TEST_LOADING_TEST_ASSETS), "The testing directory this test is looking for does not exists at path '" + UNIT_TEST_LOADING_TEST_ASSETS + "'.");
        // Get our loading area
        return IO.Root[UNIT_TEST_LOADING_TEST_ASSETS];
    }

    [Test(Description = "Check if you can find assets only at the top level in " + UNIT_TEST_LOADING_TEST_ASSETS)]
    public void GetTopLevelFiles()
    {
        // Setup our test. 
        var loadingDir = SetupTest();
        // Load all our assets
        var files = loadingDir.GetFiles();
        // Should only be root level which has a total of 3 files.
        Assert.AreEqual(files.Count, 3, "There should be 3 files in the root of the testing directory");

    }

    [Test(Description = "Check if you can find assets only at the top level with filter in " + UNIT_TEST_LOADING_TEST_ASSETS)]
    public void GetRecursiveFiles()
    {
        // Setup our test. 
        var loadingDir = SetupTest();
        // Get all
        var files = loadingDir.GetFiles(recursive: true);
        // Should be 10 assets
        Assert.AreEqual(files.Count, 10, "There should be 10 files in the testing directory");
    }

    [Test(Description = "Checks if you can verify if you can find only assets in the top level directory with a filter. In this case any file with the .anim extension in " + UNIT_TEST_LOADING_TEST_ASSETS)]
    public void GetTopLevelWithFilters()
    {
        // Setup our test. 
        var loadingDir = SetupTest();
        // We are going to try to only find files ending with .anim
        var files = loadingDir.GetFiles(filter:"*.anim");
        // There should be four. 
        Assert.AreEqual(files.Count, 1, "There should be 1 file at the root that ends with .anim in our testing directory");
    }

    [Test(Description = "Checks if you can verify if you can find all assets with a filter. In this case any file with the .anim extension in " + UNIT_TEST_LOADING_TEST_ASSETS)]
    public void GetRecursiveWithFilters()
    {
        // Setup our test. 
        var loadingDir = SetupTest();
        // We are going to try to only find files ending with .anim
        var files = loadingDir.GetFiles(filter: "*.anim", recursive:true);
        // There should be four. 
        Assert.AreEqual(files.Count, 4, "There should be 1 file at the root that ends with .anim in our testing directory");
    }
}
