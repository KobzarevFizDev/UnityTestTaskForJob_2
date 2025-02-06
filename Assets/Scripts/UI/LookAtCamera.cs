using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = Camera.main;
        Vector3 lookDir = (transform.position - camera.transform.position).normalized;
        transform.forward = lookDir;
        
    }
}
