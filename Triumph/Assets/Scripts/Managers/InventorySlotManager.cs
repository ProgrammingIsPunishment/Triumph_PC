using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlotManager : MonoBehaviour
{
    [SerializeField] private GameObject Icon;
    [SerializeField] private TextMeshProUGUI AmountText;

    public void UpdateDisplay(string guid,int amount)
    {
        //Assign resource icon
        this.AmountText.text = amount.ToString();
    }
}
