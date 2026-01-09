using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ServerLockTrigger : MonoBehaviour
{
    private CharacterController player;
    private PlayerInputActions interactionAction;

    float distance;

    [Header("Settings")]
    public float minCheckDistance = 2f;
    public GameObject interactionUI;
    public CanvasRenderer canvas;
    public GameObject doorLock;

    private void Awake()
    {
        interactionAction = new PlayerInputActions();
    }

    private void Start()
    {
        player = FindAnyObjectByType<CharacterController>();

        interactionUI = Instantiate(interactionUI, canvas.transform);

        EnableUI(false);
        if (!player)
            Debug.Log("Player not found");

        StartCoroutine(CheckPlayerPosition());
    }

    IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= minCheckDistance)
            {
                EnableUI(true);
                EnableInteraction();
            }
            else
            {
                EnableUI(false);
                DeactiveLock();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minCheckDistance);
    }

    void EnableUI(bool state)
    {
        interactionUI.SetActive(state);
    }

    void EnableInteraction()
    {
        interactionAction.Player.Interactions.performed += Interact;
    }

    public void Activatelock()
    {
        doorLock.SetActive(true);
    }

    public void DeactiveLock()
    {
        doorLock.SetActive(false);
    }

    private void OnEnable()
    {
        interactionAction.Player.Interactions.Enable();
    }
    private void OnDisable()
    {
        interactionAction.Player.Interactions.Disable();
        interactionAction.Player.Interactions.performed -= Interact;
    }

    void Interact(InputAction.CallbackContext ctx)
    {
        ctx.ReadValueAsButton();
        Activatelock();
        GameManager.instance.ActivateCursor(true);
        Debug.Log("Lock activated");
    }
}
