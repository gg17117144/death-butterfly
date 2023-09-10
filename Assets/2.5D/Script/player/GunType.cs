using System.Collections.Generic;
using UnityEngine;

public class GunType : MonoBehaviour
{
    public GameObject checkarms; //手臂
    GameObject Canvas;
    bool CanCatchFly = true;     //可不可以抓蝴蝶
    bool check_Type = true;      //槍的模式ture=單發false=雙發
    public int aa = 0;           //蝴蝶第幾格

    public GameObject FlyTankUnder;
    public GameObject[] FlyTankHere;

    public List<GameObject> FlyTank = new List<GameObject>();   //蝴蝶槽[]

    public Animator skillUnderAnime;

    AudioSource audioSource;

    public AudioClip catchfly;

    bool canuse = true;
    GameObject player;

    bool canaddaa = true;

    public float rotatinspeed;

    private Animator animator;

    void Start()
    {
        checkarms.SetActive(false); //開始先關閉

        audioSource = GetComponent<AudioSource>();

        Canvas = GameObject.FindGameObjectWithTag("Canvas");

        player = GameObject.FindGameObjectWithTag("Player");
        
        animator = gameObject.GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        if (GameManager.isStoping == false)
        {
            checkType();
            CheckFlyTank();
        }
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3 - (int)transform.position.y;

        if (FlyTank[aa] != null)
        {
            FlyTankUnder.transform.Rotate(Vector3.forward * rotatinspeed * Time.deltaTime);
        }
        
    }



    private void OnTriggerEnter2D(Collider2D other) //確認是否抓到蝴蝶
    {
        if (other.CompareTag("fly") && CanCatchFly == true)
        {
            //Debug.Log("抓到蝴蝶了");
            audioSource.clip = catchfly;
            audioSource.Play();

            for (int i = 0; i < 3; i++)
            {
                if (FlyTank[i] == null)
                {
                    FlyTank[i] = other.gameObject; //填充蝴蝶槽
                    
                    if (Canvas != null)
                    {
                        Canvas.GetComponent<UIControl>().ReloadGunUI();//重製UI介面
                    }
                    other.gameObject.transform.SetParent(FlyTankHere[i].transform); 
                    other.GetComponent<FlyData>().stop();
                    other.transform.position = FlyTankHere[i].transform.position;
                    break;
                    
                }
                if (i == 2 )
                {
                    CanCatchFly = false;
                }
            }
        }
    }

    void YesCanCatchFly()
    {
        this.GetComponent<EdgeCollider2D>().enabled = true;
    }

    void Canaddaa()
    {
        canaddaa = true;
    }

    public void flyNull()
    {
        FlyTank[aa] = null;
    }

    void CheckFlyTank() //確認第幾格蝴蝶槽
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        int delta = (int)(scrollWheelInput * 10);
        if (scrollWheelInput != 0f && canaddaa == true)
        {
            canaddaa = false;
            Invoke("Canaddaa",0.15f);
            aa = aa + delta;
            if (aa > 2)
            {
                aa = 0;
            }

            if (aa < 0)
            {
                aa = 2;
            }
            
            if (skillUnderAnime != null)
            {
                skillUnderAnime.SetInteger("aa", aa);
            }

        }
        for (int i = 0; i < 2; i++)
        {
            if (FlyTank[i] == null && FlyTank[i + 1] != null)
            {
                FlyTank[i] = FlyTank[i + 1].gameObject;
                FlyTank[i + 1] = null;

                Canvas.GetComponent<UIControl>().ReloadGunUI();//更新UI
            }
        }
    }


    void checkType() //確認模式
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            check_Type = !check_Type;   //切換模式
            CanCatchFly = !CanCatchFly; //是否可以抓蝴蝶
        }

        if (check_Type == true)
        {
            Type01();
        }
        else
        {
            Type02();
        }
    }

    void waituse()
    {
        canuse = true;
    }


    void Type01() //單發
    {
        if (canuse == true)
        {
            if (Input.GetMouseButton(0)) //左鍵
            {
                if (FlyTank[aa] == null)    //如果是空的
                {
                    //Debug.Log("空的");
                }
                else
                {
                    //Debug.Log("??�^??????????");
                    FlyTank[aa].GetComponent<FlyData>().skill();    //重製技能ui
                    Canvas.GetComponent<UIControl>().ReloadGunUI();    //使用重製ui
                    canuse = false;
                    Invoke("waituse", 1);
                }
            }
            else if (Input.GetMouseButton(1)) //右鍵
            {
                //Debug.Log("吸蝴蝶中");
                checkarms.SetActive(true);
                CanCatchFly = true;
            }
            else
            {
                //什麼都不做
                checkarms.SetActive(false);
                CanCatchFly = false;
            }



        }

    }
    


    void Type02() //雙發
    {
        if (Input.GetMouseButtonDown(0))    //左鍵
        {
            if (FlyTank[aa] == null)        //看第幾格
            {
                UnityEngine.Debug.Log("雙發技能");
            }
            else
            {
                UnityEngine.Debug.Log("空的");
            }
        }
        else if (Input.GetMouseButton(1)) //右鍵
        {

            checkarms.SetActive(true);
            CanCatchFly = true;
        }
        else
        {
            //甚麼都不做
            checkarms.SetActive(false);
            CanCatchFly = false;
        }
    }

   
}
