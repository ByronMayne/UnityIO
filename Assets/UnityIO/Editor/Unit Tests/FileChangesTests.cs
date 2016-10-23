using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityIO;
using UnityIO.Classes;
using UnityIO.Exceptions;
using UnityIO.Interfaces;

public class FileChangesTests
{
    private IDirectory m_WorkingDirectroy; 

    [SetUp]
    public void Init()
    {
        // Creating our working Directory
        m_WorkingDirectroy = IO.Root.CreateDirectory(GetType().Name);
        // Create a prefab to work with.
        PrefabUtility.CreatePrefab(m_WorkingDirectroy.Path + "/Cube.prefab", GameObject.CreatePrimitive(PrimitiveType.Cube));
        PrefabUtility.CreatePrefab(m_WorkingDirectroy.Path + "/Cylinder.prefab", GameObject.CreatePrimitive(PrimitiveType.Cylinder));
        PrefabUtility.CreatePrefab(m_WorkingDirectroy.Path + "/Plane.prefab", GameObject.CreatePrimitive(PrimitiveType.Plane));
    }

    [Test]
    [Description("Tests to see if we can duplicate a file")]
    public void DuplicateFile()
    {
        // Get our file
        IFile cube = m_WorkingDirectroy.GetFiles("*Cube*").FirstOrDefault();
        // Should not be null;
        Assert.IsNotInstanceOf<NullFile>(cube);
        // Duplicate our prefab
        var secondCube = cube.Duplicate();
        // Check if our first one still exists
        Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<GameObject>(cube.Path));
        // And our second one is alive.
        Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<GameObject>(secondCube.Path));
    }

    [Test]
    [Description("Tests to see if we can duplicate a file and give it a new name")]
    public void DuplicateFileWithNewName()
    {
        // Get our file
        IFile cube = m_WorkingDirectroy.GetFiles("*Cube*").FirstOrDefault();
        // Should not be null;
        Assert.IsNotInstanceOf<NullFile>(cube);
        // Duplicate our prefab
        var secondCube = cube.Duplicate("Super Box");
        // Check if our first one still exists
        Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<GameObject>(m_WorkingDirectroy.Path + "/Cube.prefab"));
        // And our second one is alive.
        Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<GameObject>(m_WorkingDirectroy.Path + "/Super Box.prefab"));
    }


    [Test]
    [Description("Tests to see if a file can be renamed.")]
    public void RenameFile()
    {
        // Get our file
        IFile cube = m_WorkingDirectroy.GetFiles("*Cylinder*").FirstOrDefault();
        // Should not be null;
        Assert.IsNotInstanceOf<NullFile>(cube);
        // Duplicate our prefab
        cube.Rename("Super Tube");
        // Check to make sure the original item does not exist
        Assert.IsNull(AssetDatabase.LoadAssetAtPath<GameObject>(m_WorkingDirectroy.Path + "/Cylinder.prefab"), "Our old prefab still exists");
        // Check if the rename happened.
        Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<GameObject>(m_WorkingDirectroy.Path + "/Super Tube.prefab"), "The renamed prefab could not be found");
    }


    [Test]
    [Description("Tests to see if a file can be moved.")]
    public void MoveFile()
    {
        // Create directory to move stuff into.
        var moveTo = m_WorkingDirectroy.CreateDirectory("MoveTo");
        // Get our file
        IFile cube = m_WorkingDirectroy.GetFiles("*Plane*").FirstOrDefault();
        // Should not be null;
        Assert.IsNotInstanceOf<NullFile>(cube);
        // Duplicate our prefab
        cube.Move(moveTo.Path);
        // Check to make sure the original item does not exist
        Assert.IsNull(AssetDatabase.LoadAssetAtPath<GameObject>(m_WorkingDirectroy.Path + "/Plane.prefab"), "Our old prefab still exists");
        // Check if the rename happened.
        Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<GameObject>(m_WorkingDirectroy.Path + "/MoveTo/Plane.prefab"), "The renamed prefab could not be found");
    }



    [TearDown]
    public void Dispose()
    {
        IO.Root.IfSubDirectoryExists(GetType().Name).Delete();
    }
}
