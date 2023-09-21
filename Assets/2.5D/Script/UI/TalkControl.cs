using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkControl : MonoBehaviour
{
    private bool hasTriggered = false;
    private TalkToTalk talkToTalk;
    private void Start()
    {
        talkToTalk = UIControl.instance.GetComponent<TalkToTalk>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.tag == "PlayerCollider")
        {
            talkToTalk.ShowDialogueByStoryLevel(0);
            UnityEngine.Debug.Log("碰到了");
            hasTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hasTriggered && other.tag == "PlayerCollider")
        {
            UnityEngine.Debug.Log("解除觸發了");
            hasTriggered = false;
        }
    }
    
    
    
}

