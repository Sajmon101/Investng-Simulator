using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] CompanyListUI companyListPanel;
    [SerializeField] GameObject nextRoundPanel;

    private void OnEnable()
    {
        EventManager.Instance.OnNextRound += HandleNextRoundEvent;
        EventManager.Instance.OnPrizeChange += HandlePrizeChangeEvent;
    }

    private void HandlePrizeChangeEvent()
    {
        companyListPanel.UpdateCompaniesDisplay();
    }

    private void HandleNextRoundEvent()
    {
        ShowPanel(nextRoundPanel);
    }

    public void InitializeUI(List<Company> companies)
    {
        companyListPanel.InitializeListUI(companies);
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
