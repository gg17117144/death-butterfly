using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createItem : MonoBehaviour
{
    public GameObject[] itemPrefab;

    Transform[] createpos;

    public GameObject item_parent;

    Transform player;

    float timer = 60;

    bool need;

    public float waitTimeToCreate;

    // Start is called before the first frame update
    void Start()
    {
        player = player = GameObject.FindGameObjectWithTag("Player").transform;

        item_parent = GameObject.FindGameObjectWithTag("itemSpawn");

        createpos = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerCollider")
        {
            if (timer >= waitTimeToCreate)
            {
                Debug.Log("生成道具");
                createitem();
                timer = 0;
            }
        }
    }

    void createitem()
    {
        var index = Random.Range(0,itemPrefab.Length);

        var num = Random.Range(1, createpos.Length);
        Vector3 pos = createpos[num].position;

        Instantiate(itemPrefab[index] , pos , Quaternion.identity, item_parent.transform);
    }
}
