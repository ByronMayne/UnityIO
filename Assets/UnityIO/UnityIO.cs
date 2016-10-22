using UnityEngine;
using UnityIO.Interfaces;
using UnityIO.Classes;

namespace UnityIO
{

    public class IO : MonoBehaviour
    {
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