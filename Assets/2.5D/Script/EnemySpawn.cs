using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject Enemypoint; //敵人重生點
    public GameObject Enemy; //敵人

    void Start()
    {
        Create();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Create();
        }
    }

    void Create()
    {
        Instantiate(Enemy , Enemypoint.transform.position , Quaternion.identity); //生出敵人
    }

}
