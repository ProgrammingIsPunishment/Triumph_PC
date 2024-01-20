using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TMP_InputField unitNameInput;
    [SerializeField] private TextMeshProUGUI unitActionPointText;
    [SerializeField] private GameObject unitContainer;
    [SerializeField] private GameObject noUnitContainer;

    //Action Buttons
    [SerializeField] private GameObject moveActionButton;
    [SerializeField] private GameObject gatherActionButton;
    [SerializeField] private GameObject buildActionButton;

    public void Refresh(Holding holding, Unit unit)
    {
        if (unit != null)
        {
            this.unitNameInput.text = unit.DisplayName;
            this.unitContainer.SetActive(true);
            this.unitActionPointText.text = unit.ActionPoints.ToString();

            //Update Action Buttons
            this.moveActionButton.SetActive(false);
            this.gatherActionButton.SetActive(false);
            this.buildActionButton.SetActive(false);

            if (unit.ActionPoints >= 1)
            {
                if (unit != null)
                {
                    this.moveActionButton.SetActive(true);
                }

                if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
                {
                    this.gatherActionButton.SetActive(true);
                }

                if (holding.HasUndevelopedLots())
                {
                    this.buildActionButton.SetActive(true);
                }

                //holding.HasUnconstructedBuildings()...allow leader to apply labor 
            }
        }
        else
        {
            this.unitContainer.SetActive(false);
        }
    }
}
