using UnityIO.Interfaces;
using UnityEngine;
using UnityEditor;
using UnityIO.Exceptions;

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
            // Get our path. 
            string copyDir = AssetDatabase.GenerateUniqueAssetPath(m_Path);
            // Copy our asset
            Debug.Log(AssetDatabase.CopyAsset(m_Path, copyDir));
            // Return new IFile
            return new File(copyDir);
        }

        public IFile Duplicate(string newName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Renames this file to a new name. 
        /// </summary>
        public void Rename(string newName)
        {
            if (!UnityEditorInternal.InternalEditorUtility.IsValidFileName(newName))
            {
                throw new InvalidNameException("The name '" + newName + "' contains invalid characters");
            }

            if (newName.Contains("/"))
            {
                throw new RenameException("Rename can't be used to change a files location use Move(string newPath) instead.", m_Path, newName);
            }

            int slashIndex = m_Path.LastIndexOf('/') + 1;
            string subPath = m_Path.Substring(0, slashIndex);
            string newPath = subPath + newName;

            Object preExistingAsset = AssetDatabase.LoadAssetAtPath<Object>(newPath);

            if (preExistingAsset != null)
            {
                throw new FileAlreadyExistsException("Rename can't be completed since an asset already exists with that name at path " + newPath);
            }

            AssetDatabase.RenameAsset(m_Path, newName);
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
