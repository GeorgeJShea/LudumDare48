using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target;
    [HideInInspector] public Camera cam;

    public static CameraController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 nextPos = target.position;
        Vector3 mouseDir = cam.ScreenToWorldPoint(Input.mousePosition) - Player.instance.transform.position;
        mouseDir = mouseDir.normalized;
        mouseDir *= 3;

        nextPos += mouseDir;

        nextPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, nextPos, 5f * Time.deltaTime);
    }
}
