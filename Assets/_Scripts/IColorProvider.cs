using UnityEngine;

public interface IColorProvider
{
    public Color GetBackgroundColor();
    public Color GetCardColor(int index);
}