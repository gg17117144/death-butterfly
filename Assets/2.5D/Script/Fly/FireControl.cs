using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    public float lifeTime = 3f;

    public Collider2D hardcollider;

    bool iswee = false;
    bool isbom = false;

    Vector3 moveDirection;
    public float speed = 0.3f;

    private Animator animator;
    
    AudioSource audioSource;
    public AudioClip shou;
    public AudioClip bom;

    void Start()
    {
        moveDirection = new Vector3(speed, 0, 0);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shou;
        audioSource.Play();
    }

    public void wee()
    {
        GetComponent<Animator>().SetTrigger("wee");
        iswee = true;
    }

    public void bom2()
    {
        lifeTime = 0f;
        GetComponent<Animator>().SetTrigger("bom2");
        iswee = true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void startwee()
    {
        lifeTime -= Time.deltaTime;

        if (isbom == false)
        {
            if (lifeTime <= 0)
            {
                animator.SetTrigger("bom");
                moveDirection = new Vector2(0,0); // 停止移動
            }
            else if (lifeTime > 0.1f)
            {
                transform.Translate(moveDirection);
            }
        }
    }

    void FixedUpdate()
    {
        if (iswee == true)
        {
            startwee();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "enemy")
        {
            //Debug.Log("撞到東西囉");
            //audioSource.clip = bom;
            //audioSource.Play();
            hardcollider.enabled = false;
            GetComponent<Animator>().SetTrigger("bom");
            moveDirection = new Vector2(0, 0); // 停止移動
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag == "enemy")
        {
            //Debug.Log("撞到東西囉");
            //audioSource.clip = bom;
            //audioSource.Play();
            other.GetComponent<monsterAI>().isHurt(10);
            
            hardcollider.enabled = false;

            GetComponent<Animator>().SetTrigger("bom");

            moveDirection = new Vector2(0, 0); // 停止移動
        }
    }



}
