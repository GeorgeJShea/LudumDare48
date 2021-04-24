using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class Transition : MonoBehaviour
{
    public IEnumerator FadeImage(int nextSceneToLoad)
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        GameObject screenFade = GameObject.Find("ScreenFade");

        Image image = screenFade.GetComponent<Image>();

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
        Debug.Log("next scene tottaly loaded got o script when we actually have scenes");
        //SceneManager.LoadScene(nextSceneToLoad);
    }
}
