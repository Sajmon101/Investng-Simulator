using UnityEngine;

public interface IRandomEvent
{
    bool Apply();
    float probability { get; }
}
