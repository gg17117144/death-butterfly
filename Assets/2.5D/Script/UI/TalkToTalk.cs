using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


[System.Serializable]
public class Dialogue
{
    public string speaker; // 對話的說話者
    public string message; // 對話的內容
}

[System.Serializable]
public class DialogueData
{
    public Dialogue[] dialogues; // 對話數組
}

public class TalkToTalk : MonoBehaviour
{
    [SerializeField] private string jsonFilePath; // JSON文件的路徑

    public Text nametext; // 顯示說話者名字的UI元素
    public Text messagetext; // 顯示對話內容的UI元素

    private DialogueData dialogueData; // 對話數據
    private int currentIndex = 0; // 目前對話的索引
    private bool isTyping = false; // 是否正在打印對話

    private void Start()
    {
        ReadJsonFile(); // 讀取JSON文件
    }

    public void startTalk()
    {
        //ReadJsonFile(); // 讀取JSON文件
        ShowDialogue(); // 顯示對話
    }

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // 如果正在打印對話，則直接跳過打印並顯示全部對話
                StopCoroutine(TypeMessage(dialogueData.dialogues[currentIndex].message));
                messagetext.text = dialogueData.dialogues[currentIndex].message;
                isTyping = false;
            }
            else
            {
                NextDialogue();
            }
        }
    }

    private void ReadJsonFile()
    {
        string jsonData = File.ReadAllText(jsonFilePath); // 讀取JSON文件的內容
        dialogueData = JsonUtility.FromJson<DialogueData>(jsonData); // 解析JSON數據為對話數據對象
    }

    private void ShowDialogue()
    {
        if (dialogueData != null && dialogueData.dialogues != null && currentIndex < dialogueData.dialogues.Length)
        {
            Dialogue currentDialogue = dialogueData.dialogues[currentIndex]; // 獲取目前對話的對話內容
            nametext.text = currentDialogue.speaker; // 顯示說話者名字
            StartCoroutine(TypeMessage(currentDialogue.message)); // 逐字打印對話內容
        }
        else
        {
            gameObject.SetActive(false); // 所有對話結束後關閉對話框
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

        if (currentIndex >= dialogueData.dialogues.Length)
        {
            currentIndex = 0; //重置0
        }

        ShowDialogue(); // 顯示下一句對話
    }
}
