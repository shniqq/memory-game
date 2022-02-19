using TMPro;
using UnityEngine;

namespace MemoryGame.Game.Hud
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cardsLeftText;

        public int CardsLeft
        {
            set => _cardsLeftText.text = $"{value} cards left";
        }
    }
}