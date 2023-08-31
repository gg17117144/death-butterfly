using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapObject : MonoBehaviour
{
    public bool isfull = false;

    public Transform circle;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0 - (int)gameObject.transform.position.y;
        circle = transform.GetChild(0);
        if (circle == null)
        {
            circle.transform.position = this.transform.position;
        }
    }
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider" && isfull == false)
        {
            if (other.transform.position.y > circle.transform.position.y)
            {
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1 - (int)gameObject.transform.position.y;
                this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
                //Debug.Log("角色進入物件後面囉");
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2 - (int)gameObject.transform.position.y;
            this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            //Debug.Log("角色離開物件囉");
        }
    }

}
