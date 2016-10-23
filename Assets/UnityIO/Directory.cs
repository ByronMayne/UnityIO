using System;
using UnityIO.Interfaces;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections;
using System.Collections.Generic;
using sIO = System.IO;

namespace UnityIO.Classes
{
    public class Directory : IDirectory
    {
        private string m_Path;


        public string Path
        {
            get { return m_Path; }
        }

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
                if (SubDirectoryExists(directoryPath))
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
            if (!SubDirectoryExists(directoryPath))
            {
                string[] paths = directoryPath.Split(IO.PATH_SPLITTER);
                for (int i = 0; i < paths.Length; i++)
                {
                    if (!SubDirectoryExists(workingPath + IO.PATH_SPLITTER + paths[i]))
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
            AssetDatabase.DeleteAsset(m_Path);
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

        /// <summary>
        /// Our internal function which is used by all of the GetFilesFunctions. Used to search
        /// for files in the current directory. 
        /// </summary>
        /// <param name="filter">Which filter should be used to search</param>
        /// <param name="recursive">If we should also search sub directoires.</param>
        /// <returns></returns>
        private IFiles GetFiles_Internal(string filter, bool recursive)
        {
            sIO.SearchOption options;
            if (recursive)
            {
                options = sIO.SearchOption.AllDirectories;
            }
            else
            {
                options = sIO.SearchOption.TopDirectoryOnly;
            }

            string systemPath = Application.dataPath.Replace("Assets", m_Path);

            IFiles iFiles = new Files();

            string[] serachResult = sIO.Directory.GetFiles(systemPath, filter, options);
            for (int i = 0; i < serachResult.Length; i++)
            {
                if (!serachResult[i].EndsWith(".meta"))
                {
                    string unityPath = FileUtil.GetProjectRelativePath(serachResult[i]);
                    iFiles.Add(new File(unityPath));
                }
            }

            return iFiles;
        }

        /// <summary>
        /// Gets all the Unity files that are at the top level of this directory.
        /// </summary>
        public IFiles GetFiles()
        {
            return GetFiles_Internal("*", recursive:false);
        }

        /// <summary>
        /// Gets all the Unity files that are at the top level of this directory with a filter.
        /// </summary>
        public IFiles GetFiles(string filter)
        {
            return GetFiles_Internal(filter, recursive:false);
        }

        /// <summary>
        /// Gets all the Unity files that are in this directory with an option to look recessively.
        /// </summary>
        public IFiles GetFiles(bool recursive)
        {
            return GetFiles_Internal("*", recursive);
        }

        /// <summary>
        /// Gets all the Unity files that are in this directory with an option to look recessively with a filter.
        /// </summary>
        public IFiles GetFiles(string filter, bool recursive)
        {
            return GetFiles_Internal(filter, recursive);
        }

        /// <summary>
        /// If the directory exists this will return that directory. If the directory does not
        /// exist it will return a NullFile directory which will make all the following function
        /// calls not have any effect. This allows you to chain requests and only continue execution
        /// if the directory exists. 
        /// </summary>
        /// <param name="directoryPath">The path to the directory you are trying to find.</param>
        /// <returns>The IDirectory class or a NullFile if ti does not exist.</returns>
        public IDirectory IfSubDirectoryExists(string directoryPath)
        {
            if (SubDirectoryExists(directoryPath))
            {
                return this[directoryPath];
            }
            else
            {
                return NullFile.SHARED_INSTANCE;
            }
        }

        /// <summary>
        /// If the directory does not exist this will return the current directory otherwise if it
        /// does it will return a null file. 
        /// </summary>
        /// <param name="directoryPath">The path to the directory you are trying to find.</param>
        /// <returns>This directory class or a NullFile if ti does exist.</returns>
        public IDirectory IfSubDirectoryDoesNotExist(string directoryPath)
        {
            if (!SubDirectoryExists(directoryPath))
            {
                return this;
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
        public bool SubDirectoryExists(string directoryPath)
        {
            IO.ValidatePath(directoryPath);
            return AssetDatabase.IsValidFolder(m_Path + '/' + directoryPath);
        }

        /// <summary>
        /// Checks this directory to see if any assets are contained inside of it
        /// or any of it's sub folder. 
        /// </summary>
        /// <param name="assetOnly">If this directory contains only other empty sub directories it will be considered empty otherwise it will not be.</param>
        /// <returns>true if it's empty and false if it's not</returns>
        public bool IsEmpty(bool assetOnly = false)
        {
            // This is the only way in Unity to check if a folder has anything.
            int assetCount = AssetDatabase.FindAssets(string.Empty, new string[] { m_Path }).Length;

            if (!assetOnly)
            {
                assetCount += AssetDatabase.GetSubFolders(m_Path).Length;
            }

            return assetCount == 0;
        }

        /// <summary>
        /// Duplicates the current directory in the same place but gives it a unique
        /// name by adding an incrementing number at the end. 
        /// </summary>
        public void Duplicate()
        {
            string copyDir = AssetDatabase.GenerateUniqueAssetPath(m_Path);
            AssetDatabase.CopyAsset(m_Path, copyDir);
        }

        /// <summary>
        /// Duplicates a directory and renames it. The new name is the full name
        /// mapped from the root of the assets folder.
        /// </summary>
        public void Duplicate(string copyDirectroy)
        {
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(copyDirectroy);
            AssetDatabase.CopyAsset(m_Path, uniquePath);
        }

        /// <summary>
        /// Moves a directory from one path to another. If a directory of the 
        /// same name already exists there it will give it a unique name. 
        /// </summary>
        /// <param name="moveDirectroy">The directory you want to move too</param>
        public void Move(string moveDirectroy)
        {
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(moveDirectroy);
            Debug.LogError(AssetDatabase.MoveAsset(m_Path, uniquePath));
        }

        /// <summary>
        /// Renames our directory to the name of our choice.
        /// </summary>
        public void Rename(string newName)
        {
            AssetDatabase.RenameAsset(m_Path, newName);
        }

        /// <summary>
        /// Returns this directory if it's empty otherwise it returns
        /// a null directory which will then ignore all other commands. 
        /// </summary>
        /// <param name="assetsOnly">If false sub directories folders will not count as content inside the folder. If true a folder
        /// just filled with empty folders will count as not being empty.</param>
        /// <returns>This directory if it's not empty otherwise a null file directory.</returns>
        public IDirectory IfEmpty(bool assetsOnly)
        {
            if(IsEmpty(assetsOnly))
            {
                return this;
            }
            else
            {
                return NullFile.SHARED_INSTANCE;
            }
        }

        /// <summary>
        /// Returns this directory if it's not empty otherwise it returns
        /// a null directory which will then ignore all other commands. 
        /// </summary>
        /// <param name="assetsOnly">If false sub directories folders will not count as content inside the folder. If true a folder
        /// just filled with empty folders will count as not being empty.</param>
        /// <returns>This directory if it's not empty otherwise a null file directory.</returns>
        public IDirectory IfNotEmpty(bool assetsOnly)
        {
            if (!IsEmpty(assetsOnly))
            {
                return this;
            }
            else
            {
                return NullFile.SHARED_INSTANCE;
            }
        }

        /// <summary>
        /// Loops over our directory recessively. 
        /// </summary>
        IEnumerator<IDirectory> IEnumerable<IDirectory>.GetEnumerator()
        {
            string[] subFolder = AssetDatabase.GetSubFolders(m_Path);
            yield return this;
            for (int i = 0; i < subFolder.Length; i++)
            {
                IEnumerable<IDirectory> enumerable = new Directory(subFolder[i]);
                IEnumerator<IDirectory> enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            string[] subFolder = AssetDatabase.GetSubFolders(m_Path);

            for (int i = 0; i < subFolder.Length; i++)
            {
                yield return new Directory(subFolder[i]);
            }
        }
    }
}
