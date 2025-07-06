using UnityEngine;

public class MainGamePanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] CompanyListUI companyListUI;
    [SerializeField] RoundNrPanel roundNrPanel;
    [SerializeField] PlayerCashPanel playerCashPanel;
    [SerializeField] RumorPanel rumorPanel;
    [SerializeField] RandomEventsPanel randomEventsPanel;

    public void UpdatePanel()
    {
        rumorPanel.UpdatePanel();
        playerCashPanel.UpdatePanel();
        randomEventsPanel.UpdatePanel();
        roundNrPanel.UpdatePanel();
    }
}
