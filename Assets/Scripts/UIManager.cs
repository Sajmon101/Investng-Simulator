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

    private void OnEnable()
    {
        EventManager.Instance.OnNextRound += HandleNextRoundEvent;
        EventManager.Instance.OnPrizeChange += HandlePrizeChangeEvent;
        EventManager.Instance.OnRecordMessage += HandleRecordMessageEvent;
    }

    private void HandleRecordMessageEvent(Company company, string msg, InfoMessageType type)
    {
        companyListPanel.ShowRecordMessage(company, msg, type);
        companyListPanel.UpdateCompaniesDisplay();
        playerCashPanel.UpdateDisplay();
    }

    private void HandlePrizeChangeEvent()
    {
        companyListPanel.UpdateCompaniesDisplay();
    }

    private void HandleNextRoundEvent()
    {
        ShowPanel(nextRoundPanel);
        roundNrPanel.IncreaseRoundNr();
    }

    public void InitializeUI(List<Company> companies)
    {
        companyListPanel.InitializeListUI(companies, player);
        playerCashPanel.UpdateDisplay();
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
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
