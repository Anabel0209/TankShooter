using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTerret : MonoBehaviour
{
    public float spinSpeed = 90.0f;

    public Vector2 spin;

    // Update is called once per frame
    void Update()
    {
        

        spin.x += Input.GetAxis("Mouse X") * spinSpeed;
        transform.localRotation = Quaternion.Euler(0, spin.x, 0);

    }


}
