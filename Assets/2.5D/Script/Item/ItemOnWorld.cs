using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public int itemID;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            //AddNewItem();
            skill();
            Destroy(gameObject);
        }
    }


    void skill()
    {
        switch (itemID)
        {
            case 0:
                //Debug.Log(itemID);
                player.GetComponent<PlayerHeart>().healHp(50);
                break;
            case 1:
                player.GetComponent<PlayerHeart>().healO2(10);
                break;
        }

    }
}