using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject mainGamePanel;
    [SerializeField] private GameObject effectPanel;
    [SerializeField] private GameObject rumorPanel;
    [SerializeField] private GameObject randomEventPanel;
    [SerializeField] private GameObject endPanel;

    private GameObject currentPanel;
    private Dictionary<GameObject, GameObject[]> transitions;

    void Awake()
    {
        transitions = new Dictionary<GameObject, GameObject[]>
       {
           { startPanel,       new GameObject[] { mainGamePanel } },
           { mainGamePanel,    new GameObject[] { effectPanel, endPanel } },
           { effectPanel,      new GameObject[] { rumorPanel } },
           { rumorPanel,       new GameObject[] { randomEventPanel } },
           { randomEventPanel, new GameObject[] { mainGamePanel } }
       };
    }

    void Start()
    {
        startPanel.SetActive(true);
        currentPanel = startPanel;
    }

    public void ShowNextPanel()
    {
        
        if (transitions.ContainsKey(currentPanel))
        {
            currentPanel.SetActive(false);

            if (transitions.TryGetValue(currentPanel, out var targets) && targets.Length > 1)
            {
                GameObject nextScreen = GetNextScreenForBranch();
                nextScreen.SetActive(true);
                currentPanel = nextScreen;
            }
            else
            {
                currentPanel = transitions[currentPanel][0];
                currentPanel.SetActive(true);
            }

            
        }
        else
        {
            Debug.LogWarning("No transition found for the current panel.");
        }
        Debug.Log("Current panel: " + currentPanel.name);
    }

    private GameObject GetNextScreenForBranch()
    {
        //branches
        if (currentPanel == mainGamePanel)
        {
            int idx = (GameManager.Instance.roundNr <= GameManager.Instance.maxRoundNr) ? 0 : 1;
            return transitions[mainGamePanel][idx];
        }

        Debug.LogWarning("Current panel is not the main game panel, cannot determine next screen for branch.");
        return null;
    }
}
