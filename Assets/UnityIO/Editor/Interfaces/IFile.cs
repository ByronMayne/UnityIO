using UnityEngine;

namespace UnityIO.Interfaces
{
    public interface IFile
    {
        void Delete();
        void Rename(string name);
        void Move(string directroy);
        IFile Duplicate();
        IFile Duplicate(string newName);
        string Path { get; }
        IDirectory Directory { get; }
        Object LoadAsset();
        T LoadAsset<T>() where T : Object;
    }
}
