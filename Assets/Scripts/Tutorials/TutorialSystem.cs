using TMPro;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    [Header("Help Menu Settings")]
    public GameObject helpMenu;
    public TextMeshProUGUI helpText;
    public string[] messages;

    private void Start()
    {
        helpMenu.SetActive(false);
    }

    public void OpenHelpMenu()
    {
        GameManager.instance.ActivateCursor(true);
        helpMenu.SetActive(true);
        ShowBinTable();
    }

    public void CloseHelpMenu()
    {
        GameManager.instance.ActivateCursor(false);
        helpMenu.SetActive(false);
    }

    public void ShowBinTable()
    {
        helpText.text = string.Empty;
        helpText.text = messages[0];
    }

    public void ShowBinAddMenu()
    {
        helpText.text = string.Empty;
        helpText.text = messages[1];
    }

    public void MovementControls()
    {
        helpText.text = string.Empty;
        helpText.text = messages[2];
    }

    public void UnwantedStiuations()
    {
        helpText.text = string.Empty;
        helpText.text = messages[3];
    }

    public void AboutInfo()
    {
        helpText.text = string.Empty;
        helpText.text = messages[4];
    }
}
