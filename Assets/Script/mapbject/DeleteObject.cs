using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{

    public Animator firemm;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "skill" && other.GetComponent<FireControl>() != null)
        {
            if (other.GetComponent<FireControl>().ID == 1 )
            {
                Debug.Log(other);
                firemm.SetTrigger("fire");
            }

        }
    }

    

    private void Start()
    {

    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

}
