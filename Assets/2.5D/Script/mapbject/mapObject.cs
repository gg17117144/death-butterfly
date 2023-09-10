using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class mapObject : MonoBehaviour
{
    public bool isfull = false;

    public Transform Shadow;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0 - (int)gameObject.transform.position.y;
        Shadow = transform.GetChild(0);
        if (Shadow == null)
        {
            Shadow.transform.position = this.transform.position;
        }
    }
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider" && isfull == false)
        {
            if (other.transform.position.y > Shadow.transform.position.y)
            {
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1 - (int)gameObject.transform.position.y;
                this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
                //Debug.Log("����i�J����᭱�o");
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2 - (int)gameObject.transform.position.y;
            this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            //Debug.Log("�������}�����o");
        }
    }

}
