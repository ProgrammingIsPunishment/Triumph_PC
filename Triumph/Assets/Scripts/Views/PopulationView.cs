using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI populationAmountText;

    public void Refresh(Population population)
    {
        this.populationAmountText.text = population.Amount.ToString();
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
