using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject ottp;     //另一個傳送們
    public GameObject player;   //玩家

    void Start()
    {
        ottp.GetComponent<CircleCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.transform.position = ottp.transform.position;
            ottp.GetComponent<CircleCollider2D>().enabled = false;
            Invoke("reply", 0.5f);
        }
    }

    void reply()
    {
        ottp.GetComponent<CircleCollider2D>().enabled = true;
    }

}
