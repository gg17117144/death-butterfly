using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    GameObject player;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // 計算距離
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // 計算掉落物要移動的方向
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;

            // 移動掉落物
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)moveDirection, speed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            Task.DropEmeny += 1;
            Destroy(this.gameObject);
            Debug.Log("掉落物加1");
        }
    }
}
