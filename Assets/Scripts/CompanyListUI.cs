using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompanyListUI : MonoBehaviour
{
    [SerializeField] private CompanyRecord recordPrefab;
    [SerializeField] private RectTransform firstRecordPoint;
    Vector2 offset = new Vector2(0, 0);
    int recordSpace = 90;
    private List<CompanyRecord> records = new List<CompanyRecord>();

    public void InitializeListUI(List<Company> companies)
    {
        foreach (Company company in companies)
        {
            CompanyRecord record = Instantiate(recordPrefab, transform);
            records.Add(record);
            record.GetComponent<RectTransform>().anchoredPosition = firstRecordPoint.GetComponent<RectTransform>().anchoredPosition + offset;
            offset += new Vector2(0, -recordSpace);
            record.company = company;
            record.SetCompanyData(company.companyName, company.stockPrize, 0);
        }
    }
}

