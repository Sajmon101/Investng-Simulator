using TMPro;
using UnityEngine;

public class RoundNrPanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] TMP_Text roundNrUI;

    public void UpdatePanel()
    {
        roundNrUI.text = GameManager.Instance.roundNr.ToString();
    }
}

