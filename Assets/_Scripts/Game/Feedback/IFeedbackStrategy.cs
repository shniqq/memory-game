namespace MemoryGame.Game.Feedback
{
    public interface IFeedbackStrategy
    {
        FeedbackType[] Types { get; }
        void ShowFeedback(FeedbackType feedback);
    }
}