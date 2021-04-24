using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public int health;

    public void Update()
    {
        healthBar.fillAmount = Player.instance.Health / 100;
    }
}
