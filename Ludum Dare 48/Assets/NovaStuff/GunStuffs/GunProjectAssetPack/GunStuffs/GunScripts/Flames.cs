using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    public Color firstColor;
    public Color secondColor;

    public SpriteRenderer partical;

    public float lifetime = .3f;
    // Start is called before the first frame update
    void Awake()
    {
        partical = gameObject.GetComponent<SpriteRenderer>();

        partical.color = Color.Lerp(firstColor, secondColor, Random.RandomRange(0, 1));


    }

    // Update is called once per frame
    void Update()
    {

        if (lifetime > 0)
        {
            gameObject.transform.localScale -= new Vector3(0.01f, .01f, .01f);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
}
