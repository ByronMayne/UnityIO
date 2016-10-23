using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityIO.Interfaces;

namespace UnityIO.Classes
{
    public class NullFile : IDirectory, IFile
    {
        public static NullFile SHARED_INSTANCE = new NullFile();

        public string Path
        {
            get { return "Null"; }
        }

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

        public bool SubDirectoryExists(string directoryName)
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

        public IFiles GetFiles()
        {
            throw new NotImplementedException();
        }

        public IFiles GetFiles(string filter)
        {
            throw new NotImplementedException();
        }

        public IFiles GetFiles(bool recursive)
        {
            throw new NotImplementedException();
        }

        public IFiles GetFiles(string filter, bool recursive)
        {
            throw new NotImplementedException();
        }

        public IFiles GetFiles<T>() where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public IFiles GetFiles<T>(string filter) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public IFiles GetFiles<T>(bool recursive) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public IFiles GetFiles<T>(string filter, bool recursive)
        {
            throw new NotImplementedException();
        }

        public IDirectory IfSubDirectoryExists(string name)
        {
            return SHARED_INSTANCE;
        }

        public IDirectory IfSubDirectoryDoesNotExist(string directoryName)
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

        public IEnumerator<IDirectory> GetEnumerator()
        {
            yield return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return null;
        }

        public UnityEngine.Object LoadAsset()
        {
            throw new NotImplementedException();
        }

        public T LoadAsset<T>() where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }
    }
}