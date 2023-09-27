using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private HoldingDetailsManager HoldingDetailsManager;
    [SerializeField] private GameObject SelectionTarget;
    private Holding SelectedHolding;

    public void SetUIState(UIState uiState)
    {
        //Make changes based on the current uiState
        switch (Oberkommando.UISTATE)
        {
            case UIState.Default:
                //Dont have to do anything?...
                break;
            case UIState.HoldingDetails:
                break;
            case UIState.MoveLeader:
                this.ShowSelectableHoldings();
                break;
            default:
                break;
        }
        Oberkommando.UISTATE = uiState;
    }

    public void UpdateDisplay(UIType uiType, Holding holding)
    {
        switch (uiType)
        {
            case UIType.HoldingDetails:
                if (this.SelectedHolding != null) { this.SelectedHolding.HoldingManager.HideSelected(); }
                this.SelectedHolding = holding;
                this.SelectedHolding.HoldingManager.ShowSelected();
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
                if (this.SelectedHolding != null) { this.SelectedHolding.HoldingManager.HideSelected(); }
                this.HoldingDetailsManager.Hide();
                break;
            default: break;
        }
    }

    public void HideAll()
    {
        this.HoldingDetailsManager.Hide();
    }

    public void ShowDiscoveredHoldings(Civilization civilization)
    {
        foreach (Holding h in Oberkommando.SAVE.AllHoldings)
        {
            if (h.DiscoveredCivilizationGUIDs.Contains(civilization.GUID))
            {
                h.HoldingManager.ShowDiscovered();
            }
        }
    }

    public void ShowExplorableHoldings(Civilization civilization)
    {
        foreach (Holding h in Oberkommando.SAVE.AllHoldings)
        {
            if (h.DiscoveredCivilizationGUIDs.Contains(civilization.GUID))
            {
                List<Holding> adjacentHoldings = Oberkommando.SAVE.AllHoldings.Where(ah => h.AdjacentHoldingGUIDs.Contains(ah.GUID)).ToList();
                foreach (Holding ah in adjacentHoldings)
                {
                    ah.HoldingManager.ShowExplorable();
                }
            }
        }
    }

    public void ShowSelectableHoldings()
    {
        List<Holding> selectableHoldings = Oberkommando.SAVE.AllHoldings.Where(ah => this.SelectedHolding.AdjacentHoldingGUIDs.Contains(ah.GUID)).ToList();
        foreach (Holding h in selectableHoldings)
        {
            h.HoldingManager.ShowSelectable();
        }
    }

    public void ShowSelectionTarget(HoldingManager holdingManager)
    {
        float newXPosition = holdingManager.gameObject.transform.position.x;
        float newZPositon = holdingManager.gameObject.transform.position.z;
        Vector3 newPosition = new Vector3(newXPosition, 5f, newZPositon);
        this.SelectionTarget.transform.localPosition = newPosition;
    }
}
