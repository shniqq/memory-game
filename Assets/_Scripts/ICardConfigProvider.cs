using System;
using UnityEngine;

public interface ICardConfigProvider
{
    Tuple<int, Sprite> GetConfig();
}