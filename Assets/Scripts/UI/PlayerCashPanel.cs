using System;
using TMPro;
using UnityEngine;

public class PlayerCashPanel : MonoBehaviour
{
    [SerializeField] TMP_Text cashText;
    [SerializeField] Player player;

    private void OnEnable()
    {
        EventManager.Instance.OnPlayerCashChanged += OnPlayerCashChanged;

        UpdatePanel();
    }

    private void OnPlayerCashChanged()
    {
        UpdatePanel();
    }


    public void UpdatePanel()
    {
        cashText.text = player.currentCash.ToString();
    }
}
