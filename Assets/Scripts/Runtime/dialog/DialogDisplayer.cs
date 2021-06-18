using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDisplayer : MonoBehaviour
{
    public bool playOnAwake = false;
    public TMP_Text text;
    public DialogData[] messages;

    private void Awake()
    {
        if (playOnAwake) StartDialog();
    }

    public void StartDialog()
    {
        StartCoroutine(DialogDisplay());
    }

    private IEnumerator DialogDisplay()
    {
        for (var i = 0; i < messages.Length; i++)
        {
            text.text = messages[i].message;   
            yield return new WaitForSeconds(messages[i].displayFor);
        }

        text.text = "";
    }
}
