using System.Collections;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [Header("Settings")]
    public TextMeshProUGUI creditDisplay;
    public string[] texts;
    public float delay = 2f;
    public TextMeshProUGUI countdownTimer;

    int textAmount;
    float timer;
    float maxTime = 10f;

    private void Start()
    {
        creditDisplay.text = string.Empty;
        textAmount = texts.Length;

        StartCoroutine(ShowCredits());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        countdownTimer.text = $"Quitting In {maxTime - timer:f0}s";
        if (timer >= maxTime)
        {
            QuitGame();
        }
    }

    IEnumerator ShowCredits()
    {
        for (int i = 0; i < textAmount; i++)
        {
            creditDisplay.text = texts[i];
            yield return new WaitForSeconds(delay);
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
