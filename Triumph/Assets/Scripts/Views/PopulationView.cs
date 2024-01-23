using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopulationView : MonoBehaviour
{
    [NonSerialized] public List<EffectView> EffectViews = new List<EffectView>();

    [SerializeField] private TextMeshProUGUI populationAmountText;
    [SerializeField] private TextMeshProUGUI happinessAmountText;

    public void Initialize()
    {
        this.EffectViews.AddRange(this.GetComponentsInChildren<EffectView>());
        this.HideAllEffects();
    }

    public void Refresh(Population population)
    {
        this.populationAmountText.text = population.Amount.ToString();
        this.happinessAmountText.text = population.Happiness.ToString();

        this.HideAllEffects();

        int index = 0;
        foreach (Effect e in population.Effects)
        {
            this.EffectViews[index].Refresh(e);
            this.EffectViews[index].Show();
            index++;
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

    private void HideAllEffects()
    {
        foreach (EffectView ev in this.EffectViews)
        {
            ev.Hide();
        }
    }
}
