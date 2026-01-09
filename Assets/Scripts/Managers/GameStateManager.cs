using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    float timer;

    [Header("Timer Settings")]
    public TextMeshProUGUI coundownText;

    [Header("Gameplay Settings")]
    public float maxGameplayTime = 450f;
    public GameObject gameoverPanel;

    private void Start()
    {
        coundownText.text = string.Empty;
        gameoverPanel.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        coundownText.text = $"{maxGameplayTime - timer:F0} <color=grey>Seconds</color>";

        if (timer >= maxGameplayTime)
        {
            coundownText.maxVisibleCharacters = 0;
            GameManager.instance.ActivateCursor(true);
            gameoverPanel.SetActive (true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}