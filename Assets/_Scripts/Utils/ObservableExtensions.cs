using System;
using UniRx;

namespace MemoryGame.Utils
{
    public static class ObservableExtensions
    {
        public static IObservable<bool> IfSwitchedToTrue(this IObservable<bool> observable)
        {
            return observable.Pairwise().Where(pair => !pair.Previous && pair.Current).Select(pair => pair.Current);
        }
        public static IObservable<bool> IfSwitchedToFalse(this IObservable<bool> observable)
        {
            return observable.Pairwise().Where(pair => pair.Previous && !pair.Current).Select(pair => pair.Current);
        }
    }
}