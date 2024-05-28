using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DispatchListItemManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipientText;
    [SerializeField] private TextMeshProUGUI MessageText;
    [SerializeField] private GameObject ReceivedSeal;
    [SerializeField] private GameObject CompletedSeal;
    [SerializeField] private Dispatch CoupledDispatch;

    public void Refresh(Dispatch dispatch)
    {
        this.RecipientText.text = dispatch.Recipient;
        this.MessageText.text = dispatch.Message;

        this.ReceivedSeal.SetActive(dispatch.IsReceived);
        this.CompletedSeal.SetActive(dispatch.IsCompleted);

        this.CoupledDispatch = dispatch;
    }

    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.OutgoingDispatchesManager.RefreshDispatch(this.CoupledDispatch);
    }
}
