using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeasonsView : MonoBehaviour
{
    [SerializeField] private Image seasonIcon;
    [SerializeField] private TextMeshProUGUI daysLeftText;

    public void Refresh(Season season)
    {
        this.daysLeftText.text = season.DaysLeft.ToString();
        this.seasonIcon.sprite = Resources.Load<Sprite>($"Sprites/UI/{season.IconFileName}");
    }
}
