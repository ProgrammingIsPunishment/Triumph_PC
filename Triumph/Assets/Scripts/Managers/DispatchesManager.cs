using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DispatchesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Recipient;
    [SerializeField] private TextMeshProUGUI Message;

    public void Show(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void OnSelect()
    {
        Oberkommando.GAME_CONTROLLER.CanCameraMove = false;
        //Debug.Log("Selected");
    }

    public void OnDeselect()
    {
        Oberkommando.GAME_CONTROLLER.CanCameraMove = true;
        //Debug.Log("Deselected");
    }

    public void OnChange()
    {
        //Debug.Log("Changed");
    }
}
