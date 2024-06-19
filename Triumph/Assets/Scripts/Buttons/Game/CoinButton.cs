using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CoinCountText;

    public void OnClickEvent()
    {

    }

    public void Refresh(int coinCount)
    {
        this.CoinCountText.text = coinCount.ToString();
    }
}
