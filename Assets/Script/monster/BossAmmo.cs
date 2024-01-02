using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAmmo : MonoBehaviour
{
    [SerializeField] private float ammoSpeed;   
    [SerializeField] private int ammoDamage;
    Vector3 moveDirection;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        ammoSpeed = Random.Range(5, 15);
        moveDirection = new Vector3(ammoSpeed * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        time += Time.deltaTime;
        if (time >= 10)
        {
            Destroy(gameObject);
        }
    }
    void move()
    { 
        transform.Translate(moveDirection);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"打到玩家了{other}");
        if (other.tag == "Player")
        {
            // Debug.Log($"打到玩家了{other}");
            other.GetComponent<PlayerHeart>().damage(ammoDamage);
            Destroy(gameObject);
        }
    }
}