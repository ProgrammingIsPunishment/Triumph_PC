using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HoldingDetailsProcessState
{
    None,
    Show,
    Update,
    NewTab,
    Hide
}

public class HoldingDetailsUIProcess : IUIProcess
{
    private HoldingDetailsManager HoldingDetailsManager;

    public UIState UIState { get; } = UIState.HoldingDetails;
    private HoldingDetailsProcessState holdingDetailsProcessState = HoldingDetailsProcessState.Hide;

    public HoldingDetailsUIProcess(HoldingDetailsManager holdingDetailsManager)
    {
        this.HoldingDetailsManager = holdingDetailsManager;
    }

    //public void Process(UIProcessData uIProcessData)
    //{
    //    this.holdingDetailsProcessState = (HoldingDetailsProcessState)uIProcessData.HoldingDetailsProcessState;

    //    switch (this.holdingDetailsProcessState)
    //    {
    //        case HoldingDetailsProcessState.Show:
    //            if (Oberkommando.UI_CONTROLLER.SelectedHoldings.Count > 0) { 
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings[0].HoldingManager.DisplaySelected(false);
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings.Clear();
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings.Add(uIProcessData.Holding);
    //            }
    //            else {
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings.Add(uIProcessData.Holding);
    //            }

    //            uIProcessData.Holding.HoldingManager.DisplaySelected(true);
    //            this.HoldingDetailsManager.UpdateDisplay(uIProcessData.Holding);
    //            this.HoldingDetailsManager.Show();
    //            break;
    //        case HoldingDetailsProcessState.Update:
    //            if (Oberkommando.UI_CONTROLLER.SelectedHoldings.Count > 0)
    //            {
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings[0].HoldingManager.DisplaySelected(false);
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings.Clear();
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings.Add(uIProcessData.Holding);
    //            }
    //            else
    //            {
    //                Oberkommando.UI_CONTROLLER.SelectedHoldings.Add(uIProcessData.Holding);
    //            }

    //            uIProcessData.Holding.HoldingManager.DisplaySelected(true);
    //            this.HoldingDetailsManager.UpdateDisplay(uIProcessData.Holding);
    //            //this.HoldingDetailsManager.Show();
    //            break;
    //        case HoldingDetailsProcessState.NewTab:
    //            this.HoldingDetailsManager.SwitchTab((HoldingDetailsTabType)uIProcessData.HoldingDetailsTabType);
    //            break;
    //        case HoldingDetailsProcessState.Hide:
    //            if (Oberkommando.UI_CONTROLLER.ActiveUIStates.Contains(UIState.MoveLeader))
    //            {
    //                Oberkommando.UI_CONTROLLER.MoveLeaderUnitUIProcess.ProcessEnd();
    //            }
    //            Oberkommando.UI_CONTROLLER.SelectedHoldings[0].HoldingManager.DisplaySelected(false);
    //            Oberkommando.UI_CONTROLLER.SelectedHoldings.Clear();
    //            this.HoldingDetailsManager.Hide();
    //            break;
    //    }
    //}

    public void ProcessEnd()
    {
        ////Logic for reseting the ui process
        //this.HoldingDetailsManager.Hide();
        //this.holdingDetailsProcessState = HoldingDetailsProcessState.None;
        //Oberkommando.UI_CONTROLLER.ActiveUIStates.Remove(this.UIState);
        //Oberkommando.UI_CONTROLLER.RefocusUIState();
    }
}
