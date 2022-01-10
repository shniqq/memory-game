using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class Card : MonoBehaviour
{
    public class CardFactory : PlaceholderFactory<int, Card>
    {
    }

    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField, Range(0, 5)] private float _cardPlayDuration;
    [SerializeField] private Ease _cardPlayEase;

    [Inject]
    private int Index
    {
        set
        {
            _index = value;
            _sortingGroup.sortingOrder = -value;
        }
    }
    private int _index;

    private void Start()
    {
        Invoke(nameof(PlayCard), _index * 1.2f);
    }

    public void PlayCard()
    {
        DOTween.Sequence().SetEase(_cardPlayEase)
            .Insert(0f, transform.DORotate(new Vector3(0, 180, 0), _cardPlayDuration, RotateMode.WorldAxisAdd))
            .Insert(0f, transform.DOMoveX(-transform.position.x, _cardPlayDuration))
            .InsertCallback(0.5f, () => _sortingGroup.sortingOrder = _index);
    }
}