using TMPro;
using UnityEngine;

public class RoundNrPanel : MonoBehaviour, IUpdatablePanel
{
    private int roundNr = 0;
    [SerializeField] TMP_Text roundNrUI;

    public void IncreaseRoundNr()
    {
        roundNr++;
        roundNrUI.text = roundNr.ToString();
    }

    public void UpdatePanel()
    {
        roundNrUI.text = roundNr.ToString();
    }
}

