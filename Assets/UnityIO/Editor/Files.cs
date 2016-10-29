/*>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
UnityIO was released with an MIT License.
Arther: Byron Mayne
Twitter: @ByMayne


IT License

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
using UnityEngine;

namespace UnityIO.Classes
{
    public class Files : List<File>, IFiles
    {
        /// <summary>
        /// Returns a list of all assets contained within that
        /// are of the type T.
        /// </summary>
        public IList<T> LoadAllofType<T>() where T : Object
        {
            List<T> result = new List<T>();
            for (int i = 0; i < Count; i++)
            {
                T loadedObj = this[i].LoadAsset<T>();
                if (loadedObj != null)
                {
                    result.Add(loadedObj);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns the first file in the list of files or if no files
        /// exist returns a null file.
        /// </summary>
        public IFile FirstOrDefault()
        {
            if (Count > 0)
            {
                return this[0];
            }
            else
            {
                return NullFile.SHARED_INSTANCE;
            }
        }
    }
}
