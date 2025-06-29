using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StockMarket stockMarket;
    [SerializeField] UIManager uiManager;

    public static GameManager Instance { get; private set; }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        uiManager.InitializeUI(stockMarket.companies);
    }

    public void OnBtnNextRound()
    {
        EventManager.Instance.NextRoundEvent();
    }
}
