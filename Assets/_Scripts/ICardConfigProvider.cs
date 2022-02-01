using UnityEngine;

namespace MemoryGame
{
    public interface ICardConfigProvider
    {
        Sprite GetConfig(int id);
    }
}