using TMPro;
using UnityEngine;

public class PlayerCashPanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] TMP_Text cashText;
    [SerializeField] Player player;

    public void UpdatePanel()
    {
        cashText.text = player.currentCash.ToString();
    }
}
