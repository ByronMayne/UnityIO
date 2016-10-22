using UnityEngine;

namespace UnityIO.Interfaces
{
    public interface IDirectory
    {
        void Delete();
        void Duplicate();
        void Duplicate(string newName);

        void Move(string newPath);
        void Rename(string newName);

        void DeleteSubDirectory(string directroyName);
        bool SubDirectoryExists(string directoryName);

        bool IsEmpty(bool assetsOnly);

        IDirectory this[string name] { get; }
        IDirectory GetDirectory(string name);
        IDirectory CreateDirectory(string name);

        // Conditionals 
        IDirectory IfSubDirectoryExists(string name);
        IDirectory IfSubDirectoryDoesNotExist(string name);
        IFile IfFileExists(string name);
        IDirectory IfEmpty(bool assetsOnly);
        IDirectory IfNotEmpty(bool assetsOnly);

        IFile GetFiles();
        IFile GetFiles(bool recursive);
        IFile GetFiles(string filter);
        IFile GetFiles(string filter, bool recursive);
        IFile GetFiles<T>() where T : UnityEngine.Object;
        IFile GetFiles<T>(bool recursive) where T : UnityEngine.Object;
        IFile GetFiles<T>(string filter) where T : UnityEngine.Object;
        IFile GetFiles<T>(string filter, bool recursive);
 
    }
}
