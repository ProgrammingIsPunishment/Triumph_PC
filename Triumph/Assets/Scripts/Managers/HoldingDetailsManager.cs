using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldingDetailsManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField holdingNameInput;
    [SerializeField] private TMP_InputField unitNameInput;
    [SerializeField] private Image terrainLandscape;
    [SerializeField] private GameObject unitContainer;
    [SerializeField] private GameObject noUnitContainer;

    public void UpdateDisplay(Holding holding)
    {
        this.holdingNameInput.textComponent.text = holding.DisplayName;

        if (holding.Unit != null)
        {
            this.unitNameInput.textComponent.text = holding.Unit.DisplayName;
            this.unitContainer.SetActive(true);
        }
        else
        {
            this.unitContainer.SetActive(false);
        }

        switch (holding.TerrainType)
        {
            case TerrainType.Grassland:
                this.terrainLandscape.sprite = 
                break;
            case TerrainType.Mountain:
                break;
            case TerrainType.Hills:
                break;
            case TerrainType.Ocean:
                break;
            default:
                break;
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
