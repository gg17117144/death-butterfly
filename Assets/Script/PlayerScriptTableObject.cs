using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float max_hp;              //最大血量值
    public float oldhp;                  //血量值
    public float newhp;

    public float max_O2;                    //最大氧氣值
    public float O2;        
    
    public List<GameObject> FlyTank = new List<GameObject>(3);   //蝴蝶槽[]
}

[CreateAssetMenu(fileName = "playerData",menuName = "playerData")]
public class Data : ScriptableObject
{
    public PlayerData m_playerData;
}


