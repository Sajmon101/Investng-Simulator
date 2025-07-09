using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject nextRoundPanel;
    [SerializeField] Player player;
    [SerializeField] RoundNrPanel roundNrPanel;
    [SerializeField] EventLogPanel eventLogPanel;
    [SerializeField] RandomEventsPanel randomEventPanel;
    [SerializeField] EndPanel endPanel;
    [SerializeField] MainGamePanel mainGamePanel;
    [SerializeField] EfectPanel efectPanel;
    public RumorPanel rumorPanel;

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);

        var initializable = panel.GetComponent<IUpdatablePanel>();
        if (initializable != null)
            initializable.UpdatePanel();
    }

    public void ShowEffectOrEndPanel()
    {
        if (GameManager.Instance.roundNr <= GameManager.Instance.maxRoundNr)
        {
            HidePanel(mainGamePanel.gameObject);
            ShowPanel(efectPanel.gameObject);
        }
        else
        {
            HidePanel(mainGamePanel.gameObject);
            ShowPanel(endPanel.gameObject);
        }
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
