using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MemoryGame
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _backgroundAudioClips;
        [SerializeField] private AudioSource _backgroundMusicSource;
    
    
        private void Awake()
        {
            _backgroundMusicSource.clip = _backgroundAudioClips[Random.Range(0, _backgroundAudioClips.Length)];
            _backgroundMusicSource.DOFade(1f, 1f).ChangeStartValue(0f).SetEase(Ease.InQuad);
            _backgroundMusicSource.Play();
        }
    }
}