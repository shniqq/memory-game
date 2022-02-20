using System;
using DG.Tweening;
using MemoryGame.Game.CardStack;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using Random = UnityEngine.Random;

namespace MemoryGame.Game.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Transform _cardRoot;
        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField, Range(0, 5)] private float _cardPlayDuration;
        [SerializeField, Range(0, 5f)] private float _introDuration;
        [SerializeField] private Ease _cardPlayEase;
        [SerializeField] private SpriteRenderer _frontBackground;
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private AudioClip[] _cardPlayAudioClips;
        [SerializeField] private AudioSource _audioSource;

        [Inject]
        public uint Index
        {
            set
            {
                _index = (int)value;
                _sortingGroup.sortingOrder = -(int)value;
            }
        }

        [Inject]
        public Sprite Icon
        {
            set => _icon.sprite = value;
        }

        [Inject] private Vector3 _position;
        [Inject] private ICardColorProvider _cardColorProvider;
        [Inject] private ICardStackConfig _cardStackConfig;

        private int _index;

        public IObservable<Unit> OnCompletedIntroAnimation => _onCompletedIntroAnimation;
        private readonly Subject<Unit> _onCompletedIntroAnimation = new();

        private void Awake()
        {
            _onCompletedIntroAnimation.AddTo(this);
            _cardRoot.position = Vector3.down * 30f + Vector3.back * 10f;

            _frontBackground.color = _cardColorProvider.GetCardColor(_index);
        }

        public void MoveCardToTheLeft()
        {
            var newPosition = _cardRoot.position;
            newPosition.x -= _cardStackConfig.Spacing;
            _cardRoot.position = newPosition;
        }

        public void PlayIntroAnimation()
        {
            DOTween.Sequence()
                .Insert(0f, _cardRoot.DOMoveY(_position.y, _introDuration * 0.6f).SetEase(Ease.OutBack))
                .InsertCallback(0f, () =>
                {
                    if (_index == 0)
                    {
                        PlayRandomCardSound();
                    }
                })
                .Insert(_introDuration * 0.6f,
                    _cardRoot.DOMoveX(_position.x, _introDuration * 0.3f).SetEase(Ease.Linear))
                .Insert(_introDuration * 0.6f,
                    _cardRoot.DOMoveZ(_position.z, _introDuration * 0.3f).SetEase(Ease.Linear))
                .InsertCallback(_introDuration * 0.6f, () =>
                {
                    if (_index == 0)
                    {
                        Invoke(nameof(PlayRandomCardSound), 0.1f);
                        Invoke(nameof(PlayRandomCardSound), 0.2f);
                        Invoke(nameof(PlayRandomCardSound), 0.3f);
                        Invoke(nameof(PlayRandomCardSound), 0.4f);
                        Invoke(nameof(PlayRandomCardSound), 0.5f);
                        Invoke(nameof(PlayRandomCardSound), 0.6f);
                        Invoke(nameof(PlayRandomCardSound), 0.7f);
                        Invoke(nameof(PlayRandomCardSound), 0.8f);
                    }
                })
                .OnComplete(() =>
                {
                    _onCompletedIntroAnimation.OnNext(Unit.Default);
                    _onCompletedIntroAnimation.OnCompleted();
                });
        }

        public void PlayCard()
        {
            PlayRandomCardSound();
            DOTween.Sequence().SetEase(_cardPlayEase)
                .Insert(0f, _cardRoot.DORotate(new Vector3(0, 180, 0), _cardPlayDuration, RotateMode.WorldAxisAdd))
                .Insert(0f, _cardRoot.DOMoveX(-_cardRoot.position.x, _cardPlayDuration))
                .InsertCallback(0.5f, () => _sortingGroup.sortingOrder = _index);
        }

        private void PlayRandomCardSound()
        {
            _audioSource.PlayOneShot(_cardPlayAudioClips[Random.Range(0, _cardPlayAudioClips.Length)]);
        }
    }
}