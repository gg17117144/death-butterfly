using UnityEngine;

public class EndTP : MonoBehaviour
{
    public Sprite insideLight;
    public int lever;
    public bool rot;
    public bool fCheck;
    public GameObject insideObGameObject;

    public GameObject waitIng;

    bool isChangScene;

    private GameObject player;
    private Animator insideAnimator;

    public bool isOver = true;

    public Animator animator;

    private void Start()
    {
        //animator.GetComponent<Animator>();
        animator.Play("appear");
        //GameManager.isStoping = false;
        if (waitIng != null)
        {
            waitIng.SetActive(false);
        }

        player = GameManager.instance.player;
        insideAnimator = insideObGameObject.GetComponent<Animator>();

        lever = GameManager.instance.SceneIndex + 1;
    }

    private void Update()
    {
        if (rot == true)
        {
            transform.Rotate(Vector3.forward, 50 * Time.deltaTime);
        }

        switch (GameManager.instance.SceneIndex)
        {
            case 2: //第一區前
                break;
            case 3: //第一區後
                break;
            case 4: //第二區前
                break;
        }

        //Debug.Log(GameManager.instance.canTP);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other is CapsuleCollider2D && !GameManager.instance.isStoping)
        {
            if (other.CompareTag("Player") && GameManager.instance.canTP)
            {
                insideAnimator.SetBool("isLight", true);
                //Debug.Log("任務完成");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("我按下了");
                    if (lever != 5) //有完成
                    {
                        GameManager.mapLevel++;
                        Task.instance.ResetTask();

                        LoadScene(lever);
                    }
                    else
                    {
                        //UIControl.instance.DebugText("還沒做完拉");
                        UIControl.instance.GetComponent<TalkToTalk>().ShowDialogueByStoryLevel(4);
                    }
                }
            }

            if (!fCheck)
            {
                if (other.CompareTag("Player"))
                {
                    //Debug.Log("觸發加載過場動畫");
                    player.transform.position = new Vector3(0, 0, 0);
                    LoadScene(lever);
                    //SceneManager.LoadScene(lever);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            insideAnimator.SetBool("isLight", false);
        }
    }

    void LoadScene(int sceneNum)
    {
        GameManager.instance.sceneController.LoadScene(sceneNum);
    }
}