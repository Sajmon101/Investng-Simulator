using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] CompanyListUI companyListPanel;
    [SerializeField] GameObject nextRoundPanel;
    [SerializeField] Player player;
    [SerializeField] PlayerCashPanel playerCashPanel;
    [SerializeField] RoundNrPanel roundNrPanel;
    [SerializeField] EventLogPanel eventLogPanel;
    [SerializeField] RandomEventsPanel randomEventPanel;
    public RumorPanel rumorPanel;

    private void OnEnable()
    {
        RandomEventManager.Instance.OnRandomEventTriggered += HandleRandomEventTrigger;
        EventManager.Instance.OnNextRound += HandleNextRoundEvent;
        EventManager.Instance.OnPrizeChange += HandlePrizeChangeEvent;
        EventManager.Instance.OnRecordMessage += HandleRecordMessageEvent;
    }

    private void HandleRandomEventTrigger(IRandomEvent obj)
    {
        eventLogPanel.AddLog(obj.GetAlertDescription());
        randomEventPanel.SetPanelData(obj.GetAlertDescription());
    }

    private void HandleRecordMessageEvent(Company company, string msg, InfoMessageType type)
    {
        companyListPanel.ShowRecordMessage(company, msg, type);
        companyListPanel.UpdatePanel();
        playerCashPanel.UpdatePanel();
    }

    private void HandlePrizeChangeEvent()
    {
        companyListPanel.UpdatePanel();
    }

    private void HandleNextRoundEvent()
    {
        GameManager.Instance.IncreaseRoundNr();
    }

    public void InitializeCompaniesRecords(List<Company> companies)
    {
        companyListPanel.InitializeListUI(companies, player);
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);

        var initializable = panel.GetComponent<IUpdatablePanel>();
        if (initializable != null)
            initializable.UpdatePanel();
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNextRound -= HandleNextRoundEvent;
        EventManager.Instance.OnPrizeChange -= HandlePrizeChangeEvent;
    }
}
