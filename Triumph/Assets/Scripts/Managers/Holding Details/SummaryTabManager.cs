using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryTabManager : MonoBehaviour
{
    [SerializeField] private Image terrainLandscape;

    public void UpdateDisplay(Holding holding)
    {
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

    public void Initialize()
    {
        //Do nothing as of the moment 11/7/2023...Keep going...for Kaitlin...
    }
}
