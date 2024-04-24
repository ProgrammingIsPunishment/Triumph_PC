using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectView : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;

    public void Refresh(Effect effect)
    {
        this.iconImage.sprite = Resources.Load<Sprite>($"Sprites/Effects/{effect.IconFileName}");

        if (effect.IsPositiveEffect)
        {
            this.backgroundImage.sprite = Resources.Load<Sprite>($"Sprites/Effects/positiveeffectbackground");
        }
        else
        {
            this.backgroundImage.sprite = Resources.Load<Sprite>($"Sprites/Effects/negativeeffectbackground");
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
