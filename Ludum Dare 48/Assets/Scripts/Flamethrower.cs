using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Item
{
    [Tooltip("Amount of ammo available for the gun to use")]
    public float ammoPool;

    public EnemyTrigger Trigger;

    [Tooltip("How much damage the bullet will do")]
    public float damage;

    public ParticleSystem FlameEffect;

    [HideInInspector]
    public UiGun uiComponent;    //Makes refrence to ui script

    void Start()
    {
        uiComponent = GameObject.Find("UiManger").GetComponent<UiGun>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FlameEffect.Play();
        }
        if (Input.GetMouseButtonUp(0) || ammoPool <= 0)
        {
            FlameEffect.Stop();
        }

        if (Input.GetMouseButton(0) && ammoPool > 0)
        {
            ammoPool -= Time.deltaTime;
            for (int i = 0; i < Trigger.Enemies.Count; i++)
            {
                if (!CheckWallBetween(Trigger.Enemies[i], transform.position))
                {
                    if (Trigger.Enemies[i])
                    {
                        Trigger.Enemies[i].Damage(damage * Time.deltaTime);
                    }
                }
            }
        }

        uiComponent.ammoClipSet(Mathf.Ceil(ammoPool), Mathf.Infinity);
    }

    public bool CheckWallBetween(Character toCheck, Vector3 fromPos)
    {
        Vector2 toCheckDir = toCheck.transform.position - fromPos;
        RaycastHit2D hit = Physics2D.Raycast(fromPos, toCheckDir, Vector2.Distance(fromPos, toCheck.transform.position), LayerMask.GetMask("wall"));
        return hit.collider;
    }
}
