using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTabManager : MonoBehaviour
{
    private List<InventorySlotDisplayManager> InventorySlotManagers = new List<InventorySlotDisplayManager>();

    public void UpdateDisplay(List<ResourceItem> resourceItems)
    {
        foreach (InventorySlotDisplayManager ism in this.InventorySlotManagers) { ism.Hide(); }
        for (int i = 0; i < resourceItems.Count; i++)
        {
            InventorySlotManagers[i].UpdateDisplay(resourceItems[i]);
            InventorySlotManagers[i].Show();
        }
    }

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
        this.InventorySlotManagers.AddRange(this.GetComponentsInChildren<InventorySlotDisplayManager>());
        foreach (InventorySlotDisplayManager ism in this.InventorySlotManagers)
        {
            ism.Hide();
        }
    }
}
