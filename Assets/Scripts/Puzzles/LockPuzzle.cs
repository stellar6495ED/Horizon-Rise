using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockPuzzle : MonoBehaviour
{ 
    [Header("Password Settings")]
    public string correctPassword = "3582";
    private int maxPasswordLength = 4;
    public LockTrigger trigger;

    [Header("UI Settings")]
    public TextMeshProUGUI passwordDisplay;
    public GameObject lockCanva;
    public Button[] buttons;

    [Header("Door Settings")]
    public TextMeshProUGUI doorText;
    public Collider doorCollider;

    [Header("SFX settings")]
    public AudioClip buttonPressSFX;
    public AudioClip WrongPassSFX;

    private StringBuilder passwordInput = new StringBuilder();
    private bool isLocked = true;

    private Vector3 originalCanvasPosition;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ActivateCollider(true);
        ResetDisplay();
        if(lockCanva != null)
        {
            originalCanvasPosition = lockCanva.transform.position;
        }
    }

    public void AddDigit(string digit)
    {
        if (!isLocked) return;
        if (passwordInput.Length >= maxPasswordLength) return;

        passwordInput.Append(digit);
        UpdateDisplay();
        audioSource.PlayOneShot(buttonPressSFX);

        if(passwordInput.Length >= maxPasswordLength)
        {
            SubmitPassword();
        }
    }

    public void ClearInput()
    {
        if (!isLocked) return;
        passwordInput.Clear();
        UpdateDisplay();
    }

    public void SubmitPassword()
    {
        if (!isLocked) return;

        if(passwordInput.ToString() == correctPassword)
        {
            SetButtonInteractables(false);
            UnlockDoor();
            DeactivateTrigger();
            Invoke(nameof(CloseInterface), 2f);
        }
        else
        {
            WrongPassword();
            Invoke(nameof(ResetDisplay), 1f);
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        ActivateCollider(false);
        GameManager.instance.ActivateCursor(false);
        passwordDisplay.color = Color.green;
        doorText.text = "UNLOCKED";
        doorText.faceColor = Color.green;
    }

    private void WrongPassword()
    {
        audioSource.PlayOneShot(WrongPassSFX);
        passwordDisplay.color = Color.red;
        Invoke(nameof(ClearInput), 1f);
    }

    private void ResetDisplay()
    {
        string placeholder = "";
        for (int i = 0; i < maxPasswordLength; i++)
        {
            placeholder += "-";
        }
        passwordDisplay.text = placeholder;
        passwordDisplay.color = Color.gray;
    }

    private void SetButtonInteractables(bool interact)
    {
        foreach(Button button in buttons)
            button.interactable = interact;
    }

    public void OpenInterface()
    {
        lockCanva.SetActive(true);
    }

    public void CloseInterface()
    {
        passwordInput.Clear();
        ResetDisplay();
        GameManager.instance.ActivateCursor(false);
        lockCanva.SetActive(false);
    }

    void UpdateDisplay()
    {
        string display = "";
        for (int i = 0; i < passwordInput.Length ;i++)
        {
            display += "*";
        }
        for (int i = passwordInput.Length; i< maxPasswordLength; i++)
        {
            display += "-";
        }

        passwordDisplay.text = display;
        passwordDisplay.color = Color.white;
    }

    void ActivateCollider(bool state)
    {
        doorCollider.enabled = state;
    }

    void DeactivateTrigger()
    {
        trigger.enabled = false;
        trigger.EnableUI(false);
        Destroy(trigger);
    }
}
