using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StockMarket stockMarket;
    [SerializeField] RumorManager rumorManager;
    [SerializeField] MainGamePanel mainGamePanel;
    [SerializeField] EventLogPanel eventLogPanel;
    [SerializeField] Player player;

    public static GameManager Instance { get; private set; }
    public int roundNr = 1;
    public int maxRoundNr = 3;

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
        player.SetupPlayerStocks(stockMarket.companies);
    }

    public void OnBtnNextRound()
    {
        
        //EventManager.Instance.NextRoundEvent();

        player.SaveCurrentRoundStats();
        eventLogPanel.AddLog(player.GetLog());
        stockMarket.UpdateCompaniesPrices();
        player.ResetPreviosRoundStocks();
        roundNr++;

        TriggerRandomEvent();
    }

    private void TriggerRandomEvent()
    {
        int eventIndex = UnityEngine.Random.Range(0, 2);

        if (eventIndex == 0)
        {
            SectorEvent sectorEvent = new SectorEvent();
            RandomEventManager.Instance.TryTriggerEvent(sectorEvent);
        }
        else
        {
            SingleCompanyEvent singleCompanyEvent = new SingleCompanyEvent();
            RandomEventManager.Instance.TryTriggerEvent(singleCompanyEvent);
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
