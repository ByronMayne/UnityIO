using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityIO.BaseClasses;
using UnityIO.Exceptions;
using UnityIO.Interfaces;
using sIO = System.IO;

namespace UnityIO.Classes
{
    public class AssetPath : BaseFile, IFile
    {
        /// <summary>
        /// Returns the directory that this file exists in.
        /// </summary>
        public override IDirectory directory
        {
            get
            {
                return new AssetDirectory(m_Directory);
            }
        }

        /// <summary>
        /// Creates a new instance of a file. 
        /// </summary>
        public AssetPath(string path) : base(path)
        {
            // Nothing to do here
        }

        /// <summary>
        /// Delete this file from disk.
        /// </summary>
        public override void Delete()
        {
            // Deletes the asset
            AssetDatabase.DeleteAsset(path);
        }

        /// <summary>
        /// Creates a copy of this file with a new name. The new name should not contain the extension
        /// that will be preserved automatically. 
        /// </summary>
        /// <param name="newName">The new name of the file (excluding the extension)</param>
        /// <returns></returns>
        public override IFile Duplicate(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new System.ArgumentNullException("You can't send a empty or null string to rename an asset. Trying to rename " + path);
            }
            // Make sure we don't have an extension. 
            if (!string.IsNullOrEmpty(sIO.Path.GetExtension(newName)))
            {
                throw new InvalidNameException("When you duplicate an asset it should not have an extension " + newName);
            }
            // Make sure it's a valid name. 
            if (!InternalEditorUtility.IsValidFileName(newName))
            {
                throw new InvalidNameException("The name '" + newName + "' contains invalid characters");
            }
            // Get our current directory
            string directory = System.IO.Path.GetDirectoryName(path);
            // and the extension
            string extension = System.IO.Path.GetExtension(path);
            // Get our path. 
            string copyDir = AssetDatabase.GenerateUniqueAssetPath(directory + "/" + newName + extension);
            // Copy our asset
            AssetDatabase.CopyAsset(path, copyDir);
            // Return new IFile
            return new AssetPath(copyDir);
        }

        /// <summary>
        /// Moves the files from it's current directory to another. 
        /// </summary>
        /// <param name="directroy">The directory you want to move it too</param>
        public override void Move(string targetDirectory)
        {
            // Make sure we have a valid path
            IO.ValidatePath(targetDirectory);
            // And the directory exists
            if (!AssetDatabase.IsValidFolder(targetDirectory))
            {
                throw new DirectoryNotFoundException("Unable to find the directory at " + targetDirectory);
            }

            // Get the current name of our file.
            string name = System.IO.Path.GetFileName(path);

            // Append the name to the end. Move can't rename.
            targetDirectory = targetDirectory + "/" + name;

            // Check to see if there will be an error.
            string error = AssetDatabase.ValidateMoveAsset(path, targetDirectory);

            // CHeck
            if (!string.IsNullOrEmpty(error))
            {
                // We messed up.
                throw new MoveException(error, path, targetDirectory);
            }
            else
            {
                // Move it we are good to go.
                AssetDatabase.MoveAsset(path, targetDirectory);
            }
        }

        /// <summary>
        /// Checks to see if this file exists on disk based on the
        /// path sent in.
        /// </summary>
        protected override bool Exists(string path)
        {
            return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path) != null;
        }

        /// <summary>
        /// Returns a new instance of this type.
        /// </summary>
        protected override IFile Internal_Create(string path)
        {
            return new AssetPath(path);
        }
    }
}
