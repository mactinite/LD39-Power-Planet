using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

    public Transform muzzlePosition;
    public Transform projectilePrefab;
    public float projectileSpeed;
    public float projectileGravity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            // Spawn projectile and apply forces
            Transform projectile = Instantiate(projectilePrefab, muzzlePosition.position, Quaternion.identity);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                projectile.GetComponent<Rigidbody>().AddForce((hit.point - muzzlePosition.position).normalized * projectileSpeed);
                Debug.Log("Hit");
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * projectileSpeed);
                Debug.Log("No Hit");
            }
            
        }
	}
}
