using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceRequirementView : MonoBehaviour
{
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI amountText;

    public void Refresh(ResourceItem resourceItem, int amount)
    {
        this.amountText.text = amount.ToString();
        this.resourceIcon.sprite = Resources.Load<Sprite>($"Sprites/Icons/{resourceItem.IconFileName}");
    }

    public void Display(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }
}
