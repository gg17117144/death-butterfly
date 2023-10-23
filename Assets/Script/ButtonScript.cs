using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject setting;

    private void Start()
    {
        setting.SetActive(false);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        GameManager.instance.player.GetComponent<PlayerHeart>().ResetPlayerValue();
        GameManager.instance.gun.GetComponent<GunType>().resetFlyTank();
    }

    public void settingon()
    {
        setting.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void Continue()
    {
        setting.SetActive(false);
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadGame()
    {

    }

}
