using UnityIO.Interfaces;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityIO.Classes
{
    public class Files : List<File>, IFiles
    {
        /// <summary>
        /// Returns a list of all assets contained within that
        /// are of the type T.
        /// </summary>
        public List<T> LoadAllofType<T>() where T : Object
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
    }
}
