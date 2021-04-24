using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System.Collections;


public class Transition : MonoBehaviour
{
    public int nextLevel;
    public bool died = false;
    public void Clicked()
    {
        StartCoroutine(FadeImage(nextLevel, died));
    }
    public IEnumerator FadeImage(int nextSceneToLoad, bool died)
    {
        //Sets up scene
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        // Grabs fade object infront of camera. 
        GameObject screenFade = GameObject.Find("ScreenFade");

        Image image = screenFade.GetComponent<Image>();

        // Grabs u died game object and sets it off
        GameObject uDied = transform.GetChild(0).gameObject;
        uDied.active = false;

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
        //Set to true if you are respawning and to current level for respawn
        if(died == true)
        {
            uDied.active = true;
            yield return new WaitForSeconds(3);

            Debug.Log("you ded");
            //SceneManager.LoadScene(nextSceneToLoad);
        }

        Debug.Log("next scene tottaly loaded got o script when we actually have scenes");
        //SceneManager.LoadScene(nextSceneToLoad);
    }
}
