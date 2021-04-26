using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flamethrower : Item
{
    [Tooltip("Amount of ammo available for the gun to use")]
    public float ammoPool;

    public EnemyTrigger Trigger;

    [Tooltip("How much damage the bullet will do")]
    public float damage;

    private float startedTime;

    public Light2D light;

    public ParticleSystem FlameEffect;

    [HideInInspector]
    public UiGun uiComponent;    //Makes refrence to ui script

    public AudioSource audioSource;

    public AudioClip FlamethrowerStart;
    public AudioClip FlamethrowerEnd;

    private float initialLightIntensity;
    private float lastLightIntesity;

    void Start()
    {
        uiComponent = GameObject.Find("UiManger").GetComponent<UiGun>();

        initialLightIntensity = light.intensity;
    }

    public void Update()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            FlameEffect.Play();
            light.enabled = true;
            startedTime = Time.time;
            lastLightIntesity = light.intensity;

            if (FlamethrowerStart) SoundManager.instance.PlaySound(FlamethrowerStart, transform.position, 1);
            audioSource.Play();
        }
        if (Input.GetMouseButtonUp(0) || ammoPool <= 0)
        {
            FlameEffect.Stop();
            startedTime = Time.time;

            if (FlamethrowerEnd) SoundManager.instance.PlaySound(FlamethrowerEnd, transform.position, 1);
            audioSource.Stop();
        }

        if (Input.GetMouseButton(0) && ammoPool > 0)
        {
            light.intensity = Mathf.Lerp(lastLightIntesity, initialLightIntensity * Random.Range(0.9f, 1.1f), Time.time - startedTime);
            lastLightIntesity = light.intensity;

            if ((Time.time - startedTime) > 0.25f)
            {
                ammoPool -= Time.deltaTime;
                for (int i = 0; i < Trigger.Enemies.Count; i++)
                {
                    if (Trigger.Enemies[i])
                    {
                        if (!CheckWallBetween(Trigger.Enemies[i], transform.position))
                        {
                            if (Trigger.Enemies[i])
                            {
                                Trigger.Enemies[i].Damage(damage * Time.deltaTime);
                            }
                        }
                    }
                    else
                    {
                        Trigger.Enemies.RemoveAt(i);
                    }
                }
            }
        }
        else
        {
            light.intensity = Mathf.Lerp(lastLightIntesity, 0, Time.time - startedTime);
            if (light.intensity == 0 && light.gameObject.activeInHierarchy)
            {
                light.enabled = false;
            }
        }

        uiComponent.ammoClipSet(Mathf.Ceil(ammoPool), Mathf.Infinity);
    }

    public bool CheckWallBetween(Character toCheck, Vector3 fromPos)
    {
        Vector2 toCheckDir = toCheck.GetPosition() - fromPos;
        RaycastHit2D hit = Physics2D.Raycast(fromPos, toCheckDir, Vector2.Distance(fromPos, toCheck.GetPosition()), LayerMask.GetMask("wall"));
        return hit.collider;
    }
}
