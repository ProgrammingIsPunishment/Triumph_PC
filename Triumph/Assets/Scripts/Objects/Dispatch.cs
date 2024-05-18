using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatch
{
    public string RecipientName { get; set; }
    public string Message { get; set; }

    public Dispatch(string recipientName, string message)
    {
        this.RecipientName = recipientName;
        this.Message = message;
    }
}
