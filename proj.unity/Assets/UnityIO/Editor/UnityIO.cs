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

using UnityEngine;
using UnityIO.Interfaces;
using UnityIO.Classes;

namespace UnityIO
{

    public class IO
    {
		/// <summary>
		/// A list of chars that are not valid for file names in Unity. 
		/// </summary>
		public static readonly char[] INVALID_FILE_NAME_CHARS = new char[]{'/', '\\', '<', '>', ':', '|', '"'};
		/// <summary>
		/// The char we use to split our paths.
		/// </summary>
        public const char PATH_SPLITTER = '/';

        /// <summary>
        /// A function used to make sure that paths being sent to UnityIO are the correct type. One common
        /// problem with Unity's own file system is that every function expects a different format. So if
        /// we force the user to way and explode with fire every other way it will become easier to use. This
        /// will throw exceptions if the path is not valid.
        /// </summary>
        /// <param name="path">The path you want to check.</param>
        public static void ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new System.IO.IOException("UnityIO. A path can not be null or empty when searching the project");
            }

            if (path[path.Length - 1] == '/')
            {
                throw new System.IO.IOException("UnityIO: All directory paths are expected to not end with a leading slash. ( i.e. the '/' character )");
            }
        }

		/// <summary>
		/// Checks to see if the file name contains any invalid chars that Unity does not accept.
		/// </summary>
		/// <remarks>Path.GetInvalidFileNameChars() works on Windows but only returns back '/' on Mac so we have to make our own version.</remarks>
		/// <returns><c>true</c> if is valid file name otherwise, <c>false</c>.</returns>
		/// <param name="name">Name.</param>
		public static bool IsValidFileName(string name)
		{
			for(int i = 0; i < INVALID_FILE_NAME_CHARS.Length; i++)
			{
				if(name.IndexOf(INVALID_FILE_NAME_CHARS[i]) != -1)
				{
					return false;
				}
			}
			return true;
		}

        /// <summary>
        /// Gets the root directory for the Unity projects asset folder.
        /// </summary>
        public static IDirectory Root
        {
            get
            {
                return new Directory("Assets");
            }

        }
    }
}