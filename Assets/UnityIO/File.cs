using UnityIO.Interfaces;
using UnityEngine;
using UnityEditor;
using UnityIO.Exceptions;
using sIO = System.IO;
using UnityEditorInternal;

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
            AssetDatabase.CopyAsset(m_Path, copyDir);
            // Return new IFile
            return new File(copyDir);
        }

        /// <summary>
        /// Creates a copy of this file with a new name. The new name should not contain the extension
        /// that will be preserved automatically. 
        /// </summary>
        /// <param name="newName">The new name of the file (excluding the extension)</param>
        /// <returns></returns>
        public IFile Duplicate(string newName)
        {
            if(string.IsNullOrEmpty(newName))
            {
                throw new System.ArgumentNullException("You can't send a empty or null string to rename an asset. Trying to rename " + m_Path);
            }
            // Make sure we don't have an extension. 
            if(!string.IsNullOrEmpty(sIO.Path.GetExtension(newName)))
            {
                throw new InvalidNameException("When you duplicate an asset it should not have an extension " + newName);
            }
            // Make sure it's a valid name. 
            if (!InternalEditorUtility.IsValidFileName(newName))
            {
                throw new InvalidNameException("The name '" + newName + "' contains invalid characters");
            }
            // Get our current directory
            string directory = System.IO.Path.GetDirectoryName(m_Path);
            // and the extension
            string extension = System.IO.Path.GetExtension(m_Path);
            // Get our path. 
            string copyDir = AssetDatabase.GenerateUniqueAssetPath(directory + "/" + newName + extension);
            // Copy our asset
            Debug.Log(AssetDatabase.CopyAsset(m_Path, copyDir));
            // Return new IFile
            return new File(copyDir);
        }

        /// <summary>
        /// Moves the files from it's current directory to another. 
        /// </summary>
        /// <param name="directroy">The directory you want to move it too</param>
        public void Move(string targetDirectory)
        {
            // Make sure we have a valid path
            IO.ValidatePath(targetDirectory);
            // And the directory exists
            if(!AssetDatabase.IsValidFolder(targetDirectory))
            {
                throw new DirectoryNotFoundException("Unable to find the directory at " + targetDirectory);
            }

            // Get the current name of our file.
            string name = System.IO.Path.GetFileName(m_Path);

            // Append the name to the end. Move can't rename.
            targetDirectory = targetDirectory + "/" + name;

            // Check to see if there will be an error.
            string error = AssetDatabase.ValidateMoveAsset(m_Path, targetDirectory);

            // CHeck
            if (!string.IsNullOrEmpty(error))
            {
                // We messed up.
                throw new MoveException(error, m_Path, targetDirectory);
            }
            else
            {
                // Move it we are good to go.
                AssetDatabase.MoveAsset(m_Path, targetDirectory);
            }
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
