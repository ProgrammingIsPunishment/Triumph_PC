using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatch
{
    public string RecipientDisplayName { get; set; }
    public string Message { get; set; }
    public List<Task> Tasks { get; set; }
    public bool IsReceived { get; set; }
    public bool IsCompleted { get; set; }
    public Civilization Owner { get; set; }

    public Dispatch(string recipientDisplayName, string message, Civilization owner)
    {
        this.RecipientDisplayName = recipientDisplayName;
        this.Message = message;
        this.Tasks = new List<Task>();
        this.IsReceived = false;
        this.IsCompleted = false;
        this.Owner = owner;
    }

    public void Completed()
    {
        this.IsCompleted = true;
    }

    public void Received()
    {
        this.IsReceived = true;
    }
}
