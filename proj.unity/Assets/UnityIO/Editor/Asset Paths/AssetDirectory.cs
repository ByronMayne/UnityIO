/*>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
UnityIO was released with an MIT License.
Arther: Byron Mayne
Twitter: @ByMayne


MIT License

Copyright(c) 2016 Byron Mayne

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>*/

using UnityIO.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections;
using System.Collections.Generic;
using sIO = System.IO;
using UnityIO.Exceptions;
using UnityEditor;
using UnityIO.BaseClasses;
using System;

namespace UnityIO.Classes
{
    public class AssetDirectory : BaseDirectory, IDirectory
    {
        /// <summary>
        /// Gets the root directory that is defined by <see cref="AssetDatabase.rootDirectory"/>
        /// </summary>
        public static IDirectory Root
        {
            get
            {
                return new Directory(Application.dataPath);
            }

        }

        /// <summary>
        /// Creates a new Directory objects.
        /// </summary>
        /// <param name="directoryPath"></param>
        public AssetDirectory(string directoryPath) : base(directoryPath)
        {
            // Nothing to do here. 
        }


        /// <summary>
        /// Deletes this directory and all it's sub directories and children. 
        /// </summary>
        public override void Delete()
        {
            AssetDatabase.DeleteAsset(path);
        }


        /// <summary>
        /// Creates the directory on disk if it does not already exist. If sent in a nested directory the
        /// full path will be created. 
        /// </summary>
        /// <param name="directoryPath">The path to the directory that you want to create.</param>
        /// <returns></returns>
        public IDirectory CreateDirectory(string directoryPath)
        {
            string workingPath = path;
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
                directory = new Directory(path + IO.PATH_SPLITTER + directoryPath);
            }

            return directory;
        }

        /// <summary>
        /// Duplicates a directory and renames it. The new name is the full name
        /// mapped from the root of the assets folder.
        /// </summary>
        public override IDirectory Duplicate(string destDirName)
        {
            destDirName = AssetDatabase.GenerateUniqueAssetPath(destDirName);
            AssetDatabase.CopyAsset(path, destDirName);
            return Internal_Create(destDirName);
        }

        /// <summary>
        /// Moves a directory from one path to another. If a directory of the 
        /// same name already exists there it will give it a unique name. 
        /// </summary>
        /// <param name="moveDirectroy">The directory you want to move too</param>
        public override void Move(string targetDirectory)
        {
            int start = targetDirectory.LastIndexOf('/');
            int length = targetDirectory.Length - start;
            string name = targetDirectory.Substring(start, length);

            if (!IO.IsValidFileName(name))
            {
                throw new InvalidNameException("The name '" + name + "' contains invalid characters");
            }

            string error = AssetDatabase.ValidateMoveAsset(path, targetDirectory);

            if (!string.IsNullOrEmpty(error))
            {
                throw new MoveException(error, path, targetDirectory);
            }
            else
            {
                AssetDatabase.MoveAsset(path, targetDirectory);
            }
        }

        /// <summary>
        /// Loops over our directory recessively. 
        /// </summary>
        public override IEnumerator<IDirectory> GetEnumerator()
        {
            string[] subFolder = AssetDatabase.GetSubFolders(path);
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
            string[] subFolder = AssetDatabase.GetSubFolders(path);

            for (int i = 0; i < subFolder.Length; i++)
            {
                yield return new Directory(subFolder[i]);
            }
        }

        /// <summary>
        /// Allows us to explicitly convert a string to a new directory.
        /// </summary>
        public static explicit operator AssetDirectory(string directory)
        {
            return new AssetDirectory(directory);
        }

        /// <summary>
        /// Allows us to implicitly convert a directory to a string.
        /// </summary>
        public static implicit operator string(AssetDirectory directory)
        {
            return directory.path;
        }

        /// <summary>
        /// Our internal function which is used by all of the GetFilesFunctions. Used to search
        /// for files in the current directory. 
        /// </summary>
        /// <param name="filter">Which filter should be used to search</param>
        /// <param name="recursive">If we should also search sub directories.</param>
        /// <returns></returns>
        protected override IFiles GetFiles_Internal(string filter, bool recursive)
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

            string systemPath = Application.dataPath.Replace("Assets", path);

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

        public override IDirectory CreateSubDirectory(string directoryName)
        {
            if (SubDirectoryExists(directoryName))
            {
                return this[directoryName];
            }
            else
            {
                // Create our path 
                string directoryPath = path + IO.PATH_SPLITTER + directoryName;
                // Create it on disk
                return CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Returns back true if this file has any sub directories and false
        /// if it does not. 
        /// </summary>
        public override bool HasSubDirectories()
        {
            return AssetDatabase.GetSubFolders(path).Length > 0;
        }

        /// <summary>
        /// Returns a new Directory object based on the path sent in.
        /// </summary>
        protected override IDirectory Internal_Create(string assetPath)
        {
            return new AssetDirectory(assetPath); 
        }

        /// <summary>
        /// Returns back true if this directory exists on disk.
        /// </summary>
        protected override bool Exists(string assetPath)
        {
            return AssetDatabase.IsValidFolder(assetPath);
        }
    }
}
