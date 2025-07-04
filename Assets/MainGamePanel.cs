using UnityEngine;

public class MainGamePanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] CompanyListUI companyListUI;
    [SerializeField] RoundNrPanel roundNrPanel;
    [SerializeField] PlayerCashPanel playerCashPanel;

    public void UpdatePanel()
    {
        
    }
}
