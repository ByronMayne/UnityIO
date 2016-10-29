using UnityEngine;

namespace UnityIO.Interfaces
{
    /// <summary>
    /// IFile is the root interface that we use to handle all file actions. <see cref="UnityIO.Classes.File"/> to
    /// see the implementation.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Gets the path to this file starting from the root of the Unity project. 
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the name of this file with it's extension included.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the name of this file without it's extension.
        /// </summary>
        string NameWithoutExtension { get; }

        /// <summary>
        /// Gets the extension of this file.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Deletes this file.
        /// </summary>
        void Delete();
        /// <summary>
        /// Renames the files throws an exception if a file already exists with the same name.
        /// </summary>
        /// <param name="name"></param>
        void Rename(string name);
        /// <summary>
        /// Moves a file from one location to another will force the name to be unique
        /// if a file already exists with the same name. 
        /// </summary>
        /// <param name="directroy"></param>
        void Move(string directroy);
        /// <summary>
        /// Copies the file on disk and will force it to have a unique name (appending a number to
        /// the end.
        /// </summary>
        IFile Duplicate();
        /// <summary>
        /// Copies the file on disk and renames it. Name must be unique or an exception is thrown. 
        /// </summary>
        IFile Duplicate(string newName);
        /// <summary>
        /// Returns the back directory that this file exists in.
        /// </summary>
        IDirectory Directory { get; }
        /// <summary>
        /// Loads the <see cref="UnityEngine.Object"/> asset that this file points too. 
        /// </summary>
        /// <returns></returns>
        Object LoadAsset();
        /// <summary>
        /// Load a type of <see cref="UnityEngine.Object"/> from disk that this file points too. 
        /// </summary>
        /// <typeparam name="T">The type of object you want to load</typeparam>
        T LoadAsset<T>() where T : Object;
    }
}
