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
