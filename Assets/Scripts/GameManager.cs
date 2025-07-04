using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StockMarket stockMarket;
    [SerializeField] UIManager uiManager;
    [SerializeField] RumorManager rumorManager;
    [SerializeField] MainGamePanel mainGamePanel;

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
        uiManager.InitializeCompaniesRecords(stockMarket.companies);
        rumorManager.LoadRumors();
        rumorManager.DrawNewRumor();
        uiManager.rumorPanel.UpdatePanel();
        mainGamePanel.UpdatePanel();
    }

    public void OnBtnNextRound()
    {
        EventManager.Instance.NextRoundEvent();
    }
}
