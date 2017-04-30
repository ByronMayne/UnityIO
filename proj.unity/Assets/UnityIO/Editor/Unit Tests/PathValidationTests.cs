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
using UnityEngine;
using UnityIO;

public class PathValidationTests
{

    [Test]
    public void CorrectPath()
    {
        try
        {
            IO.ValidatePath("Assets/Folder");
        }
        catch
        {
            Assert.Fail("A valid path should not throw an exception");
        }
    }

    [Test]
    public void LeadingSlash()
    {
        Assert.Throws<System.IO.IOException>(() =>
       {
           IO.ValidatePath("Assets/Folder/");
       }, "This path ends with a leading forward slash and this should fail.");
    }


    [Test]
    public void IsUnityPath()
    {
        // Get our data path
        string path = Application.dataPath;
        // Add a fake folder
        path += "/Units";
        // Check if the path counts
        bool isUnityPath = IO.IsPathWithinUnityProject(path);
        // Make sure it's equal
        Assert.AreEqual(true, isUnityPath, "We sent in a path of '" + path + "' which should be marked as a Unity directory and it was not.");
    }

    [Test]
    public void IsNotUnityPath()
    {
        // Add a fake folder
        string path = "C://Users/Players/Units";
        // Check if the path counts
        bool isUnityPath = IO.IsPathWithinUnityProject(path);
        // Make sure it's equal
        Assert.AreEqual(false, isUnityPath, "We sent in a path of '" + path + "' which should be not be marked as a Unity directory and it was.");
    }
}
