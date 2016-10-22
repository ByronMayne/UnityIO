using System;
using UnityEngine;
using UnityIO.Interfaces;

namespace UnityIO.Classes
{
    public class NullFile : IDirectory, IFile
    {
        public static NullFile SHARED_INSTANCE = new NullFile();

        public IDirectory this[string name]
        {
            get
            {
                return SHARED_INSTANCE;
            }
        }

        public IDirectory Directory
        {
            get
            {
                return SHARED_INSTANCE;
            }
        }

        public IDirectory CreateDirectory(string name)
        {
            return SHARED_INSTANCE;
        }

        public void Delete()
        {
        }

        public void DeleteSubDirectory(string directroyName)
        {
        }

        public bool DirectoryExists(string directoryName)
        {
            return false;
        }

        public void Duplicate()
        {
        }

        public void Duplicate(string newName)
        {
        }

        public IDirectory GetDirectory(string name)
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles()
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles(string filter)
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles(bool recursive)
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles(string filter, bool recursive)
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles<T>() where T : UnityEngine.Object
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles<T>(string filter) where T : UnityEngine.Object
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles<T>(bool recursive) where T : UnityEngine.Object
        {
            return SHARED_INSTANCE;
        }

        public IFile GetFiles<T>(string filter, bool recursive)
        {
            return SHARED_INSTANCE;
        }

        public IDirectory IfDirectoryExists(string name)
        {
            return SHARED_INSTANCE;
        }

        public IDirectory IfEmpty(bool assetsOnly)
        {
            return SHARED_INSTANCE;
        }

        public bool IsEmpty(bool assetsOnly)
        {
            return true;
        }

        public IFile IfFileExists(string name)
        {
            return SHARED_INSTANCE;
        }

        public IDirectory IfNotEmpty(bool assetsOnly)
        {
            return SHARED_INSTANCE;
        }

        public void Move(string newPath)
        {

        }

        public void Rename(string newName)
        {
        }

        IFile IFile.Duplicate()
        {
            return SHARED_INSTANCE;
        }

        IFile IFile.Duplicate(string newName)
        {
            return SHARED_INSTANCE;
        }
    }
}