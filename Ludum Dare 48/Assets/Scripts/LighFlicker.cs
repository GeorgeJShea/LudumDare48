using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class LighFlicker : MonoBehaviour
{
    public float minFlicker = .5f;
    public float maxFlicker = 1f;
    public int minLight = 2;
    public int maxLight = 4;
    private Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LightWait());
    }
    IEnumerator LightWait()
    {
        float delay = Random.Range(minFlicker, maxFlicker);
        yield return new WaitForSeconds(delay);

        light.intensity = Random.Range(minLight, maxLight);
    }
}
