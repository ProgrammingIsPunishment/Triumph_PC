using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DispatchesManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField RecipientInput;
    [SerializeField] private TMP_InputField MessageInput;
    [SerializeField] private SendDispatchButton SendDispatchButton;
    [SerializeField] private DispatchesSyntaxManager DispatchesSyntaxManager;

    public Dispatch GetDispatch()
    {
        RecipientType recipientType = RecipientType.Unit;

        if (Oberkommando.SYNTAXLIBRARY.IsUnitMatch(this.RecipientInput.text)) { recipientType = RecipientType.Unit; }
        else if (Oberkommando.SYNTAXLIBRARY.IsHoldingMatch(this.RecipientInput.text)) { recipientType = RecipientType.Holding; }

        return new Dispatch(this.RecipientInput.text, this.MessageInput.text, Oberkommando.PLAYER, recipientType);
    }

    public void Default()
    {
        this.RecipientInput.text = "";
        this.MessageInput.text = "";
        this.SendDispatchButton.Enabled(false);
    }

    public void Show(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void OnSelect()
    {
        Oberkommando.GAME_CONTROLLER.CanCameraMove = false;
        //Debug.Log("Selected");
    }

    public void OnDeselect()
    {
        Oberkommando.GAME_CONTROLLER.CanCameraMove = true;
        //Debug.Log("Deselected");
    }

    public void OnChange()
    {
        //Debug.Log("Changed");
    }
}
