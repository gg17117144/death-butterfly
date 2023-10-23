using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task : MonoBehaviour
{
    public Text[] TaskText;

    private int monsterNum;
    private int TaskNum;
    private float timeLeft = 180f;
    [SerializeField]
    public static bool task01,task02,task03;


    public static int killEmeny = 0;
    public static int DropEmeny = 0;
    private bool isMovingRight = true; // 初始狀態向右移動
    private bool isTaskMoving = false; // 任務是否正在移動
    
    private RectTransform rectTransform;
    
    // Start is called before the first frame update
    private void Start()
    {
        ResetTask();
        rectTransform = GetComponent<RectTransform>();
        killEmeny = 0;
        reTaskText();
    }
    
    // Update is called once per frame
    void Update()
    {
        //reTaskText();
        //mapTask();
        switch (GameManager.mapLevel)
        {
            case 3:
                timeLeft -= Time.deltaTime;
                reTaskText();
                break;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && gameObject.activeSelf)
        {
            moveTask();
            reTaskText();
        }
    }
    public static void ResetTask()
    {
        task01 = false;
        task02 = false;
        task03 = false;
        killEmeny = 0;
        DropEmeny = 0;
    }

    public void mapTask()
    {
        switch (GameManager.mapLevel)
        {
            case 2: //第一區前scenes01
                if (killEmeny >= 1)
                {
                    task01 = true;
                    GameManager.instance.creatMonster = true;
                }

                if (killEmeny >= monsterNum)
                {
                    task02 = true;
                }
                else
                {
                    task02 = false;
                }

                if (task01 == true || task02 == true)
                {
                    task03 = true;
                }
                break;
            case 3://第一區後scenes02
                if (DropEmeny >= 10)
                {
                    task01 = true;
                    //Debug.Log("任務1完成");
                }
                if (killEmeny >= 10)
                {
                    task02 = true;
                    //Debug.Log("任務2完成");
                }

                if (timeLeft <= 0)
                {
                    task03 = false;
                }
                else
                {
                    task03 = true;
                }
                break;
            case 4:
                break;
            case 5:
                break;

        }

        reTaskText();
    }
    public void reTaskText()
    {
        switch (GameManager.mapLevel)
        {
            case 2: //第一區前scenes01
                monsterNum = 20;
                TaskNum = 0;
                if (task01 == true)
                {
                    TaskText[0].text = "基礎移動、吸取蝴蝶 [✓] ";
                }
                else
                {
                    TaskText[0].text = "基礎移動、吸取蝴蝶 (0/1)";
                }

                if (task02 == true)
                {
                    TaskText[1].text = "擊敗20隻怪物 [✓] ";
                }
                else
                {
                    TaskText[1].text = $"擊敗20隻怪物 ( {killEmeny} / {monsterNum} )";
                }

                TaskText[2].text = "到達指定地點";
                break;
            case 3: //第一區後scenes02
                monsterNum = 20;
                TaskNum = 10;
                if (task01 == true)
                {
                    TaskText[0].text = "收集掉落物 [✓] ";
                }
                else
                {
                    TaskText[0].text = $"收集掉落物 ( {DropEmeny} / {TaskNum} )";
                }

                if (task02 == true)
                {
                    TaskText[1].text = "擊敗50隻怪物 [✓] ";
                }
                else
                {
                    TaskText[1].text = $"擊敗50隻怪物 ( {killEmeny} / {monsterNum} )";
                }

                TaskText[2].text = $"三分鐘內離開森林 {timeLeft}s";
                break;
            case 4:
                break;
            case 5:
                break;
        }
        
    }

    
    public void moveTask()
    {
        if (isTaskMoving)
        {
            return; // 如果任务正在移动，则不执行新的移动操作
        }

        int offset = isMovingRight ? 475 : -475; // 根据当前状态确定偏移量
        Vector2 targetPosition = new Vector2(rectTransform.anchoredPosition.x + offset, rectTransform.anchoredPosition.y);
        float duration = 1f; // 过渡持续时间

        isTaskMoving = true; // 设置任务正在移动中

        StartCoroutine(SmoothMove(rectTransform, targetPosition, duration, () =>
        {
            isTaskMoving = false; // 移动结束后将 isTaskMoving 设置为 false
        }));

        isMovingRight = !isMovingRight; // 切换移动方向
    }
    private IEnumerator SmoothMove(RectTransform rectTransform, Vector2 targetPosition, float duration, System.Action onMoveComplete = null)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;

        onMoveComplete?.Invoke(); // 执行移动完成后的回调函数
    }



}
