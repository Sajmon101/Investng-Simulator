using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager Instance { get; private set; }
    public event Action<IRandomEvent> OnRandomEventTriggered;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool TryTriggerEvent(IRandomEvent gameEvent)
    {
        if (UnityEngine.Random.value < gameEvent.probability)
        {
            gameEvent.Apply();
            OnRandomEventTriggered?.Invoke(gameEvent);
            return true;
        }
        else
        {
            return false;
        }
    }
}
