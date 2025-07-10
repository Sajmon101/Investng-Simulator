using UnityEngine;

public class MainGamePanel : MonoBehaviour
{
    [SerializeField] CompanyListUI companyListUI;
    [SerializeField] RoundNrPanel roundNrPanel;
    [SerializeField] PlayerCashPanel playerCashPanel;
    [SerializeField] RumorPanel rumorPanel;
    [SerializeField] RandomEventsPanel randomEventsPanel;

    private void OnEnable()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        rumorPanel.UpdatePanel();
        playerCashPanel.UpdatePanel();
        randomEventsPanel.UpdatePanel();
        roundNrPanel.UpdatePanel();
    }
}
