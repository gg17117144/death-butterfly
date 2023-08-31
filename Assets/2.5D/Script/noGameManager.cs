using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class noGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            Destroy(gameManager.gameObject);
        }

    }

    public void gg()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void gg1()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void gg2()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

}
