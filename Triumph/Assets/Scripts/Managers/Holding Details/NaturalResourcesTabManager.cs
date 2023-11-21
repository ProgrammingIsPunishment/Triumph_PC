using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResourcesTabManager : MonoBehaviour
{
    [SerializeField] public InventoryView inventoryView;

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
