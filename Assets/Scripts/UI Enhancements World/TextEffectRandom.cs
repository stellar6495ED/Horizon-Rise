using System.Collections;
using TMPro;
using UnityEngine;

public class TextEffectRandom : MonoBehaviour
{
    private TextMeshProUGUI text;

    [Header("Settings")]
    public float delay = 0.05f;

    string message;
    int alphabetLength;
    WaitForSeconds waitTime;
    

    char[] alphabets = {
    'A','B','C','D','E','F','G','H','I','J',
    'K','L','M','N','O','P','Q','R','S','T',
    'U','V','W','X','Y','Z'
    };

    private void Start()
    {
        waitTime = new WaitForSeconds(delay);
        text = GetComponent<TextMeshProUGUI>();
        message = text.text.ToUpper();

        alphabetLength = alphabets.Length;
        text.text = string.Empty;
        StartCoroutine(EffectGeneration());
    }


    IEnumerator EffectGeneration()
    {
        if (message[0] == alphabets[0])
        {
            text.text = message[0].ToString();
        }
        else
        {
            for (int i = 1; i < alphabetLength; i++)
            {
                if (message[0] == alphabets[i])
                {
                    text.text = message[0].ToString();
                    break;
                }
                else
                {
                    text.text = alphabets[i].ToString();
                }
                yield return waitTime;
            }
        }  
    }
}
