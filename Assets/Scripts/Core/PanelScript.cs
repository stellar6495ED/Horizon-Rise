using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelScript : MonoBehaviour
{
    private CharacterController player;
    private PlayerInputActions playerInteractions;

    float distance;


    [Header("Settings")]
    public float minCheckDistance = 2f;
    public GameObject interactionUI;
    public CanvasRenderer canvas;
    public GameObject credits;


    private void Awake()
    {
        player = FindObjectOfType<CharacterController>();
        playerInteractions = new PlayerInputActions();
    }

    private void Start()
    {
        interactionUI = Instantiate(interactionUI, canvas.transform);
        credits.SetActive(false);
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
        playerInteractions.Player.Interactions.performed += Interact;
    }
    private void OnEnable()
    {
        playerInteractions.Player.Interactions.Enable();
    }
    private void OnDisable()
    {
        playerInteractions.Player.Interactions.Disable();
        playerInteractions.Player.Interactions.performed -= Interact;
    }

    void Interact(InputAction.CallbackContext ctx)
    {
        ctx.ReadValueAsButton();
        credits.SetActive(true);
    }
}
