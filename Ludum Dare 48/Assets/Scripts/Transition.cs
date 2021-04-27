using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System.Collections;


public class Transition : MonoBehaviour
{
    public GameObject screenFade;

    public void Clicked()
    {
        StartCoroutine(FadeImage());
    }
    public IEnumerator FadeImage()
    {
        // Grabs fade object infront of camera. 
        GameObject screenFade = GameObject.Find("ScreenFade");

        Image image = screenFade.GetComponent<Image>();
        image.enabled = true;

        // Grabs u died game object and sets it off
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
       
        SceneManager.LoadScene(1);
    }
}
