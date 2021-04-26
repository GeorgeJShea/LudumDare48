using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactiNeedlePickup : MonoBehaviour
{
    public Vector2 Amount;
    public AudioClip[] PickupSounds;
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
        if (PickupSounds.Length > 0)
        {
            SoundManager.instance.PlaySound(PickupSounds[Random.Range(0, PickupSounds.Length)], transform.position, 0.5f);
        }

        Player.instance.PickupShotgunAmmo((int)Random.Range(Amount.x, Amount.y));
        GetComponent<GunDropAni>().pickWeapon();
        Destroy(gameObject);
    }
}
