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

    public bool creatMonster;
    public bool creatFly;

    public bool isDeading;
    public SceneController sceneController;
    public int SceneIndex;
    private TalkToTalk talkToTalk;
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
        talkToTalk = UIControl.instance.GetComponent<TalkToTalk>();
        sceneController = GetComponent<SceneController>();
        SceneCheck();
        Task.ResetTask();
        InvokeRepeating("checkNum", 2f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SceneIndex);

        if (Input.GetKeyDown(KeyCode.Escape) && !isTalking && !UIControl.instance.isPlayingVideo)
        {
            //Debug.Log("叫出暫停鍵了");
            StopOpen = !StopOpen;
            StopButton();
        }

        //Debug.Log(creatMonster);
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
            case 0: //開始畫面
                task.SetActive(false);
                gun.SetActive(false);
                gaming.SetActive(false);
                StopSetting.SetActive(false);
                player.SetActive(false);
                dead.SetActive(false);
                break;
            case 1: //研究室
                task.SetActive(false);
                gun.SetActive(false);
                gaming.SetActive(false);
                player.SetActive(true);
                dead.SetActive(false);
                UIControl.instance.PlayStartVideo();//播放開始影片
                break;
            case 2: //第一區前
                //Debug.Log(mapLevel);
                creatMonster = false;
                task.SetActive(true);
                gun.SetActive(true);
                gaming.SetActive(true);
                player.SetActive(true);
                dead.SetActive(false);
                talkToTalk.ShowDialogueByStoryLevel(1);
                break;
            case 3: //第一區後
                creatMonster = true;
                task.SetActive(true);
                gun.SetActive(true);
                gaming.SetActive(true);
                player.SetActive(true);
                dead.SetActive(false);
                break;
            case 4: //第二區前
                creatMonster = true;
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
        if (!ReferenceEquals(StopSetting , null))
        {
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
        }
        else
        {
            Debug.LogError("沒有StopSetting");
        }


        checkIsStoping();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Continue()
    {
        Debug.Log("使用Continue");

        instance.StopOpen = false;
        instance.isStoping = false;

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
            //creatMonster = true;
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
    
    public void Dead()
    {
        Debug.Log("重生");
        dead.SetActive(true);
        isStoping = true;
        //player.GetComponent<PlayerHeart>().relife();
        //SceneManager.LoadScene(mapLevel);
    }

    public void relife()
    {
        player.GetComponent<PlayerHeart>().relife();
        dead.SetActive(false);
    }
}
