using UnityEngine;

public class MainGamePanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] CompanyListUI companyListUI;
    [SerializeField] RoundNrPanel roundNrPanel;
    [SerializeField] PlayerCashPanel playerCashPanel;
    [SerializeField] RumorPanel rumorPanel;

    public void UpdatePanel()
    {
        rumorPanel.UpdatePanel();
    }
}
