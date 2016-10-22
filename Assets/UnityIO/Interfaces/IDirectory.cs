using UnityEngine;

namespace UnityIO.Interfaces
{
    public interface IDirectory
    {
        void Delete();
        void DeleteSubDirectory(string directroyName);
        bool DirectoryExists(string directoryName);

        IDirectory this[string name] { get; }
        IDirectory GetDirectory(string name);
        IDirectory CreateDirectory(string name);
        IDirectory IfDirectoryExists(string name);

        IFile GetFiles();
        IFile GetFiles(bool recursive);
        IFile GetFiles(string filter);
        IFile GetFiles(string filter, bool recursive);
        IFile GetFiles<T>() where T : UnityEngine.Object;
        IFile GetFiles<T>(bool recursive) where T : UnityEngine.Object;
        IFile GetFiles<T>(string filter) where T : UnityEngine.Object;
        IFile GetFiles<T>(string filter, bool recursive);
        IFile IfFileExists(string name);
    }
}
