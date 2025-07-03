using TMPro;
using UnityEngine;

public class RoundNrPanel : MonoBehaviour
{
    private int roundNr = 0;
    [SerializeField] TMP_Text roundNrUI;

    public void IncreaseRoundNr()
    {
        roundNr++;
        roundNrUI.text = roundNr.ToString();
    }
}

