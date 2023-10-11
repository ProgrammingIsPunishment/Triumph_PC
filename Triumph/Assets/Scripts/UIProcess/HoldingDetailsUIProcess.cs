using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HoldingDetailsProcessState
{
    Show,
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

    public void Process(UIProcessData uIProcessData)
    {
        this.holdingDetailsProcessState = (HoldingDetailsProcessState)uIProcessData.HoldingDetailsProcessState;

        switch (this.holdingDetailsProcessState)
        {
            case HoldingDetailsProcessState.Show:
                if (Oberkommando.UI_CONTROLLER.SelectedHoldings.Count > 0) { 
                    Oberkommando.UI_CONTROLLER.SelectedHoldings[0].HoldingManager.HideSelected();
                    Oberkommando.UI_CONTROLLER.SelectedHoldings.Clear();
                    Oberkommando.UI_CONTROLLER.SelectedHoldings.Add(uIProcessData.Holding);
                }
                else {
                    Oberkommando.UI_CONTROLLER.SelectedHoldings.Add(uIProcessData.Holding);
                }

                uIProcessData.Holding.HoldingManager.ShowSelected();
                this.HoldingDetailsManager.UpdateDisplay(uIProcessData.Holding);
                this.HoldingDetailsManager.Show();
                break;
            case HoldingDetailsProcessState.Hide:
                Oberkommando.UI_CONTROLLER.SelectedHoldings[0].HoldingManager.HideSelected();
                Oberkommando.UI_CONTROLLER.SelectedHoldings.Clear();
                this.HoldingDetailsManager.Hide();
                break;
        }
    }

    public void Reset()
    {
        //Logic for reseting the ui process
        this.HoldingDetailsManager.Hide();
        this.holdingDetailsProcessState = HoldingDetailsProcessState.Hide;
    }
}
