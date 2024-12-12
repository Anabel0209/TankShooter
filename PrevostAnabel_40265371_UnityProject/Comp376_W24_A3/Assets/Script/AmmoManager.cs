using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public MachineGun myMachineGun;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("AmmoRefill"))
        {
            myMachineGun.energy = 100f;
            myMachineGun.energyBar.SetHealth(100f);
            Destroy(collision.gameObject);
        }
    }
}
