using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public int weaponID = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.BroadcastMessage("UnlockWeapon", weaponID);
            Destroy(this.gameObject);
        }
    }

}
