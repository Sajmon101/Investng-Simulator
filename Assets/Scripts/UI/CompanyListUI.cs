using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CompanyListUI : MonoBehaviour
{
    [SerializeField] private CompanyRecord recordPrefab;
    [SerializeField] private Player player;

    private List<CompanyRecord> records = new List<CompanyRecord>();

    private void OnEnable()
    {
        EventManager.Instance.OnRecordMessage += HandleRecordMessageEvent;

        UpdatePanel();
    }

    private void HandleRecordMessageEvent(Company company, string msg, InfoMessageType type)
    {
        ShowRecordMessage(company, msg, type);
        UpdatePanel();
    }

    private void Start()
    {
        InitializeListUI(StockMarket.Instance.companies, player);
    }

    public void UpdatePanel()
    {
        foreach (var record in records)
            record.UpdateDisplay();
    }

    public void InitializeListUI(List<Company> companies, Player player)
    {
        foreach (Company company in companies)
        {
            CompanyRecord record = Instantiate(recordPrefab, transform);
            records.Add(record);
            record.company = company;
            record.InitializeCompanyData(company.companyName, company.stockPrice, 0);
            record.player = player;
        }
    }

    public void ShowRecordMessage(Company company, string message, InfoMessageType type)
    {
        CompanyRecord record = records.FirstOrDefault(r => r.company == company);
        if (record != null)
        {
            record.ShowMessage(message, type);
        }
    }
}

