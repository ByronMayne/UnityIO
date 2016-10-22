using NUnit.Framework;
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
}
