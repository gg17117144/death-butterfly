using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class findCamera : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.instance.player;

        GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }


}
