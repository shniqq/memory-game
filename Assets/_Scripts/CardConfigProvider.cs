using System;
using System.Linq;
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
}