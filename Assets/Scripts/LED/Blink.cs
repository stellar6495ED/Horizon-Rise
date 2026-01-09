using UnityEngine;

public class Blink : MonoBehaviour
{
    [Header("LED data")]
    private Renderer ledRend;
    private Material ledMat;
    public float ledIntensity = 1.0f;

    [Header("Color Data")]
    public Color ledDefaultColor = Color.red;
    public Color ledColor = Color.cyan;

    [Header("Additional Data")]
    public float blinkInterval = 0.2f;

    private Color cachedActiveColor;
    private bool isActiveColor = false;
    private float timer = 0f;

    private void Awake()
    {
        ledRend = GetComponent<Renderer>();
        ledMat = ledRend.material;
        cachedActiveColor = ledColor * ledIntensity;

        ledMat.SetColor("_EmissionColor", ledDefaultColor);
    }

    private void Start()
    {
        ledMat.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > blinkInterval)
        {
            timer = 0f;
            isActiveColor = !isActiveColor;

            ledMat.SetColor("_EmissionColor", isActiveColor ? cachedActiveColor : ledDefaultColor);
        }
    }
}
