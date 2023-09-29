using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTargetManager : MonoBehaviour
{
    public void ShowHoldingAsTarget(HoldingManager holdingManager)
    {
        float newXPosition = holdingManager.gameObject.transform.position.x;
        float newZPositon = holdingManager.gameObject.transform.position.z;
        Vector3 newPosition = new Vector3(newXPosition, 5f, newZPositon);
        this.gameObject.transform.localPosition = newPosition;
    }

    public void HideSelectionTarget()
    {
        Vector3 newPosition = new Vector3(this.gameObject.transform.position.x, 100f, this.gameObject.transform.position.z);
        this.gameObject.transform.localPosition = newPosition;
    }
}
