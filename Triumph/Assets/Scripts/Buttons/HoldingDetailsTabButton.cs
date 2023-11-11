using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingDetailsTabButton : MonoBehaviour
{
    [SerializeField] private HoldingDetailsTabType holdingDetailsTabType;

    public void ClickEvent()
    {
        Oberkommando.UI_CONTROLLER.holdingDetailsManager.SwitchTab(this.holdingDetailsTabType);
        //Holding tempSelectedHolding = Oberkommando.UI_CONTROLLER.SelectedHoldings[0];
        //Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails, new UIProcessData(tempSelectedHolding, HoldingDetailsProcessState.NewTab, this.HoldingDetailsTabType));
    }

    public void Disable()
    {
        this.GetComponent<Button>().interactable = false;
    }

    public void Enable()
    {
        this.GetComponent<Button>().interactable = true;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
