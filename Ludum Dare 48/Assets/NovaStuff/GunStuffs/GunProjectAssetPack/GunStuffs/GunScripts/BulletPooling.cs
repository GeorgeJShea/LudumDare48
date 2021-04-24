using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling bulletPoolInstance;

    public GameObject pooledBullets;
    private bool notEnoughBullet = true;

    private List<GameObject> bullets;

    // Start is called before the first frame update
    void Awake()
    {
        bulletPoolInstance = this;
    }

    private void Start()
    {
        bullets = new List<GameObject>();
    }
    public GameObject GetBullet()
    {
        if(bullets.Count  > 0)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                if(!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if(notEnoughBullet)
        {
            GameObject bul = Instantiate(pooledBullets);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }
        return null;

    }
}
