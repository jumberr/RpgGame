using System.IO;
using UnityEditor;
using UnityEngine;

namespace _Project.CodeBase.Editor
{
    public class DeleteSaves : EditorWindow
    {
        [MenuItem("Tools/Delete Saves")]
        private static void RemoveSaves() => 
            File.Delete(Path.Combine($"{Application.persistentDataPath}", "Progress"));
    }
}