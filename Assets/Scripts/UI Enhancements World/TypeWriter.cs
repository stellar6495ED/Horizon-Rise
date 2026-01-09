using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    [Header("Settings")]
    public float characterDelay = 0.1f;
    public float characterClearTime = 0.5f;
    public float characterRestartTime = 0.1f;

    private TextMeshProUGUI text;
    private string message;

    int messageLength;
    WaitForSeconds delay;
    WaitForSeconds clearTime;
    WaitForSeconds restartTime;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();//Initilize TMPro

        message = text.text; //Fetch the complete text from TMP & stores them
        text.text = message; 

        //creates cache for reusable variables
        messageLength = message.Length;
        delay = new WaitForSeconds(characterDelay);
        clearTime = new WaitForSeconds(characterClearTime);
        restartTime = new WaitForSeconds(characterRestartTime);
        
        StartCoroutine(WriteText());//calls or start type writer effect
    }

    IEnumerator WriteText()
    {
        while (true) //Runs infinite times
        {
            for (int i = 0; i <= messageLength; i++) 
            {
                text.maxVisibleCharacters = i;
                yield return delay;
            }
            yield return clearTime;

            text.maxVisibleCharacters = 0;
            yield return restartTime;
        }
    }
}
