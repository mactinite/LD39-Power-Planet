using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Transform hitFX;
    public float pulseAmount = 0.5f;
    private Vector3 originalScale;
    private void Start()
    {
        
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (GetComponent<Rigidbody>().isKinematic)
        {
            transform.localScale = originalScale + (originalScale * (1 + pulseAmount * Mathf.Cos(10 * Time.time)));
        }
        else
        {
            transform.localScale = originalScale;
            Destroy(this.gameObject, 3.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
