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

    private bool isDraggable;

    public void SetDraggable(bool isdraggable)
    {
        this.isDraggable = isdraggable;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.isDraggable)
        {
            Debug.Log("Begin drag");
            //transform.SetParent(Oberkommando.UI_CONTROLLER.holdingView.transform);
            //Oberkommando.UI_CONTROLLER.MoveInventoriesTopLayer(true);
            this.parentAfterDrag = this.transform.parent;
            //this.currentparent = this.transform.parent;
            this.currentInventory = this.gameObject.GetComponentInParent<InventoryView>().Inventory;
            //transform.SetParent(transform.root);
            this.transform.SetParent(Oberkommando.UI_CONTROLLER.holdingView.transform);
            //transform.SetAsLastSibling();
            //image.raycastTarget = false;
            this.canvasGroup.alpha = .6f;
            this.canvasGroup.blocksRaycasts = false;
            //Debug.Log(this.image.name);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this.isDraggable)
        {
            this.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.isDraggable)
        {
            Debug.Log("End drag");
            //Debug.Log($"{parentAfterDrag.name}");
            this.transform.SetParent(this.parentAfterDrag);
            Inventory inventory = this.parentAfterDrag.GetComponentInParent<InventoryView>().Inventory;
            ResourceItem resourceItem = this.gameObject.GetComponent<InventorySlotItemView>().GetResourceItem();
            //image.raycastTarget = true;

            if (inventory != this.currentInventory)
            {
                this.currentInventory.ResourceItems.Remove(resourceItem);
                inventory.ResourceItems.Add(resourceItem);
                this.currentInventory = inventory;
            }


            this.canvasGroup.alpha = 1f;
            this.canvasGroup.blocksRaycasts = true;
            //Oberkommando.UI_CONTROLLER.MoveInventoriesTopLayer(false);
        }
    }
}
