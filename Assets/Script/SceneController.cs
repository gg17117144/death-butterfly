using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    
    private float _displayProgress;
    private float _toProgress;
    bool _isLoadingScene = false;

    [SerializeField] public cameraCreate cameraCreate;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (GameManager.instance.SceneIndex >= 2)
        {
            cameraCreate.RefreshReferences();
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        Task.instance.reTaskText();
        // StartCoroutine(LoadSceneTask(sceneNum));
        StartCoroutine(AsynLoadScene(sceneNum));
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

    IEnumerator AsynLoadScene(int sceneNum)
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("開始加載");
        bool isFading = true;
        // yield return new WaitWhile(() => isFading);

        _displayProgress = 0;
        _toProgress = 0;
        _isLoadingScene = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNum);

        operation.allowSceneActivation = false;
        Debug.Log("開始跑條");
        //0~90%讀取條動畫
        while (operation.progress < 0.9f)
        {
            _toProgress = (int)operation.progress;
            _displayProgress = _toProgress;
            Debug.Log("Loading progress: " + (_displayProgress * 100) + "%");
            yield return null;
        }

        //補足Unity 90%~100%時的動畫
        _toProgress = 1;
        while (_displayProgress < _toProgress)
        {
            _displayProgress += 0.01f;
            Debug.Log("Loading progress: " + (_displayProgress * 100) + "%");
            yield return null;
        }
        
        operation.allowSceneActivation = true;
        _isLoadingScene = false;
        UIControl.instance.StopPlayVideo();
    }
}