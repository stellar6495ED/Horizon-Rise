using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController player;
    private Camera cam;

    Vector3 cameraForword;
    Vector3 cameraRight;
    Vector3 moveDirection;
    Quaternion targetRotation;
    Vector3 finalMove;
    Vector3 camTargetRotation;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float yaw; //Horizontal Rotation
    private float pitch; //Vertical Rotation
    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;

    [Header("Player Data")]
    public float speed = 5f;
    public float rotationSpeed = 0.5f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public float sensitivity = 10f;
    public float rotationSmoothTime = 0.05f;
    public Vector2 camPitch = new Vector2(-10f, 15f);


    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        player = GetComponent<CharacterController>();

        if (!cam) Debug.Log("Camera not found in children");

        if (!player) Debug.Log("Player not found");

        if (sensitivity <= 0) sensitivity = 10f;

        if (speed <= 0) speed = 8f;
    }

    private void Update()
    {
        if(GameManager.instance.IsInputEnabled())
        {
            MovementAndGravity();
            UpdateRotation();
        }   
    }

    void MovementAndGravity()
    {
        moveInput = GameManager.instance.moveInput;

        cameraForword = cam.transform.forward;
        cameraRight = cam.transform.right;

        cameraForword.y = 0f;
        cameraRight.y = 0f;

        cameraForword.Normalize();
        cameraRight.Normalize();

        moveDirection = cameraRight * moveInput.x + cameraForword * moveInput.y;

        if (moveDirection.sqrMagnitude >= 0.0001f)
        {
            targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }

        if (!player.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        finalMove = moveDirection * speed + new Vector3(0f, velocity.y, 0f);

        player.Move(finalMove * Time.deltaTime);
    }

    void UpdateRotation()
    {
        lookInput = GameManager.instance.lookInput;

        yaw += lookInput.x * sensitivity * Time.deltaTime;
        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, camPitch.x, camPitch.y);

        camTargetRotation = new Vector3(pitch, yaw, 0f);
        currentRotation = Vector3.SmoothDamp(currentRotation, camTargetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation + new Vector3(0f, 180f, 0f);
    }
}