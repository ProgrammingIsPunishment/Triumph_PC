using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIState UIState { get; private set; }
    public List<Holding> SelectedHoldings { get; set; } = new List<Holding>();
    public List<UIState> ActiveUIStates { get; set; } = new List<UIState>();

    [SerializeField] private SelectionTargetManager SelectionTargetManger;

    public HoldingDetailsUIProcess HoldingDetailsUIProcess;
    public MoveLeaderUnitUIProcess MoveLeaderUnitUIProcess;

    public void NewUIState(UIState uiState, UIProcessData uIProcessData)
    {
        if (!this.ActiveUIStates.Contains(uiState)) { this.ActiveUIStates.Add(uiState); }
        else { /*Do nothing...ui state already in list*/ }

        this.UIState = uiState;

        switch (uiState)
        {
            case UIState.HoldingDetails:
                if (uIProcessData != null) { this.HoldingDetailsUIProcess.Process(uIProcessData); }
                break;
            case UIState.MoveLeader:
                this.MoveLeaderUnitUIProcess.Process(uIProcessData);
                break;
            default:
                break;
        }
    }

    public void HideAll()
    {
        this.HoldingDetailsUIProcess.ProcessEnd();
        this.MoveLeaderUnitUIProcess.ProcessEnd();
        this.ActiveUIStates.Clear();
    }

    public void RefocusUIState()
    {
        if (this.ActiveUIStates.Count >= 1)
        {
            this.UIState = this.ActiveUIStates.Last();
        }
        else
        {
            this.UIState = UIState.HoldingDetails;
        }
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
