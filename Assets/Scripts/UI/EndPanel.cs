using UnityEngine;

public class EndPanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] EventLogPanel eventLogPanel;
    [SerializeField] StatsPanel statsPanel;

    public void UpdatePanel()
    {
        statsPanel.UpdatePanel();
        eventLogPanel.ShowAllLogs();
    }
}
