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
using UnityIO.Exceptions;
using sIO = System.IO;
using UnityIO.BaseClasses;
using System;

namespace UnityIO.Classes
{
    public class File : BaseFile, IFile
    {
        /// <summary>
        /// Returns back the directory that this file exists in.
        /// </summary>
        public override IDirectory directory
        {
            get { return IO.GetDirectory(m_Directory); }
        }

        /// <summary>
        /// Creates a new instance of a file. 
        /// </summary>
        public File(string path) : base(path)
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Delete this file from disk.
        /// </summary>
        public override void Delete()
        {
            if (Exists())
            {
                sIO.File.Delete(path);
            }
        }

        /// <summary>
        /// Creates a copy of this file with a new name. The new name should not contain the extension
        /// that will be preserved automatically. 
        /// </summary>
        /// <param name="newName">The new name of the file (excluding the extension)</param>
        /// <returns></returns>
        public override IFile Duplicate(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new System.ArgumentNullException("You can't send a empty or null string to rename an asset. Trying to rename " + path);
            }
            // Make sure we don't have an extension. 
            if (!string.IsNullOrEmpty(sIO.Path.GetExtension(newName)))
            {
                throw new InvalidNameException("When you duplicate an asset it should not have an extension " + newName);
            }
            // Get our current directory
            string directory = System.IO.Path.GetDirectoryName(path);
            // and the extension
            string extension = System.IO.Path.GetExtension(path);
            // Get our path. 
            string copyDir = AssetDatabase.GenerateUniqueAssetPath(directory + "/" + newName + extension);
            // Copy our asset
            AssetDatabase.CopyAsset(path, copyDir);
            // Return new IFile
            return new File(copyDir);
        }


        /// <summary>
        /// Moves the files from it's current directory to another. 
        /// </summary>
        /// <param name="directroy">The directory you want to move it too</param>
        public override void Move(string destFileName)
        {
            if(Exists())
            {
                sIO.File.Move(path, destFileName);
            }
            path = destFileName;
        }

        /// <summary>
        /// Checks to see if this file exists on disk based on the
        /// path sent in.
        /// </summary>
        protected override bool Exists(string path)
        {
            return sIO.File.Exists(path);
        }

        /// <summary>
        /// Returns a new instance of this type.
        /// </summary>
        protected override IFile Internal_Create(string path)
        {
            return new File(path);
        }
    }
}
