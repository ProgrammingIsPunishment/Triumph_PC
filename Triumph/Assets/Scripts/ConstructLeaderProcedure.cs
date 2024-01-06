using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ConstructLeaderProcedureStep
{
    ShowAvailableSlots,
    Construct
}

public class ConstructLeaderProcedure
{
    public ConstructLeaderProcedureStep ConstructLeaderProcedureStep { get; set; }
    public Holding SelectedHolding { get; private set; }
    public Unit SelectedUnit { get; private set; }
    public ImprovementsView ImprovementsView { get; private set; }
    public int SelectedLot { get; set; }
    //public ResourceItem SelectedResourceItem { get; set; }

    public void AssignFields(Holding holding, Unit unit, ImprovementsView improvementsView)
    {
        this.SelectedHolding = holding;
        this.SelectedUnit = unit;
        this.ImprovementsView = improvementsView;
    }

    public void Reset()
    {
        if (this.SelectedHolding != null)
        {
            this.SelectedHolding = null;
            this.SelectedUnit = null;
            this.SelectedLot = 0;
            this.ImprovementsView = null;
        }
    }

    public void Handle(ConstructLeaderProcedureStep constructLeaderProcedureStep)
    {
        switch (constructLeaderProcedureStep)
        {
            case ConstructLeaderProcedureStep.ShowAvailableSlots:
                this.ShowAvailableSlots(this.ImprovementsView);
                break;
            case ConstructLeaderProcedureStep.Construct:
                //this.SelectedUnit.Gather(this.SelectedResourceItem);

                //NEED TO DO...will need to allow the user to select a building
                Building tempBuilding = Oberkommando.SAVE.AllBuildings.First(b => b.GUID == "hut").CreateInstance(this.SelectedLot);
                this.SelectedHolding.Buildings.Add(tempBuilding);
                this.SelectedUnit.Construct();

                Oberkommando.UI_CONTROLLER.holdingDetailsView.Refresh(this.SelectedHolding, this.SelectedUnit);
                Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails);

                this.Reset();
                break;
        }

        this.ConstructLeaderProcedureStep = constructLeaderProcedureStep;
    }

    private void ShowAvailableSlots(ImprovementsView improvementsView)
    {
        //Oberkommando.UI_CONTROLLER.holdingDetailsView.improvementsTab.
        foreach (BuildingSlotView bsv in improvementsView.BuildingSlotViews)
        {
            if (bsv.building == null)
            {
                //Player can build here
                bsv.Enable();
            }
            else
            {
                bsv.Disable();
            }
        }
    }
}
