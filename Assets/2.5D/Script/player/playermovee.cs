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

    //Rigidbody2D rB;
    // Start is called before the first frame update
    void Start()
    {
        //rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.isTalking)
        {
            movement();
        }
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
    
}
