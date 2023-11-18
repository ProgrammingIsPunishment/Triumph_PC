using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResourcesTabManager : MonoBehaviour
{
    private List<InventorySlotManager> InventorySlotManagers = new List<InventorySlotManager>();

    public void UpdateView(List<ResourceItem> resourceItems)
    {
        foreach (InventorySlotManager ism in this.InventorySlotManagers) { ism.Hide(); }
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
        this.InventorySlotManagers.AddRange(this.GetComponentsInChildren<InventorySlotManager>());
        foreach (InventorySlotManager ism in this.InventorySlotManagers)
        {
            ism.Hide();
        }
    }
}
