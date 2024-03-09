using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipView : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI messageText;

    /// <summary>
    /// Generic Tooltip
    /// </summary>
    /// <param name="title"></param>
    public void Refresh(string title, string message)
    {
        this.titleText.text = title;
        this.messageText.text = message;
    }

    public void Display(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.y += 5;
        temp.x += 5;
        transform.position = temp;
        //Debug.Log(Input.mousePosition + "    " + transform.position);
    }
}
