using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResourcesTabManager : MonoBehaviour
{
    private List<InventorySlotDisplayManager> InventorySlotDisplayManagers = new List<InventorySlotDisplayManager>();

    public void UpdateView(List<ResourceItem> resourceItems)
    {
        foreach (InventorySlotDisplayManager ism in this.InventorySlotDisplayManagers) { ism.Hide(); }
        for (int i = 0; i < resourceItems.Count; i++)
        {
            InventorySlotDisplayManagers[i].Couple(resourceItems[i]);
            InventorySlotDisplayManagers[i].UpdateDisplay(resourceItems[i]);
            InventorySlotDisplayManagers[i].Show();
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
        this.InventorySlotDisplayManagers.AddRange(this.GetComponentsInChildren<InventorySlotDisplayManager>());
        foreach (InventorySlotDisplayManager ism in this.InventorySlotDisplayManagers)
        {
            ism.Hide();
        }
    }
}
