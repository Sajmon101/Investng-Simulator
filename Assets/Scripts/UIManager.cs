using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CompanyListUI companyListUI;

    public void InitializeUI(List<Company> companies)
    {
        companyListUI.InitializeListUI(companies);
    }


}
