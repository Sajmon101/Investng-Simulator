using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StockMarket stockMarket;
    [SerializeField] UIManager uiManager;

    public static GameManager Instance { get; private set; }
    public event Action OnNextRound;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        uiManager.InitializeUI(stockMarket.companies);
    }

    public void NextRound()
    {
        OnNextRound?.Invoke();
    }
}
