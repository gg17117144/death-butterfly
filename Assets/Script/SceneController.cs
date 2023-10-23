using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [SerializeField]
    public cameraCreate cameraCreate;
    
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (GameManager.instance.SceneIndex >= 2)
        {
            cameraCreate.RefreshReferences();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager.mapLevel = scene.buildIndex;
        //Debug.Log($"OnSceneLoaded{GameManager.mapLevel}");
        GameManager.instance.SceneCheck();
        GameManager.instance.task.GetComponent<Task>().mapTask();
        if (scene.buildIndex >= 2)
        {
            cameraCreate.RefreshReferences();
        }
    }
    
    public void LoadScene(int sceneNum)
    {
        Debug.Log("跑這裡應該比較不會卡");
        //SceneManager.LoadScene("Loading");
        UIControl.instance.PlayCutscenesVideo();
        StartCoroutine(LoadSceneTask(sceneNum));
    }

    private IEnumerator LoadSceneTask(int sceneNum)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum);
        // asyncLoad.allowSceneActivation = false;
        
        while (!asyncLoad.isDone)
        {
            // 在加载的过程中可以执行其他操作
            // 可以显示加载进度条等
            // if (Input.GetKeyDown(KeyCode.F))
            // {
            //     asyncLoad.allowSceneActivation = true;
            //     UIControl.instance.StopPlayVideo();
            // }
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            
            yield return null;
        }
        UIControl.instance.StopPlayVideo();
    }
}
