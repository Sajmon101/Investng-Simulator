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
    public event Action OnPlayerCashChanged;
    public event Action<Company> OnBuyButtonClicked;
    public event Action<Company> OnSellButtonClicked;
    public event Action<Company, string, InfoMessageType> OnRecordMessage;

    public void NextRoundEvent()
    {
        OnNextRound?.Invoke();
    }

    public void BuyButtonClickedEvent(Company company)
    {
        OnBuyButtonClicked?.Invoke(company);
    }

    public void SellButtonClickedEvent(Company company)
    {
        OnSellButtonClicked?.Invoke(company);
    }

    public void RecordMessageEvent(Company company, string msg, InfoMessageType infoMessageType)
    {
        OnRecordMessage?.Invoke(company, msg, infoMessageType);
    }
    public void PlayerCashChangedEvent()
    {
        OnPlayerCashChanged?.Invoke();
    }
}
