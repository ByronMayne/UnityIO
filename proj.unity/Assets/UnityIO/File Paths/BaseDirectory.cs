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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityIO.Classes;
using UnityIO.Interfaces;
using sIO = System.IO;

namespace UnityIO.BaseClasses
{
    public abstract class BaseDirectory : IOBase, IDirectory
    {
        /// <summary>
        /// Creates a new instance of the base directory class.
        /// </summary>
        internal BaseDirectory(string path) : base(path)
        {
            IO.ValidatePath(path);
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDirectory this[string directoryName]
        {
            get
            {
                // Create our path. 
                string subDirectory = path + IO.PATH_SPLITTER + directoryName;
                // Make sure it exists
                if (Exists(subDirectory))
                {
                    // Make sure we have a valid path
                    IO.ValidatePath(path);
                    // Return it
                    return IO.GetDirectory(path + IO.PATH_SPLITTER + directoryName);
                }

                throw new InvalidOperationException("Directory '" + subDirectory + "' does not exist on disk. Use IfExists() before trying to access");
            }
        }

        /// <summary>
        /// Creates the directory on disk if it does not already exist. If sent in a nested directory the
        /// full path will be created. 
        /// </summary>
        /// <param name="directoryPath">The path to the directory that you want to create.</param>
        public abstract IDirectory CreateSubDirectory(string directoryName);

        /// <summary>
        /// Gets all the files that are at the top level of this directory.
        /// </summary>
        public IFiles GetFiles()
        {
            return GetFiles_Internal("*", false);
        }

        /// <summary>
        /// Gets all the Unity files that are at the top level of this directory with a filter.
        /// </summary>
        public IFiles GetFiles(string filter)
        {
            return GetFiles_Internal(filter, false);
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
        /// Implementation of ICompair interface. 
        /// </summary>
        public int Compare(IDirectory x, IDirectory y)
        {
            return string.Compare(x.path, y.path);
        }

        /// <summary>
        /// Finds a sub directory of this directory and deletes it if
        /// it does exist otherwise has no effect. 
        /// </summary>
        /// <param name="directroyName">The sub directory you want to delete.</param>
        public void DeleteSubDirectory(string directroyName)
        {
            // Delete it if it exists.
            this[directroyName].IfExists().Delete();
        }
        #region  -= Conditionals =-
        /// <summary>
        /// returns back this directory if it exists on disk others
        /// it returns back a null directory which will then ignore all other
        /// commands. 
        /// </summary>
        public IDirectory IfExists()
        {
            if (Exists())
            {
                return this;
            }
            else
            {
                return NullFile.SHARED_INSTANCE;
            }
        }

        /// <summary>
        /// returns back this directory if it does not exists on disk others
        /// it returns back a null directory which will then ignore all other
        /// commands. 
        /// </summary>
        public IDirectory IfDoesNotExist()
        {
            if (!Exists())
            {
                return this;
            }
            else
            {
                return NullFile.SHARED_INSTANCE;
            }
        }

        /// <summary>
        /// Returns this directory if it's empty otherwise it returns
        /// a null directory which will then ignore all other commands. 
        /// </summary>
        /// <param name="directoriesCount">If false sub directories folders will not count as content inside the folder. If true a folder
        /// just filled with empty folders will count as not being empty.</param>
        /// <returns>This directory if it's not empty otherwise a null file directory.</returns>
        public IDirectory IfEmpty(bool directoriesCount)
        {
            if (IsEmpty(directoriesCount))
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
        /// Finds if this files exists in this directory. 
        /// </summary>
        /// <param name="name"></param>
        public IFile IfFileExists(string fileName)
        {
            // Find the files
            IFiles result = GetFiles(filter: "*" + fileName + "*", recursive: false);
            // Return the result or a null file (this is not LINQ). 
            return result.FirstOrDefault();
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
        #endregion 

        /// <summary>
        /// Checks this directory to see if any assets are contained inside of it
        /// or any of it's sub folder. 
        /// </summary>
        /// <param name="directoriesCount">If this directory contains only other empty sub directories it will be considered empty otherwise it will not be.</param>
        /// <returns>true if it's empty and false if it's not</returns>
        public bool IsEmpty(bool directoriesCount)
        {
            // Get all the files. 
            IFiles result = GetFiles(recursive: true);

            // If we have any we are good
            if(result.Count > 0)
            {
                return false;
            }

            // Check if we have any subs.
            if(directoriesCount)
            {
                return !HasSubDirectories();
            }

            return true;
        }

        /// <summary>
        /// Returns true if the sub directory exists and false 
        /// if it does not.
        /// </summary>
        /// <param name="directoryName">The directory you want to check if it exists.</param>
        /// <returns>True if it exists and false if it does not.</returns>
        public bool SubDirectoryExists(string directoryName)
        {
            // Make sure we have a valid path. 
            string directoryPath = path + IO.PATH_SPLITTER + directoryName;
            // Check if it exists
            return Exists(directoryPath);
        }

        /// <summary>
        /// returns true if we have any sub directories and false
        /// if we don't.
        /// </summary>
        public abstract bool HasSubDirectories();

        /// <summary>
        /// Our internal function which is used by all of the GetFilesFunctions. Used to search
        /// for files in the current directory. 
        /// </summary>
        /// <param name="filter">Which filter should be used to search</param>
        /// <param name="recursive">If we should also search sub directories.</param>
        protected abstract IFiles GetFiles_Internal(string filter, bool recursive);


        /// <summary>
        /// Duplicates a directory and gives it a unique name. 
        /// </summary>
        public IDirectory Duplicate()
        {
            // Get our current path 
            string name = PathUtility.GetName(path);
            string uniquePath = path;

            // Loop over it until we have a unique one
            for(int i = 1; i < 1000; i++)
            {
                if (!Exists(uniquePath))
                {
                    foreach (char c in uniquePath)
                    {
                        UnityEngine.Debug.Log("'" + c + "'");
                    }
                 
                    return Duplicate(uniquePath);
                }

                // Try a new number
                uniquePath = PathUtility.Rename(uniquePath, name + "_" + i.ToString("00"));
            }

            // Could not create the file.
            return null;
        }

        /// <summary>
        /// Duplicates a directory and renames it. The new name is the full name
        /// mapped from the root of the assets folder.
        /// </summary>
        public abstract IDirectory Duplicate(string newName);

        /// <summary>
        /// Checks to see if the two directories point to the same path. Implementation of <see cref="IEquatable<IDirectory>"/>
        /// </summary>
        public bool Equals(IDirectory other)
        {
            return string.CompareOrdinal(path, other.path) == 0;
        }

        /// <summary>
        /// Loops over our directory recessively. 
        /// </summary>
        public abstract IEnumerator<IDirectory> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
