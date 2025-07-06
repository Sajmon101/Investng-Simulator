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
    [SerializeField] EndPanel endPanel;
    [SerializeField] MainGamePanel mainGamePanel;
    [SerializeField] EfectPanel efectPanel;
    public RumorPanel rumorPanel;

    private void OnEnable()
    {
        RandomEventManager.Instance.OnRandomEventTriggered += HandleRandomEventTrigger;
        EventManager.Instance.OnNextRound += HandleNextRoundEvent;
        EventManager.Instance.OnPrizeChange += HandlePrizeChangeEvent;
        EventManager.Instance.OnRecordMessage += HandleRecordMessageEvent;
    }

    private void HandleRandomEventTrigger(IGameLogs obj)
    {
        eventLogPanel.AddLog(obj.GetLog());
        randomEventPanel.SetPanelData(obj.GetLog().message);
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

        randomEventPanel.Clear();
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

    public void ShowEffectOrEndPanel()
    {
        if (GameManager.Instance.roundNr <= GameManager.Instance.maxRoundNr)
        {
            HidePanel(mainGamePanel.gameObject);
            ShowPanel(efectPanel.gameObject);
        }
        else
        {
            HidePanel(mainGamePanel.gameObject);
            ShowPanel(endPanel.gameObject);
            //EventManager.Instance.GameEndEvent();
        }
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
