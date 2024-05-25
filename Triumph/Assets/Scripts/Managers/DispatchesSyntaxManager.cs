using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DispatchesSyntaxManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField RecipientInput;
    [SerializeField] private TextMeshProUGUI RecipientDisplayText;
    [SerializeField] private TMP_InputField MessageInput;
    [SerializeField] private TextMeshProUGUI MessageDisplayText;

    [SerializeField] private SendDispatchButton SendDispatchesButton;

    private bool isRecipientValid = false;
    private bool isMessageValid = false;

    public void OnRecipientChangeEvent()
    {
        this.isRecipientValid = false;
        this.RecipientDisplayText.text = FormatRecipient(this.RecipientInput.text);
        this.SendDispatchesButton.Enabled(this.IsValid());
    }

    public void OnMessageChangeEvent()
    {
        this.isMessageValid = false;
        this.MessageDisplayText.text = FormatMessage(this.MessageInput.text);
        this.SendDispatchesButton.Enabled(this.IsValid());
    }

    private bool IsValid()
    {
        return this.isRecipientValid && this.isMessageValid;
    }

    private string FormatRecipient(string text)
    {
        string resultFormatted = "";
        List<bool> valid = new List<bool>();

        if (Oberkommando.SYNTAXLIBRARY.IsUnitMatch(text) || Oberkommando.SYNTAXLIBRARY.IsHoldingMatch(text))
        {
            resultFormatted = $"<color=#43b501><u>{text}</u></color>";
            valid.Add(true);
        }
        else
        {
            resultFormatted = $"<color=#d10202>{text}</color>";
            valid.Add(false);
        }

        this.isRecipientValid = !valid.Contains(false);

        return resultFormatted;
    }

    private string FormatMessage(string text)
    {
        string resultFormatted = "";
        List<bool> valid = new List<bool>();

        Tuple<bool, string[]> isMatch = Oberkommando.SYNTAXLIBRARY.IsCommandMatch(text);

        if (isMatch.Item1)
        {
            string[] workingParts = text.Split();

            foreach (string s in workingParts)
            {
                if (resultFormatted != "") { resultFormatted += " "; }

                if (isMatch.Item2.Contains(s.ToUpper()))
                {
                    resultFormatted += $"<color=#43b501>{s}</color>";
                    valid.Add(true);
                }
                else
                {
                    if (Oberkommando.SYNTAXLIBRARY.IsUnitMatch(s) || Oberkommando.SYNTAXLIBRARY.IsHoldingMatch(s))
                    {
                        resultFormatted += $"<color=#43b501><u>{s}</u></color>";
                        valid.Add(true);
                    }
                    else
                    {
                        resultFormatted += $"<color=#d10202>{s}</color>";
                        valid.Add(false);
                    }
                }
            }
        }
        else
        {
            resultFormatted = $"<color=#d10202>{text}</color>";
            valid.Add(false);
        }

        this.isMessageValid = !valid.Contains(false);

        return resultFormatted;
    }
}
