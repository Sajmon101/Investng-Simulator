using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StockMarket stockMarket;
    [SerializeField] UIManager uiManager;
    [SerializeField] RumorManager rumorManager;
    [SerializeField] MainGamePanel mainGamePanel;

    public static GameManager Instance { get; private set; }
    public int roundNr = 0;

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

        IRandomEvent randomEvent = new SectorEvent();
        if (!RandomEventManager.Instance.TryTriggerEvent(randomEvent))
        {
            randomEvent = new SingleCompanyEvent();
            RandomEventManager.Instance.TryTriggerEvent(randomEvent);
        }
    }

    public void IncreaseRoundNr()
    {
        roundNr++;
    }
}
