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
    public RumorPanel rumorPanel;

    private void OnEnable()
    {
        EventManager.Instance.OnNextRound += HandleNextRoundEvent;
        EventManager.Instance.OnPrizeChange += HandlePrizeChangeEvent;
        EventManager.Instance.OnRecordMessage += HandleRecordMessageEvent;
    }

    private void HandleRecordMessageEvent(Company company, string msg, InfoMessageType type)
    {
        companyListPanel.ShowRecordMessage(company, msg, type);
        companyListPanel.UpdatePanel();
        playerCashPanel.UpdateDisplay();
    }

    private void HandlePrizeChangeEvent()
    {
        companyListPanel.UpdatePanel();
    }

    private void HandleNextRoundEvent()
    {
        roundNrPanel.IncreaseRoundNr();
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
