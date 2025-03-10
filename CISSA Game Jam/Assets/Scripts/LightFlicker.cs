using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

public class GlobalLight2DFlicker : MonoBehaviour
{
    private Light2D globalLight;
    public float maxIntensity = 2.0f;
    public float minInterval = 2f;
    public float maxInterval = 5f;

    private void Start()
    {
        globalLight = GetComponent<Light2D>(); // Get the Light2D component
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            globalLight.intensity = maxIntensity;
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            globalLight.intensity = 0f; // Reset intensity to 0
        }
    }
}
