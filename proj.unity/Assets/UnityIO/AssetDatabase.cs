#if !UNITY_EDITOR
#define RELEASE_BUILD
#else
using UnityEditor;
using UnityEditorInternal;
using uAssetDatabase = UnityEditor.AssetDatabase;
#endif

using System;
using Object = UnityEngine.Object;




namespace UnityIO
{
    public static class AssetDatabase
    {
        /// <summary>
        /// Returns the first asset object of type at given path assetPath.
        /// </summary>
        /// <typeparam name="T">The type you want to load</typeparam>
        /// <param name="path">The path of the asset</param>
        /// <returns>The loaded object</returns>
        public static T LoadAssetAtPath<T>(string assetPath) where T : Object
        {
#if !RELEASE_BUILD
            return uAssetDatabase.LoadAssetAtPath<T>(assetPath);
#endif
        }

        /// <summary>
        /// Returns the first asset object of type at given path assetPath.
        /// </summary>
        /// <param name="path">The path of the asset</param>
        /// <param name="type">The type you want to load</param>
        /// <returns>The loaded object</returns>
        public static Object LoadAssetAtPath(string assetPath, Type type)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.LoadAssetAtPath(assetPath, type);
#endif
        }

        /// <summary>
        /// Deletes the asset file at path.
        // Returns true if the asset has been successfully deleted, false if it doesn't exit or couldn't be removed.
        /// </summary>
        public static bool DeleteAsset(string assetPath)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.DeleteAsset(assetPath);
#endif
        }

        /// <summary>
        /// Creates a new unique path for an asset.
        /// </summary>
        public static string GenerateUniqueAssetPath(string path)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.GenerateUniqueAssetPath(path);
#endif
        }

        /// <summary>
        /// Duplicates the asset at path and stores it at newPath.
        /// </summary>
        /// <returns>Returns true if the copy was successful.</returns>
        public static bool CopyAsset(string path, string newPath)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.CopyAsset(path, newPath);
#endif
        }

        /// <summary>
        /// Returns true if the fileName is valid on the supported platform.
        /// </summary>
        /// <param name="fileName">The name of the file you want to check.</param>
        /// <returns>True if it's valid and false if it's not.</returns>
        public static bool IsValidFileName(string fileName)
        {
#if !RELEASE_BUILD
            return InternalEditorUtility.IsValidFileName(fileName);
#endif
        }

        /// <summary>
        /// returns true if it exists, false otherwise false.
        /// </summary>
        public static bool IsValidFolder(string path)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.IsValidFolder(path);
#endif
        }

        /// <summary>
        /// Checks if an asset file can be moved from one folder to another. (Without actually moving the file).
        /// </summary>
        /// <param name="oldPath">The path where the asset currently resides.</param>
        /// <param name="newPath">The path which the asset should be moved to.</param>
        /// <returns>An empty string if the asset can be moved, otherwise an error message.</returns>
        public static string ValidateMoveAsset(string oldPath, string newPath)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.ValidateMoveAsset(oldPath, newPath);
#endif
        }

        /// <summary>
        /// Move an asset file from one folder to another.
        /// </summary>
        /// <param name="oldPath">The path where the asset currently resides.</param>
        /// <param name="newPath">The path which the asset should be moved to.</param>
        /// <returns></returns>
        public static string MoveAsset(string oldPath, string newPath)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.MoveAsset(oldPath, newPath);
#endif
        }

        /// <summary>
        /// Rename an asset file.
        /// </summary>
        /// <param name="pathName">The path where the asset currently resides.</param>
        /// <param name="newName">The new name which should be given to the asset.</param>
        /// <returns></returns>
        public static string RenameAsset(string pathName, string newName)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.RenameAsset(pathName, newName);
#endif
        }

        /// <summary>
        /// Create a new folder.
        /// </summary>
        /// <param name="parentFolder">The name of the parent folder.</param>
        /// <param name="newFolderName">The name of the new folder.</param>
        /// <returns>The GUID of the newly created folder.</returns>
        public static string CreateFolder(string parentFolder, string newFolderName)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.CreateFolder(parentFolder, newFolderName);
#endif
        }

        /// <summary>
        /// Search the asset database using the search filter string.
        /// </summary>
        /// <param name="filter">The filter string can contain search data. See below for details about this string.</param>
        /// <param name="searchInFolders">The folders where the search will start.</param>
        /// <returns>Array of matching asset. Note that GUIDs will be returned.</returns>
        public static string[] FindAssets(string filter, string[] searchInFolders)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.FindAssets(filter, searchInFolders);
#endif
        }

        /// <summary>
        /// Given an absolute path to a directory, this method will return an array of all it's subdirectories.
        /// </summary>
        public static string[] GetSubFolders(string path)
        {
#if !RELEASE_BUILD
            return uAssetDatabase.GetSubFolders(path);
#endif
        }

        /// <summary>
        /// Converts a system path to a project local one.
        /// </summary>
        public static string GetProjectRelativePath(string path)
        {
#if !RELEASE_BUILD
            return FileUtil.GetProjectRelativePath(path);
#endif
        }

        /// <summary>
        /// Creates a new asset at path.
        /// You must ensure that the path uses a supported extension ('.mat' for materials, '.cubemap' for cubemaps, '.GUISkin' 
        /// for skins, '.anim' for animations and '.asset' for arbitrary other assets.)
        /// Note: Most of the uses of this function don't work on a build.
        /// </summary>
        public static void CreateAsset<T>(T asset, string path) where T : Object
        {
#if !RELEASE_BUILD
            uAssetDatabase.CreateAsset(asset, path);
#endif
        }
    }
}
