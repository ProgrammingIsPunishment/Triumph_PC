using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlotManager : MonoBehaviour
{
    [SerializeField] private GameObject IconImageGameObject;
    [SerializeField] private TextMeshProUGUI AmountText;

    private ResourceItem resourceItem = null;

    public void UpdateDisplay(ResourceItem resourceItem)
    {
        this.resourceItem = resourceItem;
        //Assign resource icon based on guid
        this.AmountText.text = resourceItem.Amount.ToString();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
