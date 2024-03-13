using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopSlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [NonSerialized] public Pop Pop;
    [SerializeField] private Image genderIconImage;

    public void Couple(Pop pop)
    {
        this.Pop = pop;
    }

    public void Refresh()
    {
        if (this.Pop.IsMale) { this.genderIconImage.sprite = Resources.Load<Sprite>($"Sprites/Icons/Male"); }
        else { this.genderIconImage.sprite = Resources.Load<Sprite>($"Sprites/Icons/Female"); }
    }

    public void Display(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string popEffects = "";

        foreach (Effect e in this.Pop.Effects)
        {
            popEffects += $"\n{e.DisplayName}";
        }

        if (this.Pop.Effects.Count == 0)
        {
            popEffects += $"\nNo Effects";
        }

        Oberkommando.UI_CONTROLLER.tooltipView.Refresh("Pop",$"Happiness: {this.Pop.Happiness}/10" +
            $"\nNecessities: {this.Pop.Necessities}/10" +
            $"\nLevel: {this.Pop.Level}/5" +
            $"\n{popEffects}");
        Oberkommando.UI_CONTROLLER.tooltipView.Display(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Oberkommando.UI_CONTROLLER.tooltipView.Display(false);
    }
}
