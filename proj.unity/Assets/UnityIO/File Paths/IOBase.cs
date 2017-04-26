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
using UnityIO.Exceptions;
using UnityIO.Interfaces;

namespace UnityIO.BaseClasses
{
    public abstract class IOBase
    {
        private string m_Path;

        /// <summary>
        /// Gets the full path of this object. 
        /// </summary>
        public string path
        {
            get { return m_Path; }
            protected set { m_Path = value; }
        }

        /// <summary>
        /// Creates a new instance of the base directory class.
        /// </summary>
        public IOBase(string path)
        {
            // Set our path. 
            this.path = path;
        }

        /// <summary>
        /// Deletes this object file disk. 
        /// </summary>
        public abstract void Delete();

        /// <summary>
        /// Returns true if this object exists on disk. 
        /// </summary>
        public bool Exists()
        {
            return Exists(path);
        }

        /// <summary>
        /// Checks to see if this object exists on disk given a path.
        /// </summary>
        protected abstract bool Exists(string path);

        /// <summary>
        /// Moves this object to target directory.
        /// </summary>
        public void Move(IDirectory targetDirectory)
        {
            Move(targetDirectory.path);
        }

        /// <summary>
        /// Moves this object to target directory.
        /// </summary>
        public abstract void Move(string targetDirectory);

        /// <summary>
        /// Renames this object. The new name must not be a path.
        /// </summary>
        public void Rename(string newName)
        {
            // Creates the new name 
            string dstDirectory = PathUtility.Rename(path, newName);
            // We already exist so we should throw an exception
            if(Exists(dstDirectory))
            {
                throw new DirectroyAlreadyExistsException("The directory can't be renamed to '" + dstDirectory + " because it already exists");
            }
            // Invokes Move
            Move(dstDirectory);
        }


        /// <summary>
        /// Returns back a string representation of this object as it's path.
        /// </summary>
        public override string ToString()
        {
            return path;
        }
        
        /// <summary>
        /// Returns back the hash code of this objects path.
        /// </summary>
        public override int GetHashCode()
        {
            return path.GetHashCode();
        }

        /// <summary>
        /// A test used to check if two directory classes point to the same class.
        /// </summary>
        public static bool operator ==(IOBase lhs, IOBase rhs)
        {
            return string.CompareOrdinal(lhs.path, rhs.path) == 0;
        }

        /// <summary>
        /// A test used to check if two directory classes don't point to the same type.
        /// </summary>
        public static bool operator !=(IOBase lhs, IOBase rhs)
        {
            return string.CompareOrdinal(lhs.path, rhs.path) == 0;
        }

        /// <summary>
        /// Allows us to implicitly convert a directory to a string.
        /// </summary>
        public static implicit operator string(IOBase directory)
        {
            return directory.path;
        }

        /// <summary>
        /// Returns back if the two objects have the same path. 
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                // Cast the object
                IOBase asBase = obj as IOBase;
                // Check if it's null
                if (asBase != null)
                {
                    return asBase.path.Equals(path, StringComparison.Ordinal);
                }
            }
            return false;
        }
    }
}
