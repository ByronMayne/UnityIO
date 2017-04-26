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
using UnityIO.Interfaces;
using sIO = System.IO;

namespace UnityIO.BaseClasses
{
    public abstract class BaseFile : IOBase, IFile
    {
        protected string m_Directory;
        protected string m_Extension;
        protected string m_Name;

        /// <summary>
        /// Gets the name of this file. 
        /// </summary>
        public string name
        {
            get
            {
                return m_Name + '.' + m_Extension; ;
            }

        }

        /// <summary>
        /// Returns back the directory that this file exists in.
        /// </summary>
        public abstract IDirectory directory
        {
            get;
        } 

        /// <summary>
        /// Gets the extension of this file. 
        /// </summary>
        public string extension
        {
            get
            {
                return  m_Extension;
            }
            protected set
            {
                m_Extension = value;
            }
        }

        /// <summary>
        /// Gets the name of this file without it's extension. 
        /// </summary>
        public string nameWithoutExtension
        {
            get
            {
                return m_Name;
            }
            protected set
            {
                m_Name = value;
            }
        }

        /// <summary>
        /// Creates a new base file. 
        /// </summary>
        public BaseFile(string path) : base(path)
        {
            m_Extension = sIO.Path.GetExtension(path);
            m_Name = sIO.Path.GetFileNameWithoutExtension(path);
            m_Directory = sIO.Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Makes a copy of this file on disk of it exists.
        /// </summary>
        public IFile Duplicate()
        {
            return Duplicate(MakeUnique().name);
        }

        /// <summary>
        /// Copies a file and renames it. If the name already is being used
        /// a unique one will be generated.
        /// </summary>
        public abstract IFile Duplicate(string newName);

        /// <summary>
        /// If this file already exists on disk a new name will be given to this file. A number
        /// will be appended to the end.
        /// Example: 'Dog.png' would become 'Dog_01.png'
        /// </summary>
        /// <returns></returns>
        public IFile MakeUnique()
        {
            // Create a copy. 
            BaseFile current = Internal_Create(path) as BaseFile;

            // 5000 is just a random big number. We don't want to search forever. 
            for (int i = 0; i < 5000 && !current.Exists(); i++)
            {
                // Create the new path 
                current.m_Name = name + "_" + i.ToString("00");
            }

            return current;
        }


        /// <summary>
        /// Returns back a File. This is used so that we don't have to use Generics. 
        /// </summary>
        protected abstract IFile Internal_Create(string path);

    }
}
