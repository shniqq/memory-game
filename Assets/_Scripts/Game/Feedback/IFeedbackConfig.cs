namespace MemoryGame.Game.Feedback
{
    public interface IFeedbackConfig
    {
        float QuickFeedbackThreshold { get; }
        float VeryQuickFeedbackThreshold { get; }
        int StreakFeedbackThreshold { get; }
    }
}