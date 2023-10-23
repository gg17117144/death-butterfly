using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTP : MonoBehaviour
{
    public Sprite insideLight;
    public int lever;
    public bool rot;
    public bool fCheck;
    public GameObject insideObGameObject;

    public GameObject waitIng;

    bool isChangScene;

    private GameObject player;
    private Animator insideAnimator;

    public bool isOver = true;

    private void Start()
    {
        //GameManager.isStoping = false;
        if (waitIng != null)
        {
            waitIng.SetActive(false);
        }
        
        player = GameManager.instance.player;
        insideAnimator = insideObGameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (rot ==true)
        {
            transform.Rotate(Vector3.forward, 50 * Time.deltaTime);
        }

        switch (GameManager.instance.SceneIndex)
        {
            case 2: //第一區前
                break;
            case 3: //第一區後
                break;
            case 4: //第二區前
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other is Collider2D)
        {
            if (other.CompareTag("PlayerCollider"))
            {
                insideAnimator.SetBool("isLight" , true);
                //Debug.Log("任務完成");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("我按下了");
                    if (isOver) //有完成
                    {
                        GameManager.mapLevel++;
                        Task.ResetTask();
                    
                        LoadScene(lever);
                    }
                    else
                    {
                        //UIControl.instance.DebugText("還沒做完拉");
                        UIControl.instance.GetComponent<TalkToTalk>().ShowDialogueByStoryLevel(4);
                    }
                }
            }
            if (!fCheck)
            {
                if (other.CompareTag("PlayerCollider"))
                {
                    //Debug.Log("觸發加載過場動畫");
                    player.transform.position = new Vector3(0, 0, 0);
                    LoadScene(lever);
                    //SceneManager.LoadScene(lever);
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            insideAnimator.SetBool("isLight" , false);
        }
    }

    void LoadScene(int sceneNum)
    {
        GameManager.instance.sceneController.LoadScene(sceneNum);
    }

    // public void LoadScene(int sceneNum)
    // {
    //     Debug.Log("跑這裡應該比較不會卡");
    //
    //     if (!isChangScene)
    //     {
    //         isChangScene = true;
    //         UIControl.instance.PlayCutscenesVideo();
    //         StartCoroutine(LoadSceneTask(sceneNum));
    //     }
    //
    // }
    //
    // private IEnumerator LoadSceneTask(int sceneNum)
    // {
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum);
    //     asyncLoad.allowSceneActivation = false;
    //     
    //     while (!asyncLoad.isDone)
    //     {
    //         // 在加载的过程中可以执行其他操作
    //         // 可以显示加载进度条等
    //         if (Input.GetKeyDown(KeyCode.F))
    //         {
    //             asyncLoad.allowSceneActivation = true;
    //             UIControl.instance.StopPlayVideo();
    //         }
    //         float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
    //         Debug.Log("Loading progress: " + (progress * 100) + "%");
    //
    //         yield return null;
    //     }
    // }
}
