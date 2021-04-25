using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera camera;
    public float sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue = gameObject.GetComponent<Slider>().value;
        camera.orthographicSize = sliderValue;
    }
}
