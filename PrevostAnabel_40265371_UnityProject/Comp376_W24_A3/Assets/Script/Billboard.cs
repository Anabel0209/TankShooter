using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    void LateUpdate()
    {
        //rotate the billboards towards the camera
        transform.LookAt(transform.position + camera.gameObject.transform.forward);
    }
}
