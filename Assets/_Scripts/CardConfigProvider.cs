using System;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Game/Animal Card Provider")]
public class CardConfigProvider : ScriptableObject, ICardConfigProvider
{
    [SerializeField] private Sprite[] _sprites;

    private int _lastCard;

    public Tuple<int, Sprite> GetConfig()
    {
        var replayLastCard = Random.Range(0, 3) > 1;
        var id = replayLastCard ? _lastCard : Random.Range(0, _sprites.Length);
        _lastCard = id;
        return new Tuple<int, Sprite>(id, _sprites.ElementAt(id));
    }

#if UNITY_EDITOR
    [Button(enabledMode: EButtonEnableMode.Editor)]
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