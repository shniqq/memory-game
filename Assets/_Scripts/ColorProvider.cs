using UnityEngine;

[CreateAssetMenu(menuName = "Game/Color Provider")]
public class ColorProvider : ScriptableObject, IColorProvider
{
    [SerializeField] private Color[] _cardColors;
    [SerializeField] private Color _backgroundColor;

    public Color GetBackgroundColor()
    {
        return _backgroundColor;
    }

    public Color GetCardColor(int index)
    {
        return _cardColors[(int)Mathf.Repeat(index, _cardColors.Length)];
    }
}