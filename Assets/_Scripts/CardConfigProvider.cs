using System.Linq;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Animal Card Provider")]
public class CardConfigProvider : ScriptableObject, ICardConfigProvider
{
    [SerializeField] private Sprite[] _sprites;

    private int _lastCard;

    public Sprite GetConfig(int id)
    {
        return _sprites.ElementAt(id);
    }

#if UNITY_EDITOR
    [Button(enabledMode: EButtonEnableMode.Editor), UsedImplicitly]
    private void FillConfigFromFolder()
    {
        var path = UnityEditor.EditorUtility.OpenFolderPanel("Select Folder to add Sprites from", Application.dataPath, "");
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        var pathToSearchIn = path.Replace(Application.dataPath, string.Empty);
        pathToSearchIn = "Assets" + pathToSearchIn;
        var foundAssets = UnityEditor.AssetDatabase.FindAssets("t:sprite", new[] { pathToSearchIn });
        _sprites = foundAssets
            .Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
            .Select(UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>)
            .ToArray();
    }
#endif
}