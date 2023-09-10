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
        // 监听场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
        cameraCreate.RefreshReferences();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 获取当前场景的ID并更新GameManager中的值
        GameManager.mapLevel = scene.buildIndex;
        Debug.Log(GameManager.mapLevel);
        GameManager.instance.SceneCheck();
        cameraCreate.RefreshReferences();
    }
}
