using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OutgoingDispatchesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipientText;
    [SerializeField] private TextMeshProUGUI MessageText;
    [SerializeField] private GameObject ScrollViewContent;
    [SerializeField] private GameObject DispatchContainer;
    [SerializeField] private DeleteDispatchButton DeleteDispatchButton;

    [NonSerialized] public Dispatch SelectedDispatch = null;

    public void Show(bool isBeingShown)
    {
        this.DispatchContainer.SetActive(false);
        this.DeleteDispatchButton.Show(false);
        this.gameObject.SetActive(isBeingShown);
    }

    public void Refresh(List<Dispatch> dispatches)
    {
        this.SelectedDispatch = null;
        foreach (Transform child in ScrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Dispatch d in dispatches)
        {
            Oberkommando.PREFAB_SERVICE.InstantiateDispatchListItem(d,this.ScrollViewContent);
        }
    }

    public void RefreshDispatch(Dispatch dispatch)
    {
        this.SelectedDispatch = dispatch;
        this.RecipientText.text = dispatch.Recipient;
        this.MessageText.text = dispatch.Message;

        if (Oberkommando.GAME_CONTROLLER.PendingDispatches.Contains(dispatch))
        {
            this.DeleteDispatchButton.Show(true);
        }

        this.DispatchContainer.SetActive(true);
    }

    //public void OnSelect()
    //{
    //    Oberkommando.GAME_CONTROLLER.CanCameraMove = false;
    //    //Debug.Log("Selected");
    //}

    //public void OnDeselect()
    //{
    //    Oberkommando.GAME_CONTROLLER.CanCameraMove = true;
    //    //Debug.Log("Deselected");
    //}

    //public void OnChange()
    //{
    //    //Debug.Log("Changed");
    //}
}
