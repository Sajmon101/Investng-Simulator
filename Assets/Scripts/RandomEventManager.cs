using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager Instance { get; private set; }
    public event Action<IGameLogs> OnRandomEventTriggered;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool TryTriggerEvent<T>(T gameEvent) where T : IRandomEvent, IGameLogs
    {
        if (UnityEngine.Random.value < gameEvent.probability)
        {
            if (gameEvent.Apply())
            {
                OnRandomEventTriggered?.Invoke(gameEvent);
                return true;
            }

            return false;
        }
        else
        {
            return false;
        }
    }
}
