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

    public void Refresh(Dispatch dispatch)
    {
        this.RecipientText.text = dispatch.RecipientDisplayName;
        this.MessageText.text = dispatch.Message;

        this.ReceivedSeal.SetActive(dispatch.IsReceived);
        this.CompletedSeal.SetActive(dispatch.IsCompleted);
    }
}
