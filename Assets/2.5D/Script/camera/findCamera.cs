using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class findCamera : MonoBehaviour
{
    GameObject targetGroup;

    // Start is called before the first frame update
    void Start()
    {
        targetGroup = GameObject.FindGameObjectWithTag("CM");

        // 获取 Cinemachine TargetGroup 组件

        // 将 CM TargetGroup1 设置为 vcam1 的跟随目标
        GetComponent<CinemachineVirtualCamera>().Follow = targetGroup.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
