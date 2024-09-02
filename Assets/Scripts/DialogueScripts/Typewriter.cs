using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    public bool IsTyping { get; private set; } = false;
    public string TypingMessage { get; private set; } = string.Empty;

    TypewriterConfig config;

    public void SetConfig(TypewriterConfig config)
    { 
        this.config = config;
    }

    public void BeginTypewriter(string message)
    {
        TypingMessage = message;
        StartCoroutine(TypewriteMessage(message));
    }

    IEnumerator TypewriteMessage(string message)
    {
        IsTyping = true;

        char[] characters = message.ToCharArray();
        string typedMessage = string.Empty;

        for (int i = 0; i < characters.Length; i++)
        {
            typedMessage += characters[i];
            config.display.SetText(typedMessage);
            yield return new WaitForSeconds(config.typeTime);
        }

        IsTyping = false;
    }

    public void SkipTypewriter()
    {
        if (IsTyping)
        {
            StopAllCoroutines();
            config.display.SetText(TypingMessage);
            IsTyping = false;
        }
    }
}
