using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldingDetailsManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField holdingNameInput;
    [SerializeField] private TMP_InputField unitNameInput;
    [SerializeField] private TextMeshProUGUI unitActionPointText;
    [SerializeField] private Image terrainLandscape;
    [SerializeField] private GameObject unitContainer;
    [SerializeField] private GameObject noUnitContainer;

    [SerializeField] private NaturalResourcesTabManager naturalResourcesTabManager;

    //Action Buttons
    [SerializeField] private GameObject moveActionButton;

    public void UpdateDisplay(Holding holding)
    {
        this.holdingNameInput.text = holding.DisplayName;

        if (holding.Unit != null)
        {
            this.unitNameInput.text = holding.Unit.DisplayName;
            this.unitContainer.SetActive(true);
            this.unitActionPointText.text = holding.Unit.ActionPoints.ToString();

            if (holding.Unit.ActionPoints == 0)
            {
                this.moveActionButton.SetActive(false);
            }
            else
            {
                this.moveActionButton.SetActive(true);
            }
        }
        else
        {
            this.unitContainer.SetActive(false);
        }

        List<ResourceItem> workingNaturalResourceItems = holding.ResourceItems.FindAll(ri=>ri.ResourceItemType == ResourceItemType.Foliage || ri.ResourceItemType == ResourceItemType.Fauna);
        if (workingNaturalResourceItems.Count >= 1)
        {
            this.naturalResourcesTabManager.UpdateDisplay(workingNaturalResourceItems);
            this.naturalResourcesTabManager.Show();
        }
        else
        {
            this.naturalResourcesTabManager.Hide();
        }

        this.terrainLandscape.sprite = Resources.Load<Sprite>($"Sprites/Terrain Landscapes/{holding.TerrainType}TerrainLandscape");
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void AssignManagers(NaturalResourcesTabManager naturalResourcesTabManager)
    {
        this.naturalResourcesTabManager = naturalResourcesTabManager;
    }
}
