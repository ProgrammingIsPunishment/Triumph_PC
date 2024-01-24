using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonsView : MonoBehaviour
{
    [SerializeField] private Image seasonIcon;

    public void Refresh(Season season)
    {
        this.seasonIcon.sprite = Resources.Load<Sprite>($"Sprites/UI/{season.IconFileName}");
    }
}
