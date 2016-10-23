using NUnit.Framework;
using UnityEngine;
using UnityIO;

public class EnumerationTests
{

    [Test]
    public void LoopOver()
    {
        foreach(var directory in IO.Root)
        {
            Debug.Log(directory.Path);
        }
    }
}
