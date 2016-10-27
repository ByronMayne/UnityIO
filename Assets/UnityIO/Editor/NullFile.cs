using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityIO.Interfaces;

namespace UnityIO.Classes
{
    public class NullFile : IDirectory, IFile, IFiles
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

        public int Count
        {
            get
            {
                return 0;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public File this[int index]
        {
            get
            {
                return null;
            }

            set
            {
                
                // Nothing
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
            return SHARED_INSTANCE;
        }

        public IFiles GetFiles(string filter)
        {
            return SHARED_INSTANCE;
        }

        public IFiles GetFiles(bool recursive)
        {
            return SHARED_INSTANCE;
        }

        public IFiles GetFiles(string filter, bool recursive)
        {
            return SHARED_INSTANCE;
        }

        public IFiles GetFiles<T>() where T : UnityEngine.Object
        {
            return SHARED_INSTANCE;
        }

        public IFiles GetFiles<T>(string filter) where T : UnityEngine.Object
        {
            return SHARED_INSTANCE;
        }

        public IFiles GetFiles<T>(bool recursive) where T : UnityEngine.Object
        {
            return SHARED_INSTANCE;
        }

        public IFiles GetFiles<T>(string filter, bool recursive)
        {
            return SHARED_INSTANCE;
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

        public void Move(string newDirectory)
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
            return null;
        }

        public T LoadAsset<T>() where T : UnityEngine.Object
        {
            return null;
        }

        public IList<T> LoadAllofType<T>() where T : UnityEngine.Object
        {
            return null;
        }

        public IFile FirstOrDefault()
        {
            return null;
        }

        public int IndexOf(File item)
        {
            return 0;
        }

        public void Insert(int index, File item)
        {
            
        }

        public void RemoveAt(int index)
        {
           
        }

        public void Add(File item)
        {
           
        }

        public void Clear()
        {
            
        }

        public bool Contains(File item)
        {
            return false;
        }

        public void CopyTo(File[] array, int arrayIndex)
        {
            
        }

        public bool Remove(File item)
        {
            return false;
        }

        IEnumerator<File> IEnumerable<File>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IFile CreateFile<T>(string name, T asset) where T : UnityEngine.Object
        {
            return SHARED_INSTANCE;
        }
    }
}