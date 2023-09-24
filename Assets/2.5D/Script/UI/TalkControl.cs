using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkControl : MonoBehaviour
{
    private bool hasTriggered = false;
    private TalkToTalk talkToTalk;

    public GameObject DialogBubble;

    public int PlayStoryLevel;
    private void Start()
    {
        talkToTalk = UIControl.instance.GetComponent<TalkToTalk>();
        DialogBubble.SetActive(true);
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hasTriggered && other.tag == "PlayerCollider")
        {
            //UnityEngine.Debug.Log("按下F開始對話");
            if (Input.GetKeyDown(KeyCode.F))
            {
                DialogBubble.SetActive(false);
                talkToTalk.ShowDialogueByStoryLevel(0);
                //UnityEngine.Debug.Log("碰到了");
                hasTriggered = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hasTriggered && other.tag == "PlayerCollider")
        {
            DialogBubble.SetActive(true);
            //UnityEngine.Debug.Log("解除觸發了");
            hasTriggered = false;
        }
    }
    
    
    
}

