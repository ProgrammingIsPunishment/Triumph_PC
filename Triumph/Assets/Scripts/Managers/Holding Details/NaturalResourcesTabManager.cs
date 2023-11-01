using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResourcesTabManager : MonoBehaviour
{
    private List<InventorySlotManager> InventorySlotManagers;

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        this.InventorySlotManagers.AddRange(this.GetComponentsInChildren<InventorySlotManager>());
    }
}
