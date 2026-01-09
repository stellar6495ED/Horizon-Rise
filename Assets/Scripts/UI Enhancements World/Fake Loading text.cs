using System.Collections;
using TMPro;
using UnityEngine;

public class FakeLoadingtext : MonoBehaviour
{
    private TextMeshProUGUI text;

    public float speed = 0.0245f;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        text.text = "";
        StartCoroutine(CalculatePercent());
    }

    IEnumerator CalculatePercent()
    {
        for (int i = 0; i < 101; i++)
        {
            text.text = i.ToString() + " %";
            yield return new WaitForSeconds(speed);
        }
    }
}
