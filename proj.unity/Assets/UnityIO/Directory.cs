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
using System.Collections.Generic;
using sIO = System.IO;
using UnityIO.Exceptions;
using UnityIO.BaseClasses;
using System;

namespace UnityIO.Classes
{
    public class Directory : BaseDirectory, IDirectory
    {
        /// <summary>
        /// Creates a new Directory objects.
        /// </summary>
        /// <param name="directoryPath"></param>
        public Directory(string directoryPath) : base(directoryPath)
        {
            // Nothing to do here. 
        }

        /// <summary>
        /// Deletes this directory and all it's sub directories and children. 
        /// </summary>
        public override void Delete()
        {
            if (Exists())
            {
                sIO.Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// Creates the directory on disk if it does not already exist. If sent in a nested directory the
        /// full path will be created. 
        /// </summary>
        /// <param name="directoryPath">The path to the directory that you want to create.</param>
        public override IDirectory CreateSubDirectory(string directoryName)
        {
            if(SubDirectoryExists(directoryName))
            {
                return this[directoryName];
            }
            else
            {
                // Create our path 
                string directoryPath = path + IO.PATH_SPLITTER + directoryName;
                // Create it on disk
                sIO.Directory.CreateDirectory(directoryPath);
                // Return the result
                return Internal_Create(directoryPath);
            }
        }

        /// <summary>
        /// Duplicates a directory and renames it. The new name is the full name
        /// mapped from the root of the assets folder.
        /// </summary>
        public sealed override IDirectory Duplicate(string destDirName)
        {
            if (Exists())
            {
                // If it's just a name we will create a path for them
                if(destDirName.IndexOf(IO.PATH_SPLITTER) == -1)
                {
                    destDirName = PathUtility.Rename(path, destDirName);
                }

                // Do the copy
                Internal_Duplicate(path, destDirName, true);
                // Return the result
                return Internal_Create(destDirName);
            }
            else
            {
                throw new DirectoryNotFoundException("Can't duplicate a directory that does not exist");
            }
        }



        /// <summary>
        /// Moves a directory from one path to another. If a directory of the 
        /// same name already exists there it will give it a unique name. 
        /// </summary>
        /// <param name="moveDirectroy">The directory you want to move too</param>
        public override void Move(string destDirName)
        {
            if (!string.IsNullOrEmpty(destDirName))
            {
                throw new MoveException("No detestation directory was defined.", path, destDirName);
            }

            if (!IO.IsValidFileName(destDirName))
            {
                throw new InvalidNameException("The name '" + destDirName + "' contains invalid characters");
            }

            // Make sure we have a valid path
            IO.ValidatePath(destDirName);

            // If we exist on disk we also want to move it. 
            if (Exists())
            {
                sIO.Directory.Move(path, destDirName);
            }

            // Set our new path
            path = destDirName;
        }

        /// <summary>
        /// Returns true if we have any sub directories and false 
        /// if we don't.
        /// </summary>
        public override bool HasSubDirectories()
        {
            var result = sIO.Directory.GetDirectories(path, "*", System.IO.SearchOption.TopDirectoryOnly);
            return result.Length > 0;
        }

        /// <summary>
        /// Returns true if this object directory on disk. 
        /// </summary>
        protected override bool Exists(string filePath)
        {
            return sIO.Directory.Exists(filePath);
        }

        /// <summary>
        /// Loops over our directory recessively. 
        /// </summary>
        public override IEnumerator<IDirectory> GetEnumerator()
        {
            // Get all directories 
            string[] directories = sIO.Directory.GetDirectories(path, "*", System.IO.SearchOption.AllDirectories);
            // Loop over them all
            for (int i = 0; i < directories.Length; i++)
            {
                yield return Internal_Create(directories[i]);
            }
        }

        /// <summary>
        /// Returns a new Directory object based on the path sent in.
        /// </summary>
        protected override IDirectory Internal_Create(string directoryPath)
        {
            return new Directory(directoryPath);
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
            // Create an option
            sIO.SearchOption options;
            // Set it's values.
            if (recursive)
            {
                options = sIO.SearchOption.AllDirectories;
            }
            else
            {
                options = sIO.SearchOption.TopDirectoryOnly;
            }

            // Create a result.
            IFiles iFiles = new Files();

            // Find all files on disk. 
            string[] serachResult = sIO.Directory.GetFiles(path, filter, options);
            // Loop over them all and add them to files result. 
            for (int i = 0; i < serachResult.Length; i++)
            {
                iFiles.Add(new File(serachResult[i]));
            }
            // Return it.
            return iFiles;
        }

        /// <summary>
        /// Copies a whole directory and all it's contents. 
        /// </summary>
        protected static void Internal_Duplicate(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            sIO.DirectoryInfo dir = new sIO.DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            sIO.DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!sIO.Directory.Exists(destDirName))
            {
                UnityEngine.Debug.Log("SRC: " + sourceDirName);
                sIO.Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            sIO.FileInfo[] files = dir.GetFiles();
            foreach (sIO.FileInfo file in files)
            {
                string temppath = sIO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (sIO.DirectoryInfo subdir in dirs)
                {
                    string temppath = sIO.Path.Combine(destDirName, subdir.Name);
                    Internal_Duplicate(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}