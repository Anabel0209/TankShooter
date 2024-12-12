using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAmo : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("destroy");
            Destroy(gameObject, 0.1f);
        }
    }
}
