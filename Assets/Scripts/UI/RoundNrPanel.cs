using TMPro;
using UnityEngine;

public class RoundNrPanel : MonoBehaviour
{
    [SerializeField] TMP_Text roundNrUI;

    private void OnEnable()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        roundNrUI.text = GameManager.Instance.roundNr.ToString();
    }
}

