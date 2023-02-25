using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos, startposy;
    public GameObject cam;
    public float parallaxEffect, parallaxEffectY;

    private void Start()
    {
        startpos = transform.position.x;
        startposy = transform.position.y;
    }

    private void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);
        transform.position = new Vector3(startpos + dist,transform.position.y, transform.position.z);
    }
}
