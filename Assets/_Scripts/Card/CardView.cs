using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using Random = UnityEngine.Random;

namespace Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField, Range(0, 5)] private float _cardPlayDuration;
        [SerializeField] private Ease _cardPlayEase;
        [SerializeField] private SpriteRenderer _frontBackground;
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private AudioClip[] _cardPlayAudioClips;
        [SerializeField] private AudioSource _audioSource;

        [Inject]
        public int Index
        {
            set
            {
                _index = value;
                _sortingGroup.sortingOrder = -value;
            }
        }

        [Inject]
        public Sprite Icon
        {
            set => _icon.sprite = value;
        }

        [Inject] private Vector3 _position;
        [Inject] private ICardColorProvider _cardColorProvider;

        private int _index;

        private void Start()
        {
            transform.position = Vector3.down * 30f + Vector3.back * 10f;

            _frontBackground.color = _cardColorProvider.GetCardColor(_index);
        }

        public void PlayIntroAnimation(float duration)
        {
            DOTween.Sequence()
                .Insert(0f, transform.DOMoveY(_position.y, duration * 0.6f).SetEase(Ease.OutBack))
                .InsertCallback(0f, () =>
                {
                    if (_index == 0)
                    {
                        PlayRandomCardSound();
                    }
                })
                .Insert(duration * 0.6f, transform.DOMoveX(_position.x, duration * 0.3f).SetEase(Ease.Linear))
                .Insert(duration * 0.6f, transform.DOMoveZ(_position.z, duration * 0.3f).SetEase(Ease.Linear))
                .InsertCallback(duration * 0.6f, () =>
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
                });
        }

        public void PlayCard()
        {
            PlayRandomCardSound();
            DOTween.Sequence().SetEase(_cardPlayEase)
                .Insert(0f, transform.DORotate(new Vector3(0, 180, 0), _cardPlayDuration, RotateMode.WorldAxisAdd))
                .Insert(0f, transform.DOMoveX(-transform.position.x, _cardPlayDuration))
                .InsertCallback(0.5f, () => _sortingGroup.sortingOrder = _index);
        }

        private void PlayRandomCardSound()
        {
            _audioSource.PlayOneShot(_cardPlayAudioClips[Random.Range(0, _cardPlayAudioClips.Length)]);
        }
    }
}