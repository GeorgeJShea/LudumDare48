using System.Diagnostics;
using UnityEngine;

public class GunDropAni : MonoBehaviour
{
    [Header("DROP ANIMATION SETTINGS")]
    public GameObject dropShadow;
    // This are decent deafults if you using the same scale 
    [Tooltip("How far shadow will spawn below the weapon")]
    public float dropShadowDistance = .6f;
    [Tooltip("How fast the weapon will bob up and down")]
    public float speed = .04f;
    [Tooltip("How height the weapon will bob")]
    public float height = .05f;

    [HideInInspector]
    public bool droppedBool = true;

    private float currentHeight;
    private GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        currentHeight = gameObject.transform.position.y;
        drop = Instantiate(dropShadow, new Vector3(transform.position.x, transform.position.y - dropShadowDistance, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(droppedBool == true)
        {

            float y = Mathf.PingPong(Time.time * speed, height) * 6 + currentHeight;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, 0);
        }
    }
    public void pickWeapon()
    {
        Destroy(drop);
        droppedBool = false;
    }
    public void dropWeapon()
    {
        currentHeight = gameObject.transform.position.y;
        drop = Instantiate(dropShadow, new Vector3(transform.position.x, transform.position.y - dropShadowDistance, 0), Quaternion.identity);
        droppedBool = true;
    }
}
