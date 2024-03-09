using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneLoaderEditor : EditorWindow
{
    private static List<string> scenePaths;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        LoadScenePaths();
    }

    private static void LoadScenePaths()
    {
        scenePaths = new List<string>();
        string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });

        foreach (string guid in guids)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(guid);
            scenePaths.Add(scenePath);
        }
    }

    [MenuItem("CrowAndRose/Load Scene %q")]
    private static void OpenScene()
    {
        if (scenePaths.Count > 0)
        {
            GenericMenu menu = new GenericMenu();

            foreach (var scenePath in scenePaths)
            {
                menu.AddItem(new GUIContent(scenePath), false, OpenSelectedScene, scenePath);
            }

            menu.ShowAsContext();
        }
        else
        {
            Debug.LogWarning("No scenes found in the project.");
        }
    }

    private static void OpenSelectedScene(object scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene((string)scenePath);
        }
    }
}
