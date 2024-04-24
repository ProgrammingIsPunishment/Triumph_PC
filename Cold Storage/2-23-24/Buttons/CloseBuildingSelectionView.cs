using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBuildingSelectionView : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.buildingSelectionView.Display(false);
        Oberkommando.UI_CONTROLLER.holdingView.improvementsTab.ShowImprovableLots(false);
    }
}
