using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    GameObject player;
    private GameObject task;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        task = GameManager.instance.task;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // �p��Z��
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // �p�ⱼ�����n���ʪ���V
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;

            // ���ʱ�����
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)moveDirection, speed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            Task.DropEmeny += 1;
            task.GetComponent<Task>().mapTask();
            Destroy(this.gameObject);
            Debug.Log("吃到了掉落物");
        }
    }
}
