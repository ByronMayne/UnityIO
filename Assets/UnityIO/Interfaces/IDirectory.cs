using UnityEngine;
using System.Collections.Generic;

namespace UnityIO.Interfaces
{
    public interface IDirectory : IEnumerable<IDirectory>
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

        IDirectory this[string name] { get; }
        IDirectory CreateDirectory(string name);


        // Conditionals 
        IDirectory IfSubDirectoryExists(string name);
        IDirectory IfSubDirectoryDoesNotExist(string name);
        IFile IfFileExists(string name);
        IDirectory IfEmpty(bool assetsOnly);
        IDirectory IfNotEmpty(bool assetsOnly);

        IFiles GetFiles();
        IFiles GetFiles(bool recursive);
        IFiles GetFiles(string filter);
        IFiles GetFiles(string filter, bool recursive);
    }
}
