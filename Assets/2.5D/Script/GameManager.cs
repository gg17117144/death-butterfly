using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int mapLevel;
    public static GameObject mainCamera;
    static GameManager _intance;

    public GameObject lightSet;
    public GameObject gaming;
    public GameObject StopSetting;
    public GameObject task;
    public GameObject dead;
    public static bool isStoping;
    bool StopOpen;

    GameObject player;

    public GameObject gun;

    int maxMonsterNum;

    public static bool creatMonster;

    public static bool isFight;

    void Awake()
    {

        if (_intance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _intance = this;


        DontDestroyOnLoad(this.gameObject);

        Task.ResetTask();

    }
    // Start is called before the first frame update
    void Start()
    {
        isStoping = false;

        isFight = false;
        task.SetActive(true);
        StopSetting.SetActive(false);

        gun = GameObject.FindGameObjectWithTag("gun");

        player = GameObject.FindGameObjectWithTag("Player");
        //gun = GameObject.FindGameObjectWithTag("gun");

        Task.ResetTask();

    }



    // Update is called once per frame
    void Update()
    {

        if (isStoping == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            TaskButton();
            Time.timeScale = 1;
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopOpen = !StopOpen;
            StopButton();
        }

        if (maxMonsterNum >= 100)
        {
            creatMonster = false;
        }
        else
        {
            creatMonster = true;
        }
        sceneCheck();
    }

    void sceneCheck()
    {
        mapLevel = mainCamera.GetComponent<camera>().scenesNum;

        switch (mapLevel)
        {
            case 0:
                break;
            case 2:
                isFight = false;
                task.SetActive(false);
                gun.SetActive(false);
                gaming.SetActive(false);
                break;
            case 3:
                isFight = true;
                task.SetActive(true);
                gun.SetActive(true);
                gaming.SetActive(true);
                break;
            case 4:
                isFight = true;
                gun.SetActive(true);
                gaming.SetActive(true);
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
    }

    void TaskButton()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            task.GetComponent<Task>().reTaskText();
            task.GetComponent<Task>().moveTask();
        }

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void Continue()
    {
        StopOpen = !StopOpen;

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
    }

    public void volumeSliderSet(float volumeValue)
    {
        AudioListener.volume = volumeValue;
    }

    public void lightSliderSet(float lightValue)
    {
        float mappedValue = (1 - lightValue) * 150f;
        lightSet.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)mappedValue);
    }

    public void BackHome()
    {
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

    public void relife()
    {
        dead.SetActive(false);
        player.GetComponent<PlayerHeart>().relife();
        //SceneManager.LoadScene(mapLevel);
    }
}
