using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[FilePath(SettingsAssetPath, FilePathAttribute.Location.ProjectFolder)]
internal class SceneUtilities : ScriptableSingleton<SceneUtilities>
{
    private const string SettingsAssetPath = "ProjectSettings/Scene Utilities/Settings.asset";
    private static readonly string SettingsPath = "Project/Scene Utilities";

    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider()
    {
        var provider = new SettingsProvider(SettingsPath, SettingsScope.Project)
        {
            guiHandler = _ => { DrawSettings(); },
            keywords = new HashSet<string>(new[] { "Scenes", "Editor", "Utils" })
        };

        return provider;
    }

    [SerializeField] private SceneAsset _sceneAsset;

    private static int _sceneIndex;

    private static List<(string Path, SceneAsset Asset)> _scenes = new();

    private static void DrawSettings()
    {
        if (GUILayout.Button("Refresh Scenes"))
        {
            RefreshScenes();
        }

        _sceneIndex = EditorGUILayout.Popup(_sceneIndex, _scenes.Select(e => e.Path).ToArray());

        if (GUILayout.Button("Save"))
        {
            instance._sceneAsset = _scenes[_sceneIndex].Asset;
            ApplyStartScene();
            instance.Save(true);
        }
    }

    [InitializeOnLoadMethod]
    private static void ApplyStartScene()
    {
        if (instance._sceneAsset != null)
        {
            EditorSceneManager.playModeStartScene = instance._sceneAsset;
        }
    }

    [InitializeOnLoadMethod]
    private static void RefreshScenes()
    {
        _scenes = AssetDatabase.FindAssets($"t:{nameof(SceneAsset)}")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(e => (e, AssetDatabase.LoadAssetAtPath<SceneAsset>(e)))
            .ToList();
        _sceneIndex = _scenes.FirstOrDefault(e => e.Asset == instance._sceneAsset) != default
            ? _scenes.IndexOf(_scenes.FirstOrDefault(e => e.Asset == instance._sceneAsset))
            : 0;
    }
}