using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private int Hp;
    
    public GameObject ammoPrefab;
    public int numberOfAttacks = 12; // 技能的數量
    public float radius = 5f; // 圓形範圍的半徑

    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void skill01()
    {
        for (int i = 0; i < numberOfAttacks; i++)
        {
            float angle = i * (360f / numberOfAttacks); // 計算每個技能的角度

            // 計算技能的位置
            float x = transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 attackPosition = new Vector3(x, y, 0f);

            // 計算技能的方向
            Vector3 direction = (attackPosition - transform.position).normalized;

            // 生成技能，並使其指向外部
            GameObject attack = Instantiate(ammoPrefab, attackPosition, Quaternion.identity);
            attack.transform.right = direction; // 將技能的右方向設為計算出的方向
        }
    }
}
