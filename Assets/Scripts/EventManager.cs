using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public event Action OnNextRound;
    public event Action OnPrizeChange;

    public void NextRoundEvent()
    {
        OnNextRound?.Invoke();
    }

    public void PrizeChangeEvent()
    {
        OnPrizeChange?.Invoke();
    }
}
