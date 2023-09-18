using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTP : MonoBehaviour
{
    public int lever;
    public bool rot;
    public bool fCheck;

    public GameObject waitIng;

    bool isChangScene;

    private GameObject player;

    private void Start()
    {
        //GameManager.isStoping = false;
        if (waitIng != null)
        {
            waitIng.SetActive(false);
        }

        player = GameManager.instance.player;
    }
    private void Update()
    {
        if (rot ==true)
        {
            transform.Rotate(Vector3.forward, 50 * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollider"))
        {
            if (Task.task03 == true)
            {
                //Debug.Log("任務完成");
                if (Input.GetKeyUp(KeyCode.F))
                {
                    GameManager.mapLevel++;
                    Task.ResetTask();
                    
                    LoadScene(lever);
                }
            }
        }
        if (!fCheck)
        {
            if (collision.CompareTag("PlayerCollider"))
            {
                Debug.Log("觸發加載過場動畫");
                player.transform.position = new Vector3(0, 0, 0);
                //LoadScene(lever);
                SceneManager.LoadScene(lever);
            }
        }
    }

    public void LoadScene(int sceneNum)
    {
        // 等待畫面
        if (waitIng != null)
        {
            waitIng.SetActive(true);
        }

        if (!isChangScene)
        {
            isChangScene = true;
            StartCoroutine(LoadSceneTask(sceneNum));
        }

    }

    private IEnumerator LoadSceneTask(int sceneNum)
    {
        waitIng.SetActive(false);
        var asyncLoad = SceneManager.LoadSceneAsync(sceneNum);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        isChangScene = false;
    }

}
