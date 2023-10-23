using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{

    public string Name;    //設定名子
    public int HP;         //設定血量
    public int Damgae;     //設定傷害

    public void SetMonster(string name, int hp, int damgae) //建立怪物方法
    {
        this.Name = name;
        this.HP = hp;
        this.Damgae = damgae;
    }

    public void OnDestroy()
    {
        Destroy(this.gameObject); 
    }
}
