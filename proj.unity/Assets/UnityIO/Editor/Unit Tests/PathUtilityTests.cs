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
using UnityEditor;
using UnityEngine;
using UnityIO;
using UnityIO.Classes;
using UnityIO.Interfaces;
using AssetDatabase = UnityEditor.AssetDatabase;


public class PathUtilityTests
{
    [Test]
    [TestCase("Dog", "Cat.png", Result = "Dog.png")]
    [TestCase("Dog", "Assets/Cat.png", Result = "Assets/Dog.png")]
    [TestCase("Dog", "Assets/Pets/Cat.png", Result = "Assets/Pets/Dog.png")]
    public string RenameFile(string newName, string path)
    {
        return PathUtility.Rename(path, newName);
    }

    [Test]
    [TestCase("Drinks", "Food", Result = "Drinks")]
    [TestCase("Drinks", "Party Things/Food", Result = "Party Things/Drinks")]
    [TestCase("Drinks", "Party Things/To Buy/Food", Result = "Party Things/To Buy/Drinks")]
    public string RenameDirectory(string newName, string path)
    {
        // Rename the file
        return PathUtility.Rename(path, newName);
    }

    [Test]
    [TestCase("Person.png", Result = "Person")]
    [TestCase("Assets/Person.png", Result = "Person")]
    [TestCase("Assets/Art/Person.png", Result = "Person")]
    [TestCase("Assets/Art.Final/Person.png", Result = "Person")]
    [TestCase("", ExpectedException = typeof(System.ArgumentNullException))]
    public string GetNameFile(string path)
    {
        return PathUtility.GetName(path); 
    }

    [Test]
    [TestCase("People", Result = "People")]
    [TestCase("Assets/People", Result = "People")]
    [TestCase("Assets/Art/People", Result = "People")]
    [TestCase("Assets/Art.Final/Person", Result = "Person")]
    [TestCase("", ExpectedException = typeof(System.ArgumentNullException))]
    public string GetNameDirectory(string path)
    {
        return PathUtility.GetName(path);
    }

    [Test]
    [TestCase("Projects/", "DD", "DD_01")]
    [TestCase("C:/Projects/proj/Assets/", "DD", "DD_01")]
    [TestCase("C:/Projects/proj.unity/Assets/", "DD", "DD_01")]
    public void RenamedStringLength(string directory, string oldName, string newName)
    {
        int expectedLength = directory.Length + newName.Length;
        string output = PathUtility.Rename(directory + oldName, newName);
        Assert.AreEqual(expectedLength, output.Length, "The string result was to long");
    }
}
