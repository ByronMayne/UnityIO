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