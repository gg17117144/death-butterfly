using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovee : MonoBehaviour
{
    //public float moveSpeed; //移動速度

    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;

    private Vector2 targetPosition;
    private Vector2 velocity;

    public GameObject talk;

    //Rigidbody2D rB;
    // Start is called before the first frame update
    void Start()
    {
        //rB = GetComponent<Rigidbody2D>();
        if (talk != null)
        {
            talk.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();

    }

    private void movement()//方法
    {
        //採用直接改變物件座標的方式
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;

        targetPosition = (Vector2)transform.position + inputVector * moveSpeed * Time.fixedDeltaTime;

        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (talk != null)
        {
            if (collision.tag == "uncle")
            {
                talk.SetActive(true);
                talk.GetComponent<TalkToTalk>().startTalk();
            }
        }
        else
        {
            //Debug.Log("Talk object is null");
        }
    }
}
