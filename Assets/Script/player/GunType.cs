using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunType : MonoBehaviour
{
    public GameObject checkarms; //手臂
    private GameObject Canvas;
    private GameObject task;
    bool FlyTankisFull = true;     //蝴蝶槽是不是空的
    private bool canCatchFly;       //是否可以抓蝴蝶
    bool check_Type = true;      //槍的模式ture=單發false=雙發
    [FormerlySerializedAs("aa")] 
    public int num = 0;           //蝴蝶第幾格

    public GameObject FlyTankUnder;
    public GameObject[] FlyTankHere;

    public List<GameObject> FlyTank = new List<GameObject>();   //蝴蝶槽[]
    private Dictionary<int, bool> capturedButterflies = new Dictionary<int, bool>();    //偵測抓過的蝴蝶

    private AudioSource audioSource;

    public AudioClip catchfly;

    private bool canuse = true;
    private GameObject player;

    private bool isScrolling = true;

    public float rotatinspeed;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        checkarms.SetActive(false); //開始先關閉

        audioSource = GetComponent<AudioSource>();

        Canvas = GameObject.FindGameObjectWithTag("Canvas");

        player = GameObject.FindGameObjectWithTag("Player");

        task = GameManager.instance.task;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void FixedUpdate()
    {
        if (!GameManager.instance.isStoping && !GameManager.instance.isTalking)
        {
            checkType();
            CheckFlyTank();
        }
        spriteRenderer.sortingOrder = 3 - (int)transform.position.y;

        if (!ReferenceEquals(FlyTank[num] , null))
        {
            FlyTankUnder.transform.Rotate(Vector3.forward * rotatinspeed * Time.deltaTime);
        }
        UIControl.instance.ReloadGunUI();
    }



    private void OnTriggerEnter2D(Collider2D other) //確認是否抓到蝴蝶
    {
        if (other.CompareTag("fly"))
        {
            if (canCatchFly)
            {
                //Debug.Log("抓到蝴蝶了");
                audioSource.clip = catchfly;
                audioSource.Play();
                other.GetComponent<FlyData>().isCatch = true;
                CaptureButterfly(other.GetComponent<FlyData>().ButterFlyID);
                for (int i = 0; i < 3; i++)
                {
                    if (FlyTank[i] == null)
                    {
                        FlyTankisFull = false;
                        if (!FlyTankisFull)
                        {
                            FlyTank[i] = other.gameObject; //填充蝴蝶槽

                            if (Canvas != null)
                            {
                                Canvas.GetComponent<UIControl>().ReloadGunUI(); //重製UI介面
                            }

                            other.gameObject.transform.SetParent(FlyTankHere[i].transform);
                            other.GetComponent<FlyData>().stop();
                            other.transform.position = FlyTankHere[i].transform.position;
                            task.GetComponent<Task>().mapTask();
                            break;
                        }
                    }
                    else if (i == 2)
                    {
                        FlyTankisFull = true;
                    }
                }
                if (FlyTankisFull)
                {
                    FlyTank[num] = other.gameObject; //填充蝴蝶槽
                    
                    if (Canvas != null)
                    {
                        Canvas.GetComponent<UIControl>().ReloadGunUI();//重製UI介面
                    }
                    other.gameObject.transform.SetParent(FlyTankHere[num].transform); 
                    other.GetComponent<FlyData>().stop();
                    other.transform.position = FlyTankHere[num].transform.position;
                    task.GetComponent<Task>().mapTask();
                }
            }
        }
    }

    void YesCanCatchFly()
    {
        this.GetComponent<EdgeCollider2D>().enabled = true;
    }

    public void CaptureButterfly(int flyID)
    {
        if (!capturedButterflies.ContainsKey(flyID) || !capturedButterflies[flyID])
        {
            // 這是第一次抓到的蝴蝶，可以執行特定操作
            Debug.Log("抓到ID是 " + flyID + " 的蝴蝶！");
            switch (flyID)
            {
                case 1:
                    GameManager.instance.talkToTalk.ShowDialogueByStoryLevel(5);
                    break;
                case 2:
                    GameManager.instance.talkToTalk.ShowDialogueByStoryLevel(6);
                    break;
                case 3:
                    GameManager.instance.talkToTalk.ShowDialogueByStoryLevel(7);
                    break;
            }
            //GameManager.instance.talkToTalk.ShowDialogueByStoryLevel();
            capturedButterflies[flyID] = true;
        }
        else
        {
            // 不是第一次抓到该蝴蝶的ID，不执行操作
            Debug.Log("ID是 " + flyID + " 的蝴蝶已經被抓過了！");
        }
    }

    public void reSetFlyCapture()
    {
        capturedButterflies = new Dictionary<int, bool>();
    }

    public void flyNull()
    {
        FlyTank[num] = null;
    }

    void CheckFlyTank() //確認第幾格蝴蝶槽
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput > 0)
        {
            scrollWheelInput = 1;
        }
        else if (scrollWheelInput < 0)
        {
            scrollWheelInput = -1;
        }
        int delta = (int)scrollWheelInput;

        //Debug.Log($"delt:{delta} scroll:{scrollWheelInput}");
        if (scrollWheelInput != 0f && isScrolling)
        {
            isScrolling = false;
            //Debug.Log(isScrolling);
            StartCoroutine(ProcessScrollInput(delta));
        }
        /*
        for (int i = 0; i < 2; i++)
        {
            if (ReferenceEquals(FlyTank[i],null) && !ReferenceEquals(FlyTank[i+1],null))
            {
                FlyTank[i] = FlyTank[i + 1].gameObject;
                FlyTank[i + 1] = null;

                Canvas.GetComponent<UIControl>().ReloadGunUI();//更新UI
            }
        }
        */
    }
    
    IEnumerator ProcessScrollInput(int input)
    {
        num += input;
        if (num > 2)
        {
            num = 0;
        }

        if (num < 0)
        {
            num = 2;
        }
        UIControl.instance.ChangeGunTank(input);
        //Debug.Log(canuse);
        yield return new WaitForSeconds(0.5f);

        isScrolling = true; // 重置滚动标志
    }

    void checkType() //確認模式
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            check_Type = !check_Type;   //切換模式
            //CanCatchFly = !CanCatchFly; //是否可以抓蝴蝶
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

    public void flyNoEnergy()
    {
        FlyTank[num] = null;
        UIControl.instance.ReloadGunUI();
    }

    public void resetFlyTank()
    {
        for (int i = 0; i < 3; i++)
        {
            FlyTank[i] = null;
        }
        UIControl.instance.ReloadGunUI();
    }


    void Type01() //單發
    {
        if (canuse == true)
        {
            if (Input.GetMouseButton(0) && !canCatchFly) //左鍵
            {
                if (FlyTank[num] == null)    //如果是空的
                {
                    //Debug.Log("空的");
                    UIControl.instance.ReloadGunUI();
                }
                else
                {
                    //Debug.Log("??�^??????????");
                    FlyTank[num].GetComponent<FlyData>().skill();    //使用技能
                    canuse = false;
                    UIControl.instance.ReloadGunUI();
                    Invoke("waituse", 1);
                }
            }
            else if (Input.GetMouseButton(1)) //右鍵
            {
                //Debug.Log("吸蝴蝶中");
                checkarms.SetActive(true);
                canCatchFly = true;
            }
            else
            {
                //什麼都不做
                checkarms.SetActive(false);
                canCatchFly = false;
            }
        }
    }
    


    void Type02() //雙發
    {
        if (Input.GetMouseButtonDown(0))    //左鍵
        {
            if (FlyTank[num] == null)        //看第幾格
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
            canCatchFly = true;
        }
        else
        {
            //甚麼都不做
            checkarms.SetActive(false);
            canCatchFly = false;
        }
    }

    public void reSetGunValue()
    {
        checkarms.SetActive(false); //開始先關閉
        resetFlyTank();
        for (int i = 0; i < 3; i++)
        {
            if (player && FlyTankHere[i].transform.childCount > 0)
            {
                for (int j = 0; j < FlyTankHere[i].transform.childCount; j++)
                {
                    Destroy(FlyTankHere[i].transform.GetChild(j).gameObject);
                    Debug.Log($"已清空FlyTankHere{i}");
                }
            }
            else
            {
                Debug.Log("沒有找到東西");
            }
        }
    }
    

   
}
