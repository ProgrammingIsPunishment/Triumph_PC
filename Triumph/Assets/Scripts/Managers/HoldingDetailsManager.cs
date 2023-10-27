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
}
