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

using NUnit.Framework;
using System;

public class GetFilesTests : UnityIOTestBase
{
    [TestCase("*", false, Result = 9)]
    [TestCase("*", true, Result = 23)]
    [TestCase("**.meta", false, Result = 6)]
    [TestCase("**.meta", true, Result = 13)]
    [TestCase(null, false, ExpectedException = typeof(ArgumentNullException), Result = 0)]
    [Test(Description = "Check if you can find assets only at the top level in " + UNIT_TEST_LOADING_TEST_ASSETS)]
    public int GetFiles(string filter, bool recursive)
    {
        // Setup our test. 
        var loadingDir = SetupAssetLoadingTest();
        // Load all our assets
        var files = loadingDir.GetFiles(filter, recursive);
        // Should only be root level which has a total of 3 files.
        return files.Count;
    }

    [Test(Description = "Test to make sure the meta information about the file is correct")]
    public void FileNameProperties()
    {
        // Setup our test. 
        var loadingDir = SetupAssetLoadingTest();
        // We are going to try to only find files ending with .anim
        var file = loadingDir.GetFiles(filter: "*.anim").FirstOrDefault();
        // Make sure it's the correct directory.
        Assert.AreEqual(loadingDir.path, file.directory.path, "The directory of the file does not match.");
        // Make sure it's the correct extension.
        Assert.AreEqual(".anim", file.extension, "The extension of this class should be '.anim'.");
        // Make sure it's the correct extension.
        Assert.AreEqual("Misc Animation", file.nameWithoutExtension, "The name of this class is not correct.");
    }
}
