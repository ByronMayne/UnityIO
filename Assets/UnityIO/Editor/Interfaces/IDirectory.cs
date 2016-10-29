using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace UnityIO.Interfaces
{
    public interface IDirectory : IEnumerable<IDirectory>, IComparer<IDirectory>, IEquatable<IDirectory>
    {
        string Path { get; }

        void Delete();
        void Duplicate();
        void Duplicate(string newName);

        void Move(string newPath);
        void Rename(string newName);

        void DeleteSubDirectory(string directroyName);
        bool SubDirectoryExists(string directoryName);

        bool IsEmpty(bool assetsOnly);

        // Directories
        IDirectory this[string name] { get; }
        IDirectory CreateDirectory(string name);


        // Conditionals 
        IDirectory IfSubDirectoryExists(string name);
        IDirectory IfSubDirectoryDoesNotExist(string name);
        IDirectory IfEmpty(bool assetsOnly);
        IDirectory IfNotEmpty(bool assetsOnly);

        // IFIle
        IFile IfFileExists(string name);
        IFile CreateFile<T>(string name, T asset) where T : Object;

        // IFiles
        IFiles GetFiles();
        IFiles GetFiles(bool recursive);
        IFiles GetFiles(string filter);
        IFiles GetFiles(string filter, bool recursive);
    }
}
