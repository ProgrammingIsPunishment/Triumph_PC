using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private HoldingDetailsManager HoldingDetailsManager;

    public void UpdateDisplay(UIType uiType, Holding holding)
    {
        switch (uiType)
        {
            case UIType.HoldingDetails:
                this.HoldingDetailsManager.UpdateDisplay(holding);
                break;
            default: break;
        }
    }

    public void Show(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.HoldingDetails:
                this.HoldingDetailsManager.Show();
                break;
            default: break;
        }
    }

    public void Hide(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.HoldingDetails:
                this.HoldingDetailsManager.Hide();
                break;
            default: break;
        }
    }

    public void HideAll()
    {
        this.HoldingDetailsManager.Hide();
    }
}
