using UnityEngine;

namespace UnityIO.Interfaces
{
    public interface IFile
    {
        void Delete();
        void Rename(string name);
        IFile Duplicate();
        IFile Duplicate(string newName);
        IDirectory Directory { get; }
    }
}
