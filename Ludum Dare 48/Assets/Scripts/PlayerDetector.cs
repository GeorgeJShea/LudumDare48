using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private Ai ai;

    private void Start()
    {
        ai = GetComponentInParent<Ai>();
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
