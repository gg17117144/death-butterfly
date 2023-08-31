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

    public Text promptText;

    bool isChangScene;

    private void Start()
    {
        GameManager.isStoping = false;
        // 在目標場景中，隱藏提示文字
        if (promptText != null)
        {
            promptText.enabled = false;
        }
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
        if (collision.tag == "PlayerCollider")
        {
            if (Task.task03 == true)
            {
                Debug.Log("按下f去下一關");
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
            if (collision.tag == "PlayerCollider")
            {
                Debug.Log("按下f去下一關");
                //LoadScene(lever);
                SceneManager.LoadScene(lever);
            }
        }

    }

    public void LoadScene(int sceneNum)
    {
        // 在切換場景前，顯示提示文字
        if (promptText != null)
        {
            promptText.enabled = true;
        }

        if (!isChangScene)
        {
            isChangScene = true;
            StartCoroutine(LoadSceneTask(sceneNum));
        }

    }

    private IEnumerator LoadSceneTask(int sceneNum)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneNum);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        isChangScene = false;
    }

}
