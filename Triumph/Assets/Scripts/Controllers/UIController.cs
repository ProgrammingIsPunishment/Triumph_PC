using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public List<UIState> UIStateStack { get; set; } = new List<UIState>();

    //UI Data
    public Holding SelectedHolding { get; set; } = null;
    public Unit SelectedUnit { get; set; } = null;
    public Holding SelectedDestinationHolding { get; set; } = null;
    public ResourceItem SelectedResourceItemForGather { get; set; } = null;
    public int SelectedLotForBuild { get; set; } = 0;
    public Building SelectedBuildingForBuild { get; set; } = null;
    public Building SelectedBuildingForLabor { get; set; } = null;

    //Master Views
    [SerializeField] public HoldingView holdingView;
    [SerializeField] public SeasonsView seasonsView;
    [SerializeField] public PoliticalPowerView politicalPowerView;
    [SerializeField] public BuildingSelectionView buildingSelectionView;

    public Holding selectedHolding = null;
    public Unit selectedUnit = null;

    public event EventHandler OnNewHoldingClickForSelection;
    public event EventHandler OnHoldingClickForMovement;
    public event EventHandler OnBuildingSlotClickForBuild;

    public void Initialize()
    {
        foreach (Holding h in Oberkommando.SAVE.AllHoldings)
        {
            h.HoldingDisplayManager.OnHoldingClickForSelection += EventHandler_HoldingSelected;
            h.HoldingDisplayManager.OnHoldingClickForMovement += EventHandler_HoldingClickedForMovement;
        }

        foreach (BuildingSlotView bsv in this.holdingView.improvementsTab.BuildingSlotViews)
        {
            bsv.OnBuildingSlotButtonClick += EventHandler_BuildingSlotClickedForBuild;
        }

        this.holdingView.closeHoldingViewButton.OnHoldingViewCloseEvent += EventHandler_HoldingViewCloseClicked;

        this.holdingView.unitView.moveLeaderButton.OnMoveLeaderButtonClick += EventHandler_MoveLeaderButtonClicked;
        this.holdingView.unitView.claimLeaderButton.OnClaimLeaderButtonClick += EventHandler_ClaimLeaderButtonClicked;
        this.holdingView.unitView.buildLeaderButton.OnBuildLeaderButtonClick += EventHandler_BuildLeaderButtonClicked;
    }

    private void EventHandler_HoldingSelected(object sender, EventArgs eventArgs)
    {
        OnNewHoldingClickForSelection?.Invoke(this, EventArgs.Empty);

        HoldingDisplayManager holdingDisplayManager = (HoldingDisplayManager)sender;

        this.selectedHolding = holdingDisplayManager.holding;
        this.selectedUnit = holdingDisplayManager.GetUnitAtThisLocation();

        this.ReturnAllToColdStorage();
        this.holdingView.Refresh(holdingDisplayManager.holding, this.selectedUnit);
        this.selectedHolding.HoldingDisplayManager.ShowSelected(true);

        this.holdingView.ShowDefaultTab();
        this.holdingView.unitView.ShowDefaultTab();
        this.holdingView.Show();
    }

    private void EventHandler_HoldingClickedForMovement(object sender, EventArgs eventArgs)
    {
        HoldingDisplayManager holdingDisplayManager = (HoldingDisplayManager)sender;

        holdingDisplayManager.holding.UpdateVisibility(Oberkommando.SAVE.AllCivilizations[0]);

        bool tempUnitMoved = this.selectedUnit.Move(holdingDisplayManager.holding, holdingDisplayManager.holding.XPosition, holdingDisplayManager.holding.ZPosition);

        this.ShowHoldingsWithinRange(false, this.selectedHolding);

        if (tempUnitMoved)
        {
            this.selectedHolding.HoldingDisplayManager.ShowSelected(false);
        }
        else
        {
            this.selectedHolding.HoldingDisplayManager.ShowSelected(false);
            this.selectedUnit = null;
        }

        this.selectedHolding = holdingDisplayManager.holding;
        this.selectedHolding.HoldingDisplayManager.ShowSelected(true);
        this.holdingView.Refresh(holdingDisplayManager.holding, this.selectedUnit);
    }

    private void EventHandler_MoveLeaderButtonClicked(object sender, EventArgs eventArgs)
    {
        this.ShowHoldingsWithinRange(true, this.selectedHolding);
    }

    private void EventHandler_ClaimLeaderButtonClicked(object sender, EventArgs eventArgs)
    {
        this.selectedHolding.ClaimTerritory(Oberkommando.SAVE.AllCivilizations[0]);
        Oberkommando.SAVE.AllCivilizations[0].UsePoliticalPower(1);

        this.politicalPowerView.Refresh(Oberkommando.SAVE.AllCivilizations[0].PoliticalPower);
        this.holdingView.Refresh(this.selectedHolding, this.selectedUnit);

        this.selectedHolding.HoldingDisplayManager.ShowSelected(true);
        this.selectedHolding.HoldingDisplayManager.ShowBorder(true);
    }

    private void EventHandler_HoldingViewCloseClicked(object sender, EventArgs eventArgs)
    {
        this.holdingView.Hide();
        this.buildingSelectionView.Display(false);
        this.ShowHoldingsWithinRange(false,this.selectedHolding);
        this.selectedHolding.HoldingDisplayManager.ShowSelected(false);

        this.selectedHolding = null;
        this.selectedUnit = null;
    }

    private void EventHandler_BuildLeaderButtonClicked(object sender, EventArgs eventArgs)
    {
        this.holdingView.SwitchTab(HoldingDetailsTabType.Improvements);
        this.holdingView.improvementsTab.ShowImprovableLots(true);

        //this.buildingSelectionView.Display(true);
        //this.buildingSelectionView.Refresh(Oberkommando.SAVE.AllBuildings);
    }

    private void EventHandler_BuildingSlotClickedForBuild(object sender, EventArgs eventArgs)
    {
        this.buildingSelectionView.Display(true);
        this.buildingSelectionView.Refresh(Oberkommando.SAVE.AllBuildings);
    }

    public void UpdateUIState(UIState newUIState)
    {
        Holding tempDestinationHolding = this.SelectedDestinationHolding;
        Holding tempHolding = this.SelectedHolding;
        Unit tempUnit = this.SelectedUnit;

        //Process the new UI state
        switch (newUIState)
        {
            case UIState.Initialize:
                this.ReturnAllToColdStorage();
                this.HideAll();
                this.ClearStateAndData();
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.EndTurn:
                this.ReturnAllToColdStorage();
                this.HideAll();
                this.ResetViews();
                this.UncoupleViews();
                this.ClearStateAndData();
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.HoldingDetails_SelectHolding:
                this.ReturnAllToColdStorage();
                this.holdingView.Refresh(this.SelectedHolding, this.SelectedUnit);
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                this.holdingView.ShowDefaultTab();
                this.holdingView.unitView.ShowDefaultTab();
                this.holdingView.Show();
                break;
            case UIState.HoldingDetails_End:
                this.ReturnAllToColdStorage();
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(false);
                this.holdingView.Hide();
                this.ResetViews();
                this.ClearStateAndData();
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.LeaderMove_SelectHolding:
                this.ShowHoldingsWithinRange(true, this.SelectedHolding);
                break;
            case UIState.LeaderMove_End:
                this.ReturnAllToColdStorage();
                this.SelectedDestinationHolding.UpdateVisibility(Oberkommando.SAVE.AllCivilizations[0]);
                Unit unitToPass = this.SelectedUnit;
                //if (this.SelectedDestinationHolding.HasPassableTerrain())
                //{
                //    this.SelectedUnit.Move(this.SelectedDestinationHolding.XPosition, this.SelectedDestinationHolding.ZPosition);
                //}
                //else
                //{
                //    unitToPass = null;
                //}
                //this.SelectedUnit.Move(this.SelectedDestinationHolding.XPosition,this.SelectedDestinationHolding.ZPosition);
                this.holdingView.Refresh(this.SelectedDestinationHolding, unitToPass);
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(false);
                this.ResetViews();
                this.ClearStateAndData();
                this.HoldingDetailsData(tempDestinationHolding, tempUnit);
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.LeaderGather_SelectResourceItem:
                this.holdingView.naturalResourcesTab.ShowGatherableResources(true);
                this.holdingView.SwitchTab(HoldingDetailsTabType.NaturalResources);
                break;
            case UIState.LeaderGather_End:
                this.ReturnAllToColdStorage();
                this.SelectedUnit.Gather(this.SelectedResourceItemForGather);
                this.ResetViews();
                this.ClearStateAndData();
                this.HoldingDetailsData(tempHolding, tempUnit);
                this.holdingView.Refresh(this.SelectedHolding, this.SelectedUnit);
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.LeaderBuild_SelectLot:
                this.holdingView.SwitchTab(HoldingDetailsTabType.Improvements);
                this.holdingView.improvementsTab.ShowImprovableLots(true);
                break;
            case UIState.LeaderBuild_End:
                this.ReturnAllToColdStorage();
                if (this.SelectedUnit.Inventory.HasResourcesForConstruction(this.SelectedBuildingForBuild.Construction.RequiredComponents))
                {
                    this.SelectedHolding.BuildBuilding(this.SelectedBuildingForBuild);
                    Oberkommando.PREFAB_CONTROLLER.InstantiateBuildingModel(this.SelectedHolding, this.SelectedBuildingForBuild);
                    this.SelectedUnit.Build(this.SelectedBuildingForBuild.Construction.RequiredComponents);
                }
                else
                {
                    //NEED TO DO...do something else
                    Debug.Log("Not enough resources");
                }
                this.ResetViews();
                this.ClearStateAndData();
                this.HoldingDetailsData(tempHolding, tempUnit);
                this.holdingView.Refresh(this.SelectedHolding, this.SelectedUnit);
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.LeaderLabor_SelectImprovement:
                this.holdingView.SwitchTab(HoldingDetailsTabType.Improvements);
                this.holdingView.improvementsTab.ShowNeededLabor(true);
                break;
            case UIState.LeaderLabor_End:
                this.ReturnAllToColdStorage();
                this.SelectedBuildingForLabor.Construction.Labor();
                if (this.SelectedBuildingForLabor.Construction.IsCompleted)
                {
                    Oberkommando.PREFAB_CONTROLLER.InstantiateBuildingModel(this.SelectedHolding, this.SelectedBuildingForLabor);
                }
                this.SelectedUnit.Labor();
                this.ResetViews();
                this.ClearStateAndData();
                this.HoldingDetailsData(tempHolding, tempUnit);
                this.holdingView.Refresh(this.SelectedHolding, this.SelectedUnit);
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.LeaderSettle_End:
                //this.ReturnAllToColdStorage();
                //int tempPeopleToSettle = this.SelectedUnit.Population;
                //this.SelectedUnit.Settle();
                //this.SelectedHolding.Population.Settle(tempPeopleToSettle);
                //this.SelectedHolding.PassEffectFromHolding();
                //this.SelectedHolding.Population.DetermineTurnEffects();
                //this.ResetViews();
                //this.ClearStateAndData();
                //this.HoldingDetailsData(tempHolding, tempUnit);
                //this.holdingView.Refresh(this.SelectedHolding, this.SelectedUnit);
                //this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                //newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            case UIState.LeaderClaim_End:
                this.SelectedHolding.ClaimTerritory(Oberkommando.SAVE.AllCivilizations[0]);
                Oberkommando.SAVE.AllCivilizations[0].UsePoliticalPower(1);
                this.politicalPowerView.Refresh(Oberkommando.SAVE.AllCivilizations[0].PoliticalPower);
                this.ResetViews();
                this.ClearStateAndData();
                this.HoldingDetailsData(tempHolding, tempUnit);
                this.holdingView.Refresh(this.SelectedHolding, this.SelectedUnit);
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                this.SelectedHolding.HoldingDisplayManager.ShowBorder(true);
                newUIState = UIState.HoldingDetails_SelectHolding;
                break;
            default:
                break;
        }

        this.UIStateStack.Add(newUIState);
    }

    public UIState CurrentUIState()
    {
        return this.UIStateStack.Last();
    }

    private void ClearStateAndData()
    {
        this.UIStateStack.Clear();
        this.UIStateStack.Add(UIState.HoldingDetails_SelectHolding);

        this.SelectedHolding = null;
        this.SelectedUnit = null;
        this.SelectedDestinationHolding = null;
        this.SelectedResourceItemForGather = null;
        this.SelectedLotForBuild = 0;
        this.SelectedBuildingForBuild = null;
        this.SelectedBuildingForLabor = null;
    }

    public void ResetViews()
    {
        if (Oberkommando.SELECTED_HOLDING != null)
        {
            //Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.ShowSelected(false);
            Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.DisplayHoldingsWithinRange(false);

            //this.holdingView.naturalResourcesTab.ShowGatherableResources(false);
            //this.holdingView.improvementsTab.ShowImprovableLots(false);
            //this.holdingView.improvementsTab.ShowNeededLabor(false);
            //this.buildingSelectionView.Display(false);
        }

        this.holdingView.naturalResourcesTab.ShowGatherableResources(false);
        this.holdingView.improvementsTab.ShowImprovableLots(false);
        this.holdingView.improvementsTab.ShowNeededLabor(false);
        this.buildingSelectionView.Display(false);
    }

    private void ReturnAllToColdStorage()
    {
        Oberkommando.COLDSTORAGE_CONTROLLER.ReturnAllInventoryItemViews();
    }

    private void UncoupleViews()
    {
        this.holdingView.naturalResourcesTab.UncoupleView();
        this.holdingView.improvementsTab.UncoupleView();

        this.holdingView.unitView.unitInventoryView.UncoupleView();
    }

    private void HideAll()
    {
        this.holdingView.Hide();
    }

    public void HoldingDetailsData(Holding holding, Unit unit)
    {
        if (this.SelectedHolding != null)
        {
            this.SelectedHolding.HoldingDisplayManager.ShowSelected(false);
        }

        this.SelectedHolding = holding;
        this.SelectedUnit = unit;
    }

    public void LeaderMoveData(Holding holding)
    {
        this.SelectedDestinationHolding = holding;
    }

    public void LeaderGatherData(ResourceItem resourceItem)
    {
        this.SelectedResourceItemForGather = resourceItem;
    }

    public void LeaderBuildData(int lot, Building building)
    {
        this.SelectedLotForBuild = lot;
        this.SelectedBuildingForBuild = building;
    }

    public void LeaderLaborData(Building building)
    {
        this.SelectedBuildingForLabor = building;
    }

    public void MapRefresh(Civilization civilization)
    {
        //Set visibility level to hidden for all holdings to start with
        foreach (Holding h in Oberkommando.SAVE.AllHoldings)
        {
            h.VisibilityLevel = VisibilityLevel.Hidden;
        }

        //Loop through and determine explored holdings
        foreach (Holding h in civilization.ExploredHoldings)
        {
            h.VisibilityLevel = VisibilityLevel.Explored;
            h.HoldingDisplayManager.ShowExplored(true);
            h.HoldingDisplayManager.Show(true);

            foreach (Holding hh in h.AdjacentHoldings)
            {
                if (!civilization.ExploredHoldings.Contains(hh))
                {
                    hh.VisibilityLevel = VisibilityLevel.Unexplored;
                    hh.HoldingDisplayManager.ShowExplored(false);
                    hh.HoldingDisplayManager.Show(true);
                }
            }
        }
    }

    //public void MoveInventoriesTopLayer(bool moveTopLayer)
    //{
    //    if (moveTopLayer)
    //    {
    //        this.holdingView.unitView.unitInventoryView.transform.SetParent(this.holdingView.transform);
    //        this.holdingView.storageTab.transform.SetParent(this.holdingView.transform);
    //    }
    //    else
    //    {
    //        this.holdingView.unitView.unitInventoryView.transform.SetParent(this.holdingView.unitView.transform);
    //        this.holdingView.storageTab.transform.SetParent(this.holdingView.transform);
    //    }
    //}

    //public void ShowDiscoveredHoldings(Civilization civilization)
    //{
    //    foreach (Holding h in Oberkommando.SAVE.AllHoldings)
    //    {
    //        //if (h.DiscoveredCivilizationGUIDs.Contains(civilization.GUID))
    //        //{
    //        //    //h.HoldingManager.ShowDiscovered();
    //        //}
    //    }
    //}

    public void ShowExploredHoldings(Civilization civilization)
    {
        foreach (Holding h in civilization.ExploredHoldings)
        {
            h.HoldingDisplayManager.ShowExplored(true);
            h.HoldingDisplayManager.Show(true);
        }
    }

    public void HideAllSelections()
    {
        if (Oberkommando.SELECTED_HOLDING != null)
        {
            Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.ShowSelected(false);
            Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.DisplayHoldingsWithinRange(false);
        }
    }

    private void ShowHoldingsWithinRange(bool isBeingShown, Holding holding)
    {
        if (isBeingShown)
        {
            foreach (Holding h in holding.AdjacentHoldings)
            {
                h.HoldingDisplayManager.ShowSelectable(true);
            }
        }
        else
        {
            foreach (Holding h in holding.AdjacentHoldings)
            {
                h.HoldingDisplayManager.ShowSelectable(false);
            }
        }
    }
}
