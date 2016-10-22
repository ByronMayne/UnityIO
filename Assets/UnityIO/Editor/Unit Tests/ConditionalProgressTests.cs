using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityIO;

public class ConditionalProgressTests
{
    [Test]
    public void ConditionFolderRoot()
    {
        // Only create Sub Directory if Conditional Progress exists.
        IO.Root.IfDirectoryExists("Conditional Progress").CreateDirectory("Sub Directory");
        // It should not exists
        Assert.False(IO.Root.DirectoryExists("Conditional Progress/Sub Directory"));
        // Then really create it
        IO.Root.CreateDirectory("Conditional Progress");
        // Then try conditional again
        IO.Root.IfDirectoryExists("Conditional Progress").CreateDirectory("Sub Directory");
        // It should not exists since we created the parent directory
        Assert.True(IO.Root.DirectoryExists("Conditional Progress/Sub Directory"));
    }

    [Test]
    public void ConditionFolderNested()
    {
        // Only create Sub Directory if Conditional Progress exists.
        IO.Root.IfDirectoryExists("Conditional Progress").CreateDirectory("Sub Directory");
        // It should not exists
        Assert.False(IO.Root.DirectoryExists("Conditional Progress/Sub Directory"));
        // Then really create it
        IO.Root.CreateDirectory("Conditional Progress");
        // Then try conditional again
        IO.Root.IfDirectoryExists("Conditional Progress").CreateDirectory("Sub Directory");
        // It should not exists since we created the parent directory
        Assert.True(IO.Root.DirectoryExists("Conditional Progress/Sub Directory"));
    }
}
