using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoliticalPowerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI politicalPowerText;

    public void Refresh(int newValue)
    {
        this.politicalPowerText.text = newValue.ToString();

        int tempPerTurnGain = Oberkommando.SAVE.AllCivilizations[0].PerTurnPoliticalPowerGain();
        int tempPerTurnLoss = Oberkommando.SAVE.AllCivilizations[0].PerTurnPolticalPowerLoss();
        int difference = tempPerTurnGain - tempPerTurnLoss;

        this.politicalPowerText.text += $" ({difference})";
    }
}
