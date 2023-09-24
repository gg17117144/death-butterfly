using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int mapLevel;
    public static GameObject mainCamera;
    [SerializeField]
    public GameObject gaming;
    [SerializeField]
    public GameObject StopSetting;
    [SerializeField]
    public GameObject task;
    [SerializeField]
    public GameObject dead;
    public bool isStoping;
    public bool isTalking;
    
    bool StopOpen;

    public GameObject player;
    [SerializeField]
    public GameObject gun;

    public static bool creatMonster;
    public static bool creatFly;
    
    public int SceneIndex;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        gun = GameObject.FindGameObjectWithTag("gun");
        player = GameObject.FindGameObjectWithTag("Player");
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(SceneIndex);
        mapLevel = SceneIndex;
        //SceneCheck();
        Task.ResetTask();
    }
    // Start is called before the first frame update
    void Start()
    {
        isStoping = false;
        checkIsStoping();
        StopSetting.SetActive(false);
        SceneCheck();
        Task.ResetTask();
        
        InvokeRepeating("checkNum", 2f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SceneIndex);

        if (Input.GetKeyDown(KeyCode.Escape) && !isTalking)
        {
            //Debug.Log("叫出暫停鍵了");
            StopOpen = !StopOpen;
            StopButton();
        }

    }

    public void checkIsStoping()
    {
        if (isStoping == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    
    public void SceneCheck()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        Debug.Log("現在場景編號：" + SceneIndex);
        switch (SceneIndex)
        {
            case 0:
                task.SetActive(false);
                gun.SetActive(false);
                gaming.SetActive(false);
                StopSetting.SetActive(false);
                player.SetActive(false);
                dead.SetActive(false);
                break;
            case 1:
                task.SetActive(false);
                gun.SetActive(false);
                gaming.SetActive(false);
                player.SetActive(true);
                dead.SetActive(false);
                break;
            case 2:
                //Debug.Log(mapLevel);
                task.SetActive(true);
                gun.SetActive(true);
                gaming.SetActive(true);
                player.SetActive(true);
                dead.SetActive(false);
                break;
            case 3:
                task.SetActive(true);
                gun.SetActive(true);
                gaming.SetActive(true);
                player.SetActive(true);
                dead.SetActive(false);
                break;
            case 4:
                task.SetActive(true);
                gun.SetActive(true);
                gaming.SetActive(true);
                player.SetActive(true);
                dead.SetActive(false);
                break;

        }

    }

    void StopButton()
    {
        if (StopOpen == true)
        {
            StopSetting.SetActive(true);
            isStoping = true;
        }
        else
        {
            StopSetting.SetActive(false);
            isStoping = false;
        }

        checkIsStoping();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Continue()
    {
        StopOpen = !StopOpen;

        if (StopOpen)
        {
            StopSetting.SetActive(true);
            isStoping = true;
        }
        else
        {
            StopSetting.SetActive(false);
            isStoping = false;
        }

        checkIsStoping();
    }



    public void BackHome()
    {
        Debug.Log("回主選單");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void checkNum()
    {
        var maxMonsterNum = GameObject.FindGameObjectsWithTag("enemy");
        if (maxMonsterNum.Length >= 50)
        {
            creatMonster = false;
        }
        else
        {
            creatMonster = true;
        }
        var maxFlyNum = GameObject.FindGameObjectsWithTag("fly");
        if (maxFlyNum.Length >= 40)
        {
            creatFly = false;
        }
        else
        {
            creatFly = true;
        }

        //Debug.Log($"maxMonsterNum:{maxMonsterNum.Length}  maxFlyNum:{maxFlyNum.Length}");
    }
    
    public void relife()
    {
        Debug.Log("重生");
        dead.SetActive(false);
        player.GetComponent<PlayerHeart>().relife();
        SceneManager.LoadScene(mapLevel);
    }
}
