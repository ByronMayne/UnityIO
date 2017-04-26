using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityIO
{
    public class PathUtility
    {
        /// <summary>
        /// Takes the last file or directory and renames it returning the result. You can send
        /// in a full path or just a file name.
        /// </summary>
        public static string Rename(string path, string newName)
        {
            // Make sure we are not null. 
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Path", "Was null and we can't rename a null file.");
            }

            // Make sure we have a name provided 
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentNullException("newName", "Was null and we can't rename an asset to a null string.");
            }

            // Get the lengths
            int nameStartIndex = path.LastIndexOf('/') + 1;
            // If it's -1 we don't have a directory (which is fine)
            if (nameStartIndex < 0) nameStartIndex = 0;
            // Now get the extension index
            int extensionIndex = path.LastIndexOf('.');
            extensionIndex = extensionIndex < nameStartIndex ? -1 : extensionIndex;
            // And get the length of our extension
            int extensionLength = extensionIndex < 0 ? 0 : path.Length - extensionIndex;
            // Now if we don't have an extension that is also okay (but weird). 
            int nameLength = newName.Length;
            // Lets build a string as a result 
            char[] pathBuilder = new char[nameStartIndex + nameLength + extensionLength];
            // Copy the start of the path
            path.CopyTo(0, pathBuilder, 0, nameStartIndex);
            // Copy our new name
            newName.CopyTo(0, pathBuilder, nameStartIndex, nameLength);
            // Add back on the extension
            if (extensionIndex > -1)
            {
                Debug.Log(path);
                Debug.Log(extensionIndex);
                path.CopyTo(extensionIndex, pathBuilder, extensionIndex, extensionLength);
            }
            // Return the result
            return (new string(pathBuilder));
        }

        /// <summary>
        /// If a directory this returns the name of that directory. If a file path
        /// this returns the name of the file without it's extension. 
        /// </summary>
        public static string GetName(string path)
        {
            // Make sure we are not null. 
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Path", "Was null and we can't rename a null file.");
            }

            // Get the lengths
            int nameStartIndex = path.LastIndexOf('/') + 1;
            // Now get the extension index
            int extensionIndex = path.LastIndexOf('.');
            // If our extension is before our name start index we might have an extra period in our path
            // and we are a directory so we have to change that
            extensionIndex = extensionIndex < nameStartIndex ? path.Length : extensionIndex;
            // If we have no extension we just push it to the end
            extensionIndex = extensionIndex == -1 ? path.Length : extensionIndex;
            // Get our length
            int nameLength = extensionIndex - nameStartIndex;
            // Create a result
            char[] nameBuilder = new char[nameLength];
            // Copy the name over
            path.CopyTo(nameStartIndex, nameBuilder, 0, nameLength);
            // REturn the result 
            return new string(nameBuilder);
        }
    }
}

