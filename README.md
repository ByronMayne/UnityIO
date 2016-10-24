 [![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://github.com/ByronMayne/UnityIO/blob/master/LICENSE)
# Unity IO

### Description
Unity IO is made to try to remove the pain of working with Unity's file system. For anyone who has done extended work has figured out Unity makes this a huge pain. All the functionaililty you need is spread across multiple classes including [FileUtil](https://docs.unity3d.com/ScriptReference/FileUtil.html), [AssetDatabase](https://docs.unity3d.com/ScriptReference/AssetDatabase.html), [Resources](https://docs.unity3d.com/ScriptReference/Resources.html), [File](https://msdn.microsoft.com/en-us/library/system.io.file(v=vs.110).aspx), [FileInfo](https://msdn.microsoft.com/en-us/library/system.io.fileinfo(v=vs.110).aspx), [Path](https://msdn.microsoft.com/en-us/library/system.io.path(v=vs.110).aspx), [Directory](https://msdn.microsoft.com/en-us/library/system.io.directory(v=vs.110).aspx), [Directory Info](https://msdn.microsoft.com/en-us/library/system.io.directoryinfo(v=vs.110).aspx), ect. 

### Goals

 * Simple to use API
 * Support Unity's meta files. 
 * Allow for complex chaining with conditionals execution. 
 * Allow of loading, creating, destroying of Unity Assets.



## Directory Basics
UnityIO works with the Unity asset path so all paths start from the ```Assets/``` folder. To start using UnityIO in your classes you must include the namespace ```UnityIO```

### Creating a Directory
``` csharp
/// <summary>
/// Creates a directory the root of our project
/// </summary>
public void CreatingRootDirectory()
{
	IO.Root.CreateDirectory("Favorite Animals");
}
```
```IO.Root``` is the ```/Assets``` folder at the root of your Unity project. In the example above we are asking UnityIO to create a new folder called Favorite Animals  at ```Assets/Favorite Animales```. 
``` csharp
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
```
This code is doing the same above but it will also create a subdirectory called Cats. When you want to create subdirectories you have a few ways to do it. It all really depends how you want to do it but they all have the same result. If the code were to run above only two folders would be created and all of the variables would point to the same one.

#### Notes
* The paths <b>do not end</b> with a leading slash ```/```. Unity has a habit of switching this up but UnityIO will throw a planned exception letting you know. 
* All directories are split using the ```/``` character. You can use the constant ```IO.PATH_SPLITTER```.


### Destroying a Directory
Well it turns out all our hard work we did was for nothing! Turns out you hate cats, don't worry I get it you are a monster. I will not judge lets destroy that directory.
```csharp
/// <summary>
/// Let's blow some things up
/// </summary>
public void Destroy()
{
    // Get our directory and nuke it
    IO.Root["Favorite Animals"].GetDirectory("Cats").Delete();

    // Delete our cats folder. 
    IO.Root.DeleteSubDirectory("Favorite Animals/Cats");
}
```
As with creating folder there are more than way to do things. The first example we are just finding our cats directory and tell it to kill itself with the ```Delete()``` function. The second way we are starting from the root and it will look for  the subdirectory at the path we sent in. The ```DeleteSubDirectory(string name)``` just calls ```Delete()``` on the folder behind the scenes. 

Take that cats!

You might be asking what happens if you tell it to delete a directory  that does not exist.

```scharp
/// <summary>
/// This should do nothing.
/// </summary>
public void DeleteSomethingNotReal()
{
    IO.Root["Favorite Animals"].GetDirectory("Dogs").Delete(); // Does not exist
}
```
In this case you well get an ```System.IO.DirectoryNotFoundException``` since you are trying to delete something that is not there. This can be super helpful but what if you only want to delete it if it does exist? 
```csharp
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
```
Above you can see we are checking to see if it exists before deleting it. This way we will not get an exception which is awesome. However this can become annoying to check every element has to be checked along the way. The other more usable way to do this would be to use a conditional. 
```scharp
/// <summary>
/// We should play it safe and make it easy
/// </summary>
public void EasyValidateBeforeDelete()
{
    IO.Root["Favorite Animals"].IfDirectoryExists("Dogs").Delete();
}
```
"Wait what does ```IfDirectoryExists()``` do?" Well I am glad you asked. One of the really cool features of UnityIO is conditionals. In this case if the directory exists the function ```Delete()``` will be called on the Dogs directory. If it does not exist the function will take no effect. 
```csharp
/// <summary>
/// Look at the length of that things!
/// </summary>
public void ILikeChains()
{
    IO.Root["Favorite Animals"].IfDirectoryExists("Dogs").IfDirectoryExists("With Four Legs").IfDirectoryExists("Who stink").Delete();
}
```
 In this case only the first folder exists so the first conditional will return a NullFile class which has all the same functions but none of them have any effect. So that way your code will execute only as far as it can get. You don't have to worry about null checking since the code can't break if a file is missing. You have the power to chain as many of these as you want. 
 ``` csharp
/// <summary>
/// Find, Create, and Destroy
/// </summary>
public void CreateAndDestroy()
{
    IO.Root["Favorite Animals"].IfDirectoryExists("Dogs").CreateDirectory("Delete Me").Delete(); 
}
 ```

#### Notes
* You can delete your whole project with ```IO.Root.Delete()```. I don't suggest you do that. 

### Renaming a Directory
Sometimes there are cases where you want to rename you directory. To do this is pretty simple.
```csharp
/// <summary>
/// Pick any name that is valid
/// </summary>
public void RenameDirectory()
{
    IO.Root["My Dir"].Rename("Super Dir");
}
```
This is all you have to do just make sure you don't include any ```/``` in your name as they are already preserved. If a directory already exists with that name this function will throw an ```DirectroyAlreadyExistsException()``` so make sure you check before you try to create one.

### Duplicate a Directory
As with moving duplicate is just as easy.
```CSHARP
public void DuplicateADirectory()
{
    var cheeseDrive = IO.Root["Types of Cheese"];
    // Creates a copy
    cheeseDrive.Duplicate();
    // Creates a copy with a set name
    cheeseDrive.Duplicate("Other Types of Cheese");
}
```
The ```cheeseDrive.Duplicate()``` function will create a new copy of the cheeseDrive and all it's contents. It will then rename that drive with a number on the end to make it unique. If you want more control you can just use the overloaded function to pick a valid name. If the name is already taken you well get an exception. 
## File Basics

### Deleting a File
//TODO: 

### Renaming a File
//TODO: 

### Moving a File
//TODO: 
