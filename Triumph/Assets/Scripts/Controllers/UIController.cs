using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private SelectionTargetManager SelectionTargetManger;
    [SerializeField] private HoldingDetailsManager HoldingDetailsManager;

    public void SetUIState(UIState uiState)
    {
        Oberkommando.UISTATE = uiState;
    }

    public void UpdateHoldingDetails(Holding holding)
    {
        this.HoldingDetailsManager.UpdateDisplay(holding);
    }

    public void UpdateSelectionTarget(HoldingManager holdingMangager, bool isBeingShown)
    {
        switch (isBeingShown)
        {
            case true:
                this.SelectionTargetManger.ShowHoldingAsTarget(holdingMangager);
                break;
            case false:
                this.SelectionTargetManger.HideSelectionTarget();
                break;
        }
    }

    public void SelectHoldingForDetails(Holding holding)
    {
        if (Oberkommando.SELECTED_HOLDINGS.Count > 0) { Oberkommando.SELECTED_HOLDINGS[0].HoldingManager.HideSelected(); }
        Oberkommando.SELECTED_HOLDINGS.Add(holding);
        holding.HoldingManager.ShowSelected();
    }

    public void SelectHoldingForMovement(Holding holding)
    {
        Oberkommando.SELECTED_HOLDINGS.Add(holding);
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
                //if (this.SelectedHolding != null) { this.SelectedHolding.HoldingManager.HideSelected(); }
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
}
