using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Transform hitFX;

    private void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
