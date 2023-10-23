using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsType : MonoBehaviour
{
    public GameObject arms01;
    public GameObject arms02;

    void Start()
    {
        arms01.SetActive(false);
        arms02.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Checktype();
    }

    void Checktype()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arms01.SetActive(false);
            arms02.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arms01.SetActive(true);
            arms02.SetActive(false);
        }
    }
}
