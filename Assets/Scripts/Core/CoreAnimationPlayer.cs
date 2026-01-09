using System.Collections;
using UnityEngine;

public class CoreAnimationPlayer : MonoBehaviour
{
    public Animator bodyAnimator;
    private CharacterController player;

    float distance;
    WaitForSeconds checkDelay;

    [Header("Settings")]
    public float checkDistance = 10f;
    public float delay = 0.2f;
    public string animationName = "BodyAnimation";

    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
        checkDelay = new WaitForSeconds(delay);
        bodyAnimator.StopPlayback();
        StartCoroutine(CheckPosition());
    }

    IEnumerator CheckPosition()
    {
        while(true)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);

            if(distance < checkDistance )
            {
                bodyAnimator.Play(animationName);
            }
            yield return checkDelay;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkDistance);
    }
}
