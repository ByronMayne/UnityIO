using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityIO;


public class Documention
{
    /// <summary>
    /// Creates a directory the root of our project
    /// </summary>
    public void CreatingRootDirectory()
    {
        IO.Root.CreateDirectory("Favorite Animals");
    }

    /// <summary>
    /// Creates a nested directory in our project
    /// </summary>
    public void CreateNestedDirectory()
    {
        IO.Root.CreateDirectory("Favorite Animals/Cats");
    }

    /// <summary>
    /// A few more ways to create folders
    /// </summary>
    public void CreateNestedCatFolder()
    {
        // Create in a chain
        var catsFolder1 = IO.Root.CreateDirectory("Favorite Animals").CreateDirectory("Cats");

        // Break it down into two parts.
        var animals = IO.Root.CreateDirectory("Favorite Animals");
        var catsFolder2 = animals.CreateDirectory("Cats");

        // Do it in one step.
        var catsFolder3 = IO.Root.CreateDirectory("Favorite Animals/Cats");

        // Do it in one step with the helper
        var catsFolder4 = IO.Root.CreateDirectory("Favorite Animals" + IO.PATH_SPLITTER + "Cats");
    }

    /// <summary>
    /// Lets blow some things up
    /// </summary>
    public void Destroy()
    {
        // Get our directory and nuke it
        IO.Root["Favorite Animals"]["Cats"].Delete();

        // Delete our cats folder. 
        IO.Root.DeleteSubDirectory("Favorite Animals/Cats");
    }

    /// <summary>
    /// Lets see some explosions
    /// </summary>
    public void DeleteSomethingNotReal()
    {
        IO.Root["Favorite Animals"]["Dogs"].Delete(); // Does not exist
    }

    /// <summary>
    /// We should play it safe.
    /// </summary>
    public void ValidateBeforeDelete()
    {
        var favoriteAnimals = IO.Root["Favorite Animals"];

        if(favoriteAnimals.SubDirectoryExists("Dogs"))
        {
            favoriteAnimals.DeleteSubDirectory("Dogs");
        }
    }

    /// <summary>
    /// We should play it safe and make it easy
    /// </summary>
    public void EasyValidateBeforeDelete()
    {
        IO.Root["Favorite Animals"].IfSubDirectoryExists("Dogs").Delete();
    }

    /// <summary>
    /// Look at the length of that things!
    /// </summary>
    public void ILikeChains()
    {
        IO.Root["Favorite Animals"].IfSubDirectoryExists("Dogs").IfSubDirectoryExists("With Four Legs").IfSubDirectoryExists("Who stink").Delete();
    }

    /// <summary>
    /// Find, Create, and Destroy
    /// </summary>
    public void CreateAndDestroy()
    {
        IO.Root["Favorite Animals"].IfSubDirectoryExists("Dogs").CreateDirectory("Delete Me").Delete(); 
    }

}
