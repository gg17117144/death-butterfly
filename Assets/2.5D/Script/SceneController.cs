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
        //Debug.Log(GameManager.mapLevel);
        GameManager.instance.SceneCheck();
        GameManager.instance.task.GetComponent<Task>().mapTask();
        if (scene.buildIndex >= 2)
        {
            cameraCreate.RefreshReferences();
        }
    }
    
    
}
