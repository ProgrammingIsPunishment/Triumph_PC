using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    //[HideInInspector] public Transform currentparent;
    [HideInInspector] public Inventory currentInventory;
    [SerializeField] private CanvasGroup canvasGroup;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        //transform.SetParent(Oberkommando.UI_CONTROLLER.holdingView.transform);
        //Oberkommando.UI_CONTROLLER.MoveInventoriesTopLayer(true);
        parentAfterDrag = transform.parent;
        //this.currentparent = this.transform.parent;
        this.currentInventory = this.gameObject.GetComponentInParent<InventoryView>().Inventory;
        //transform.SetParent(transform.root);
        transform.SetParent(Oberkommando.UI_CONTROLLER.holdingView.transform);
        //transform.SetAsLastSibling();
        //image.raycastTarget = false;
        canvasGroup.alpha = .6f;
        this.canvasGroup.blocksRaycasts = false;
        //Debug.Log(this.image.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        //Debug.Log($"{parentAfterDrag.name}");
        transform.SetParent(this.parentAfterDrag);
        Inventory inventory = this.parentAfterDrag.GetComponentInParent<InventoryView>().Inventory;
        ResourceItem resourceItem = this.gameObject.GetComponent<InventorySlotItemView>().GetResourceItem();
        //image.raycastTarget = true;

        if (inventory != this.currentInventory)
        {
            this.currentInventory.ResourceItems.Remove(resourceItem);
            inventory.ResourceItems.Add(resourceItem);
            this.currentInventory = inventory;
        }


        canvasGroup.alpha = 1f;
        this.canvasGroup.blocksRaycasts = true;
        //Oberkommando.UI_CONTROLLER.MoveInventoriesTopLayer(false);
    }
}
