using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResourcesTabManager : MonoBehaviour
{
    private List<InventorySlotManager> InventorySlotManagers = new List<InventorySlotManager>();

    public void UpdateDisplay(List<ResourceItem> resourceItems)
    {
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
        foreach (InventorySlotManager ism in this.InventorySlotManagers)
        {
            ism.Hide();
        }
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
