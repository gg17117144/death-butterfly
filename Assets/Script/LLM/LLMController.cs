using LLMUnity;
using UnityEngine;

public class LLMController : MonoBehaviour
{
    public LLM llm;
  
    void HandleReply(string reply){
        // do something with the reply from the model
        Debug.Log(reply);
    }
  
    void Game(){
        // your game function
        string message = "Hello bot!";
        _ = llm.Chat(message, HandleReply);
    }
}
