using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame
{
    public class DecisionInputView : MonoBehaviour
    {
        [SerializeField] private Button _equalDecisionButton;
        [SerializeField] private Button _differentDecisionButton;

        public Button EqualDecisionButton => _equalDecisionButton;
        public Button DifferentDecisionButton => _differentDecisionButton;
    }
}