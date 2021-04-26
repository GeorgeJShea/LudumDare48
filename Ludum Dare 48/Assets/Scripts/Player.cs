using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Animator anim;
    public GameObject WeaponsHolder;
    public GameObject Hands;
    public PlayerMovement Movement;

    public Flamethrower Flamethrower;
    public AdvGunLogic Shotgun;

    public AudioClip[] StepSounds;

    [HideInInspector] public Rigidbody2D rb;

    public static Player instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }
            return _instance;
        }
    }
    private static Player _instance;

    public override void Awake()
    {
        base.Awake();

        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    public void PickupSnakeOil(float amount)
    {
        Flamethrower.ammoPool += amount;
    }
    public void PickupShotgunAmmo(float amount)
    {
        Shotgun.ammoPool += amount;
    }


    private void Update()
    {
        if (isDead) return;

        //anim.SetFloat("X", rb.velocity.x);
        Vector3 mouseDir = CameraController.instance.cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        anim.SetFloat("X", mouseDir.x);

        anim.SetBool("IsMoving", rb.velocity.sqrMagnitude != 0);
    }

    public override void Damage(float damage)
    {
        if (isDead) return;

        anim.Play("Hurt", 0, 0);

        base.Damage(damage);
    }

    public override void Die()
    {
        if (isDead) return;
        StartCoroutine(IDie());
        //transform.position = LevelManager.instance.SpawnPoint.position;
        //health = StartHealth;
        //gameObject.SetActive(false);
    }

    public IEnumerator IDie()
    {
        isDead = true;

        anim.Play("Death", 0, 0);

        Movement.freezeInput = true;
        Hands.SetActive(false);
        WeaponsHolder.SetActive(false);

        yield return new WaitForSeconds(2);

        anim.Play("Idle");

        Movement.freezeInput = false;
        Hands.SetActive(true);
        WeaponsHolder.SetActive(true);

        transform.position = LevelManager.instance.SpawnPoint.position;
        health = StartHealth;

        isDead = false;
    }

    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, StartHealth);
    }

    public override void AnimEvent()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            SoundManager.instance.PlaySound(StepSounds[Random.Range(0, StepSounds.Length)], transform.position, 0.15f, 0.9f);
        }
    }

    public override void MoveCharacter(Vector3 by)
    {
    }

    public void MovePlayer(Ai by)
    {
    }
}
