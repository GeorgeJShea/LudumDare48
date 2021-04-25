using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public List<Character> Enemies = new List<Character>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character _character = collision.GetComponentInParent<Character>();
        if (_character)
        {
            if (!Enemies.Contains(_character))
            {
                Enemies.Add(_character);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character _character = collision.GetComponentInParent<Character>();
        if (_character)
        {
            if (Enemies.Contains(_character))
            {
                Enemies.Remove(_character);
            }
        }
    }
}
