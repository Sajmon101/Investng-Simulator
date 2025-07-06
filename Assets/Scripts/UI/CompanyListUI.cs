using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CompanyListUI : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] private CompanyRecord recordPrefab;
    [SerializeField] private RectTransform firstRecordPoint;
    Vector2 offset = new Vector2(0, 0);
    int recordSpace = 90;
    private List<CompanyRecord> records = new List<CompanyRecord>();

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
            //record.GetComponent<RectTransform>().anchoredPosition = firstRecordPoint.GetComponent<RectTransform>().anchoredPosition + offset;
            //offset += new Vector2(0, -recordSpace);
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

