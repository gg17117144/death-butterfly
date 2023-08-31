using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdataOrderInLayer : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y - gameObject.transform.position.y >= -1 )
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
    }
}
