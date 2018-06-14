﻿using UnityEngine;

public class SmoothCamera2d : MonoBehaviour
{

    public float dampTime = 0.15f;
    public float Xoffset = 0.65f;
    public float Yoffset = 0.4f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint((target.position));
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(Xoffset, Yoffset, point.z));
            Vector3 destination = transform.position + delta - new Vector3(0, 0.5f, 0);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}