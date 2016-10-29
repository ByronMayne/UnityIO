using NUnit.Framework;
using System;
using UnityEngine;
using UnityIO;
using UnityIO.Interfaces;

public class UnityIOTestBase
{
    public const string UNIT_TEST_LOADING_TEST_ASSETS = "UnityIO/Editor/Unit Tests/Loading Assets";

    /// <summary>
    /// Returns back an IDirectory for testing asset loading. 
    /// </summary>
    /// <returns></returns>
    public static IDirectory SetupAssetLoadingTest()
    {
        // We can only test if our testing directory exists
        Assume.That(IO.Root.SubDirectoryExists(UNIT_TEST_LOADING_TEST_ASSETS), "The testing directory this test is looking for does not exists at path '" + UNIT_TEST_LOADING_TEST_ASSETS + "'.");
        // Get our loading area
        return IO.Root[UNIT_TEST_LOADING_TEST_ASSETS];
    }

    /// <summary>
    /// Logs to the Unity Console.
    /// </summary>
    public void UnityLog(string log)
    {
        Debug.Log(log);
    }

    /// <summary>
    /// Logs to the test's console.
    /// </summary>
    public void TestLog(string log)
    {
        Console.WriteLine(log);
    }
}
