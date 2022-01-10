using UnityEngine;
using Zenject;

public class CardStack : MonoBehaviour, IInitializable
{
    [SerializeField] private uint _cardAmount;
    [SerializeField, Range(0, 0.5f)] private float _spacing;

    [Inject] private Card.CardFactory _cardFactory;
    
    public void Initialize()
    {
        for (var i = 0; i < _cardAmount; i++)
        {
            var card = _cardFactory.Create(i);
            card.transform.position = Vector3.right * _spacing * i;
        }
    }
}