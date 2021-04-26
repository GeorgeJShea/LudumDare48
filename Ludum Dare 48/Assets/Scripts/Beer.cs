using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    public float Amount;

    private float SpawnTime;

    private void Start()
    {
        SpawnTime = Time.time;
    }

    private void Update()
    {
        float newScale = Mathf.Lerp(0, 1, Time.time - SpawnTime);
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.instance.Heal(Amount);
        GetComponent<GunDropAni>().pickWeapon();
        Destroy(gameObject);
    }
}
