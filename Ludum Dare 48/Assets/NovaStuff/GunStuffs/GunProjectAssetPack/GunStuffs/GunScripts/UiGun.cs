/*
_______________________________________________________
Summary:
chagnes ui elements to reflect state of guns

_______________________________________________________

 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiGun : MonoBehaviour
{
    public TextMeshProUGUI clipText;
    public TextMeshProUGUI ammoText;


    [Header("Reload Ani")]

    public float reloadTime;
    public float timeLeft;
    public Image ticker;

    public bool reloadAnimBool;

    public void Start()
    {
        ticker.GetComponent<Image>();
    }
    public void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            ticker.fillAmount = timeLeft / reloadTime;
        }
        else
        {
            ticker.fillAmount = 1;
            timeLeft = 0;
        }
    }
    public void ammoClipSet(float clip, float ammoPool)
    {
        clipText.text = clip.ToString();
        ammoText.text = ammoPool.ToString();
    }

    public void ReloadAni (float reloadTimer)
    {
        timeLeft = reloadTimer;
        reloadTime = reloadTimer;
    }
}
