using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Sprite[] BackgroundImages;
    public Image Background;

    void Update()
    {
        float image = Screen.currentResolution.height / BackgroundImages.Length;
        for (int i = 0; i < BackgroundImages.Length; i++)
        {
            if (Input.mousePosition.y < image * (i + 1))
            {
                Background.sprite = BackgroundImages[i];
                break;
            }
        }
    }
}
