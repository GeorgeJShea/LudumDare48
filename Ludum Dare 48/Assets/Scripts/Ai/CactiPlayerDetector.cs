using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactiPlayerDetector : MonoBehaviour
{
    private Cacti ai;

    private void Start()
    {
        ai = GetComponentInParent<Cacti>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ai.isPlayerClose = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ai.isPlayerClose = false;
    }
}
