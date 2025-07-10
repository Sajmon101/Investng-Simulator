using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] EventLogPanel eventLogPanel;
    [SerializeField] StatsPanel statsPanel;

    private void OnEnable()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        statsPanel.UpdatePanel();
        eventLogPanel.ShowAllLogs();
    }
}
