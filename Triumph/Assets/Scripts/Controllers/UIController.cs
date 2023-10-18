using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIState UIState { get; private set; }
    public List<Holding> SelectedHoldings { get; set; } = new List<Holding>();

    [SerializeField] private SelectionTargetManager SelectionTargetManger;

    public HoldingDetailsUIProcess HoldingDetailsUIProcess;
    public MoveLeaderUnitUIProcess MoveLeaderUnitUIProcess;

    public void NewUIState(UIState uiState, UIProcessData uIProcessData)
    {
        switch (uiState)
        {
            case UIState.Default:
                this.HideAll();
                break;
            case UIState.HoldingDetails:
                this.HoldingDetailsUIProcess.Process(uIProcessData);
                break;
            case UIState.MoveLeader:
                this.MoveLeaderUnitUIProcess.Process(uIProcessData);
                break;
            default:
                break;
        }

        this.UIState = uiState;
    }

    public void HideAll()
    {
        this.HoldingDetailsUIProcess.Reset();
        this.MoveLeaderUnitUIProcess.Reset();
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
