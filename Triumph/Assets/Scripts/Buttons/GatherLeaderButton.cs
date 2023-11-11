using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GatherType
{
    Wood
}

public class GatherLeaderButton : MonoBehaviour
{
    [SerializeField] private GatherType gatherType;

    public void OnClickEvent()
    {
        Holding selectedHolding = Oberkommando.UI_CONTROLLER.SelectedHoldings[0];
        selectedHolding.Unit.Gather(selectedHolding.NaturalResourcesInventory, gatherType);
    }
}
