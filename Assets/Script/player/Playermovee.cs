using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Playermovee : MonoBehaviour
{
    //public float moveSpeed; //移動速度
    public float SpeedUP = 1f;
    [SerializeField]
    public float SpeedUPTime = 0f;
    
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
        if (!GameManager.instance.isTalking && !UIControl.instance.isPlayingVideo && !GameManager.instance.isDeading)
        {
            movement();
            PlayerSpeed();
        }
    }

    private void movement()//方法
    {
        //採用直接改變物件座標的方式
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;

        targetPosition = (Vector2)transform.position + inputVector * moveSpeed * Time.fixedDeltaTime * SpeedUP;

        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }

    public void startplayerSpeedUP(float time)
    {
        //UIControl.instance.DebugText("應該要調整動畫速度");
        //UIControl.instance.DebugText("應該要有加速動畫效果");
        if (SpeedUP < 2f)
        {
            SpeedUP += 0.25f;
        }

        //Debug.Log($"以增加{SpeedUPTime}");
        SpeedUPTime = time;
    }

    public void PlayerSpeed()
    {
        if (SpeedUPTime > 0)
        {
            //SpeedUP = 1.5f;
            SpeedUPTime -= Time.deltaTime;
        }
        else
        {
            //SpeedUP = 1f;
            if (SpeedUP > 1f)
            {
                SpeedUP -= 0.1f;
            }
            SpeedUPTime = 0f;
        }
    }

    public void reSetPlayerValue()
    {
        SpeedUPTime = 0f;
        SpeedUP = 1f;
    }
    
}
