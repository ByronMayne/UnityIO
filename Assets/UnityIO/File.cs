using UnityIO.Interfaces;
using UnityEngine;
using UnityEditor;

namespace UnityIO.Classes
{
    public class File : IFile
    {
        private string m_Path; 

        public string Path
        {
            get { return m_Path; }
        }

        public File(string path)
        {
            m_Path = path;
        }

        public IDirectory Directory
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        public IFile Duplicate()
        {
            throw new System.NotImplementedException();
        }

        public IFile Duplicate(string newName)
        {
            throw new System.NotImplementedException();
        }

        public void Rename(string name)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Loads the Unity asset at the files path. 
        /// </summary>
        /// <returns>Returns the asset</returns>
        public UnityEngine.Object LoadAsset()
        {
            return AssetDatabase.LoadAssetAtPath(m_Path, typeof(Object));
        }

        /// <summary>
        /// Loads the Unity asset at the files path. 
        /// </summary>
        /// <returns>Returns the asset</returns>
        public T LoadAsset<T>() where T : UnityEngine.Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(m_Path);
        }
    }
}
