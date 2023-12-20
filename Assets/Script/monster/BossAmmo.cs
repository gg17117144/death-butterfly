using System;
using UnityEngine;

public class BossAmmo : MonoBehaviour
{
    [SerializeField] private float ammoSpeed;   
    [SerializeField] private int ammoDamage;
    Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3(ammoSpeed * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    { 
        transform.Translate(moveDirection);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"打到玩家了{other}");
        if (other.tag == "Player")
        {
            Debug.Log($"打到玩家了{other}");
            other.transform.parent.gameObject.GetComponent<PlayerHeart>().damage(ammoDamage);
            Destroy(gameObject);
        }
    }
}