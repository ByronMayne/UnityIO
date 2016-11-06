[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://github.com/ByronMayne/UnityIO/blob/master/LICENSE)
![Mac](https://img.shields.io/badge/Tested-Mac-green.svg)
![Windows](https://img.shields.io/badge/Tested-Windows-green.svg)

# Unity IO

### Description
Unity IO is made to try to remove the pain of working with Unity's file system. For anyone who has done extended work has figured out Unity makes this a huge pain. All the functionality you need is spread across multiple classes including [FileUtil](https://docs.unity3d.com/ScriptReference/FileUtil.html), [AssetDatabase](https://docs.unity3d.com/ScriptReference/AssetDatabase.html), [Resources](https://docs.unity3d.com/ScriptReference/Resources.html), [File](https://msdn.microsoft.com/en-us/library/system.io.file(v=vs.110).aspx), [FileInfo](https://msdn.microsoft.com/en-us/library/system.io.fileinfo(v=vs.110).aspx), [Path](https://msdn.microsoft.com/en-us/library/system.io.path(v=vs.110).aspx), [Directory](https://msdn.microsoft.com/en-us/library/system.io.directory(v=vs.110).aspx), [Directory Info](https://msdn.microsoft.com/en-us/library/system.io.directoryinfo(v=vs.110).aspx), ect. 

### Goals

 * Simple to use API.
 * Support Unity's meta files.
 * Allow for complex chaining with conditional execution.
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
```IO.Root``` is the ```/Assets``` folder at the root of your Unity project. In the example above we are asking UnityIO to create a new folder called Favorite Animals at ```Assets/Favorite Animals```. 
``` csharp
/// <summary>
/// A few more ways to create folders
/// </summary>
public void CreateNestedCatFolder()
{
    // Create in a chain.
    var catsFolder1 = IO.Root.CreateDirectory("Favorite Animals").CreateDirectory("Cats");
    
    // Break it down into two parts.
    var animals = IO.Root.CreateDirectory("Favorite Animals");
    var catsFolder2 = animals.CreateDirectory("Cats");
    
    // Do it in one step.
    var catsFolder3 = IO.Root.CreateDirectory("Favorite Animals/Cats");
    
    // Do it in one step with the helper.
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

You might be asking what happens if you tell it to delete a directory that does not exist.

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
This is all you have to do just make sure you don't include any ```/``` in your name as they are already preserved. If a directory already exists with that name this function will throw an ```DirectoryAlreadyExistsException()```, so make sure you check before you try to create one.

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

As with working with directories working with files is just as easy. UnityIO does it's best to try to make things as painless as possible. 

### Anatomy a File
UnityIO has two interfaces that are used when dealing with files. The first one is ```IFile``` and ```IFiles```. Ifiles is simple an extension of ```IList<IFile>``` with some extended functionality that I will cover later. IFile is the one you will be dealing with the most. IFile is implemented by ```File.cs``` and ```NullFile.cs```.
### Getting a File
To get a file does not require to much work. In the example below we go to the directory we want and ask for all the files in there.
``` csharp
// Normally we just use var for the return types but these examples we are using
// the interface types to make it more clear so we have to include the following.
using UnityIO.Interfaces;

/// <summary>
/// A simple example of getting all files from a folder called resources 
/// at 'Assets/Resources'
/// </summary>
public void GetResourceFiles()
{
    // Get our directory
    IDirectory resourcesDirectory = IO.Root["Resources"];
    // Get all files.
    IFiles files = resourcesDirectory.GetFiles(); 
}
```
The code above looks for the directory ```Assets/Resources``` and returns all files that are in there. This code returns us back an ```IFiles``` which we can then just loop over to grab each file one by one. 
```csharp
// Get our directory
IDirectory resourcesDirectory = IO.Root["Resources"];
// Get all files.
IFiles files = resourcesDirectory.GetFiles(); 
// iterate over our files and print their names
for(int i = 0; i < files.Count; i++)
{
    Debug.Log(files[i].Name);
}
```
Above we are using our IFiles and printing the names to the UnityEngine.Debug console. Lets say we wanted
to also get all files recursively we can do that too.
```csharp
public void GetFilesRecursively()
{
    // Get our directory
    IDirectory resourcesDirectory = IO.Root["Resources"];
    // Get all files recursively
    IFiles files = resourcesDirectory.GetFiles(recursive:true);
}
```

#### Searching for files
Sometimes we also might want to only select files if they match a name. UnityIO under the hood uses System.IO to find files
so it has the ability to use two unique wild cards. ```*```(asterisk) means Zero or more characters in that position. ```?``` (question mark) means Zero or one character in that position.
This search can not use Unity's tags like ```t:```, ```l:```, or ```ref:```.  

Below are a few examples of some search functions.
```csharp
// Returns every asset with 'Player' in it's name. 
var playerFiles = IO.Root.GetFiles("*Player*");
```
``` csharp
// Get everything named with 'Player' and '.anim' extension
var playerAnimations = IO.Root.GetFiles("*Player*.anim");
```
```csharp
// get everything named 'Player' with one extra char maybe an 's'
var playerChar = IO.Root.GetFiles("Player?");
```

```GetFiles()``` has a few different overrides that lets you combine both the search filter and the recursive bool.

Notes:
 * At one point I might add Regex searches to this feature but I am not sure if that is overkill. Let me know with feedback if you would like that feature. 
 * You might notice that you don't have the open to filter by Unity types. This was done so the code does not have to be rigid. I just did not want to have to write a huge case statement for all valid Unity types an extensions that match.
 * When you call ```GetFiles``` the assets themselves have not been loaded into memory we are only working with the paths.

### Deleting a File
Once you have your file you might just want to be able to delete it. 
```csharp
/// <summary>
/// Grabs all the files in the root directory and deletes them. 
/// </summary>
public void DeleteAFile()
{
    // Get all our files. 
    var files = IO.Root.GetFiles();
    // Loop over them all and delete them
    for (int i = 0; i < files.Count; i++)
    {
        // Delete the file.
        files[i].Delete();
    }
}
```
This is great but lets say you want to delete a file only if it exists. Well just like the IDirectory class IFile has the option to only do tasks if a condition is met.
```csharp
/// Only delete our file if it exists. 
IO.Root.IfFileExists("DeleteMe.txt").Delete(); 
```
As expected this will only delete the file if it happens to exist on disk and otherwise have no effect. 

Notes:
* All deletion uses AssetDatabase so the meta files are cleaned up too. 
* The files get deleted synchronously so if you check if it exists in the next line of code it will be cleared.  
* 
### Renaming a File
Sometimes you just want to change the file you are working with to do that try the following code. 

``` csharp
// Get the file we want to use.
IFile fileToRename = IO.Root.GetFiles("DeleteMe.txt").FirstOrDefault();
// Rename it
fileToRename.Rename("DontDeleteMe.txt");
```
You might also notice that we used the function ```FirstOrDefault``` this is a function on ```IFiles``` the returns the first file in the list or our null file (which just takes no actions. Read about it below). One thing you should take note of is ```Rename()``` is for renaming and not moving files, use ```Move()``` for that or an exception will be thrown.
### Moving a File
Moving a file is the same as renaming but you can move the file outside it's current directory. 
```csharp
// Get the file we want to use.
IFile fileToMove = IO.Root.GetFiles("DeleteMe.txt").FirstOrDefault();
// Move it
fileToRename.Move(IO.Root["Resources"].Path);
```
This will take the ```Assets/DeleteMe.txt``` and move it too ```Assets/Resources/DeleteMe.txt```. If a file already exists in that directory it will be given a unique name by append incrementing numbers to the end.
