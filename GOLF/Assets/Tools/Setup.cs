using System.IO;
using UnityEngine;
using UnityEditor;
using static System.IO.Path;
using static System.IO.Directory;
using static UnityEditor.AssetDatabase;

public static class Setup
{
    [MenuItem("Tools/Setup/Add base folders")]
    public static void DefaultFolders()
    {
        Folders.CreateDefaultFolder("_Project", "Animations", "Art", "Materials", "Prefabs", "Scripts", "Settigns");
        Refresh();
    }

    static class Folders
    {
        public static void CreateDefaultFolder(string root, params string[] folders)
        {
            var fullpath = Combine(Application.dataPath, root);
            foreach (var folder in folders)
            {
                var path = Combine(fullpath, folder);
                if (!Exists(path))
                {
                    CreateDirectory(path);
                }
            }
        }
    }
}