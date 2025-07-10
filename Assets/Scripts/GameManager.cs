using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StockMarket stockMarket;
    [SerializeField] RumorManager rumorManager;
    [SerializeField] MainGamePanel mainGamePanel;
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
        rumorManager.LoadRumors();
        rumorManager.DrawNewRumor();
    }

    public void OnBtnNextRound()
    {
        
        EventManager.Instance.NextRoundEvent();

        SectorEvent randomEvent = new SectorEvent();
        if (!RandomEventManager.Instance.TryTriggerEvent(randomEvent))
        {
            SingleCompanyEvent randomEvent2 = new SingleCompanyEvent();
            RandomEventManager.Instance.TryTriggerEvent(randomEvent2);
        }
    }

    public void IncreaseRoundNr()
    {
        roundNr++;
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
