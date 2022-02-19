using UnityEngine;

namespace MemoryGame.Game.Feedback
{
    [CreateAssetMenu(menuName = "Game/Feedback Config")]
    public class FeedbackConfig : ScriptableObject, IFeedbackConfig
    {
        [SerializeField] private float _quickFeedbackThreshold;
        [SerializeField] private float _veryQuickFeedbackThreshold;
        [SerializeField] private int _streakFeedbackThreshold;

        public float QuickFeedbackThreshold => _quickFeedbackThreshold;
        public float VeryQuickFeedbackThreshold => _veryQuickFeedbackThreshold;
        public int StreakFeedbackThreshold => _streakFeedbackThreshold;
    }
}