using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime;
    public Vector3 positionOffset;

    [Header("Axis Limitations")]
    public Vector2 xLimit;
    public Vector2 yLimit;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = target.position + positionOffset;
        targetPos = new Vector3(Mathf.Clamp(targetPos.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPos.y, yLimit.x, yLimit.y), -10 );
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
