using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.IO;
using System.Linq;

[System.Serializable]
public class Dialogue
{
    public string character; // 對話的說話者
    public string context;   // 對話的內容
}

[System.Serializable]
public class DialogueData
{
    public int StoryLevel;   // 故事等級
    public List<Dialogue> dialogue; // 對話列表
}

[System.Serializable]
public class StoryData
{
    public List<DialogueData> Story; // 故事數據列表
}

public class TalkToTalk : MonoBehaviour
{
    public GameObject TalkUI;
    
    [SerializeField] private TextAsset jsonFilePath; // JSON文件的路徑
    //public List<Dialogue> dialogues;
    public Text nametext; // 顯示說話者名字的UI元素
    public Text messagetext; // 顯示對話內容的UI元素

    [SerializeField] private DialogueData dialogueData; // 對話數據
    [SerializeField] private StoryData storyData; // 將對話數據類型更改為 StoryData
    [SerializeField] private int storyLevel; // 目前對話的索引
    [SerializeField] private int currentIndex = 0; // 目前對話的索引
    [SerializeField] private bool isTyping = false; // 是否正在打印對話

    [SerializeField] private bool checckk;

    public Animator animator;
    private void Start()
    {
        ReadJsonFile(); // 讀取JSON文件
        //ShowDialogueByStoryLevel(0);
        
        TalkUI.SetActive(false);
    }

    [Button]
    public void startTalk()
    {
        ShowDialogueByStoryLevel(storyLevel); // 顯示對話
    }

    void Update()
    {
        if (GameManager.instance.isTalking)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                //Debug.Log("跳出對話");
                GameManager.instance.isTalking = false;
                currentIndex = 0;
                animator.Play("disappear");
                TalkUI.SetActive(false);
            }
            
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
            {
                if (isTyping)
                {
                    // 如果正在打印對話，則直接跳過打印並顯示全部對話
                    StopCoroutine(TypeMessage(dialogueData.dialogue[currentIndex].context));
                    //messagetext.text = dialogueData.dialogue[currentIndex].context;
                    //isTyping = false;
                }
                else
                {
                    //Debug.Log($"應該要加到{currentIndex}");
                    NextDialogue();
                }
            }
        }
    }
    
    private void ReadJsonFile() //讀取故事檔案
    {
        // 指定JSON文件的名稱（不要包括文件擴展名）
        string jsonFileName = "dialogue";

        // 使用Resources.Load來讀取JSON文件
        jsonFilePath = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFilePath != null)
        {
            string jsonData = jsonFilePath.text;

            // 現在你可以解析JSON數據，並將其轉換為 StoryData 對象
            storyData = JsonUtility.FromJson<StoryData>(jsonData);

            // 不要忘記釋放資源
            Resources.UnloadAsset(jsonFilePath);
        }
        else
        {
            Debug.LogError("找不到JSON文件: " + jsonFileName);
        }
    }
    

    // 新增函数，根据故事等级展示对话
    public void ShowDialogueByStoryLevel(int _storyLevel)//對外
    {
        storyLevel = _storyLevel;
        // 首先檢查 storyData 是否為 null
        if (storyData != null && storyData.Story != null)
        {
            GameManager.instance.isTalking = true;
            //storyLevel = 0;            storyLevel = _storyLevel;
            TalkUI.SetActive(true);
            animator.Play("Appear");
            string aaa = storyData.Story[0].dialogue[0].context.ToString();
            // 根據傳入的故事等級查找對應的 StoryData
            DialogueData foundDialogueData = storyData.Story[_storyLevel];
            
            if (foundDialogueData != null)
            {
                //Debug.Log($"此對話的長度{foundDialogueData.dialogue.Count()}");
                for (int i = 0; i < foundDialogueData.dialogue.Count(); i++)
                {
                    //Debug.Log($"此對話的所有對話:{foundDialogueData.dialogue[i].context}");
                }
                // 重置索引並展示對話
                currentIndex = 0;
                dialogueData = foundDialogueData;
                ShowDialogue(_storyLevel);
            }
            else
            {
                Debug.LogError("找不到故事等級: " + _storyLevel);
            }
        }
        else
        {
            Debug.LogError("故事數據為 null 或不存在");
        }
    }
    
    private void ShowDialogue(int storyLevel)
    {
        if (storyData != null && storyData.Story != null && currentIndex < storyData.Story[storyLevel].dialogue.Count())
        {
            DialogueData currentDialogueData = storyData.Story[storyLevel];
            Dialogue currentDialogue = currentDialogueData.dialogue[currentIndex];
            nametext.text = currentDialogue.character; // 顯示說話者名字
            StartCoroutine(TypeMessage(currentDialogue.context)); // 逐字打印對話內容
        }
        else
        {
            GameManager.instance.isTalking = false;
            TalkUI.SetActive(false);
            animator.Play("disappear");
        }
    }
    
    
    private IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        messagetext.text = ""; // 清空對話內容
        for (int i = 0; i < message.Length; i++)
        {
            messagetext.text += message[i]; // 逐字添加對話內容
            yield return new WaitForSeconds(0.05f); // 等待一段時間後繼續
        }
        isTyping = false;
    }

    public void NextDialogue()
    {
        currentIndex++; // 切換到下一句對話
        ShowDialogue(storyLevel); // 顯示下一句對話
        //Debug.Log($"currentIndex:{currentIndex} \n dialogueData.dialogue.Count:{dialogueData.dialogue.Count}");
        if (currentIndex >= dialogueData.dialogue.Count)
        {
            currentIndex = 0; //重置0
        }
    }

}
