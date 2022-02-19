using UnityEngine;

namespace MemoryGame.Game.Card
{
    public interface ICardConfigProvider
    {
        Sprite GetConfig(int id);
    }
}