using System;
using UnityIO.Interfaces;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityIO.Classes
{
    public class Directory : IDirectory
    {
        private string m_Path;

        public Directory(string directoryPath)
        {
            IO.ValidatePath(directoryPath);
            m_Path = directoryPath;
        }

        /// <summary>
        /// Returns back a sub directory of this directory if it exists 
        /// otherwise returns a null file object. 
        /// </summary>
        /// <param name="name">The directory you want to find</param>
        /// <returns>The sub directory or a null directory object</returns>
        public IDirectory this[string directoryPath]
        {
            get
            {
                IO.ValidatePath(directoryPath);
                if (DirectoryExists(directoryPath))
                {
                    return new Directory(m_Path + IO.PATH_SPLITTER + directoryPath);
                }
                else
                {
                    throw new System.IO.DirectoryNotFoundException("UnityUI: A directory was not found at " + directoryPath);
                }
            }
        }

        /// <summary>
        /// Creates the directory on disk if it does not already exist. If sent in a nested directory the
        /// full path will be created. 
        /// </summary>
        /// <param name="directoryPath">The path to the directory that you want to create.</param>
        /// <returns></returns>
        public IDirectory CreateDirectory(string directoryPath)
        {
            string workingPath = m_Path;
            IDirectory directory = null;
            if (!DirectoryExists(directoryPath))
            {
                string[] paths = directoryPath.Split(IO.PATH_SPLITTER);
                for (int i = 0; i < paths.Length; i++)
                {
                    if (!DirectoryExists(workingPath + IO.PATH_SPLITTER + paths[i]))
                    {
                        AssetDatabase.CreateFolder(workingPath, paths[i]);
                    }
                    workingPath += IO.PATH_SPLITTER + paths[i];
                }
                directory = new Directory(workingPath);
            }
            else
            {
                directory = new Directory(m_Path + IO.PATH_SPLITTER + directoryPath);
            }
       
            return directory;
        }

        /// <summary>
        /// Deletes this directory and all it's sub directories and children. 
        /// </summary>
        public void Delete()
        {
            FileUtil.DeleteFileOrDirectory(m_Path);
        }

        /// <summary>
        /// Finds a sub directory of this directory and deletes it if
        /// it does exist otherwise has no effect. 
        /// </summary>
        /// <param name="directroyName">The sub directory you want to delete.</param>
        public void DeleteSubDirectory(string directroyName)
        {
            IDirectory directoryToDelete = this[directroyName];
            directoryToDelete.Delete();
        }

        public IDirectory GetDirectory(string name)
        {
            throw new NotImplementedException();
        }

        public IFile GetFiles()
        {
            throw new NotImplementedException();
        }

        public IFile GetFiles(string filter)
        {
            throw new NotImplementedException();
        }

        public IFile GetFiles(bool recursive)
        {
            throw new NotImplementedException();
        }

        public IFile GetFiles(string filter, bool recursive)
        {
            throw new NotImplementedException();
        }

        public IFile GetFiles<T>() where T : UnityEngine.Object
        {
            return NullFile.SHARED_INSTANCE;
        }

        public IFile GetFiles<T>(string fileter) where T : UnityEngine.Object
        {
            return NullFile.SHARED_INSTANCE;
        }

        public IFile GetFiles<T>(bool recursive) where T : UnityEngine.Object
        {
            return NullFile.SHARED_INSTANCE;
        }

        public IFile GetFiles<T>(string filter, bool recursive)
        {
            return NullFile.SHARED_INSTANCE;
        }

        /// <summary>
        /// If the directory exists this will return that directory. If the directory does not
        /// exist it will return a NullFile directory which will make all the following function
        /// calls not have any effect. This allows you to chain requests and only continue execution
        /// if the directory exists. 
        /// </summary>
        /// <param name="directoryPath">The path to the directory you are trying to find.</param>
        /// <returns>The IDirectory class or a NullFile if ti does not exist.</returns>
        public IDirectory IfDirectoryExists(string directoryPath)
        {
            if(DirectoryExists(directoryPath))
            {
                return this[directoryPath];
            }
            else
            {
                return NullFile.SHARED_INSTANCE;
            }
        }

        public IFile IfFileExists(string name)
        {
            return NullFile.SHARED_INSTANCE;
        }

        /// <summary>
        /// Returns true if the sub directory exists and false 
        /// if it does not.
        /// </summary>
        /// <param name="directoryName">The directory you want to check if it exists.</param>
        /// <returns>True if it exists and false if it does not.</returns>
        public bool DirectoryExists(string directoryPath)
        {
            IO.ValidatePath(directoryPath);
            return AssetDatabase.IsValidFolder(m_Path + '/' + directoryPath);
        }


    }
}
