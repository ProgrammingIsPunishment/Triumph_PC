using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoldingDetailsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HoldingName;
    [SerializeField] private TextMeshProUGUI UnitName;

    public void Refresh(Holding holding, Unit unit)
    {
        this.HoldingName.text = holding.Name;

        if (unit != null)
        {
            this.UnitName.text = unit.Name;
        }
        else
        {
            this.UnitName.text = "";
        }
    }

    public void Show(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }
}
