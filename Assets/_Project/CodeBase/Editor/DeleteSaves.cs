using System.IO;
using UnityEditor;
using UnityEngine;

namespace _Project.CodeBase.Editor
{
    public class DeleteSaves : EditorWindow
    {
        [MenuItem("Tools/Delete Saves")]
        private static void RemoveSaves()
        {
            Debug.Log("Deleted saves");

            foreach (var file in Directory.GetFiles(Application.persistentDataPath)) 
                File.Delete(file);
        }
    }
}