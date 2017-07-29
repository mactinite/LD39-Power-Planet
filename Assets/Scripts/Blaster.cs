using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

    public Transform muzzlePosition;
    public Transform projectilePrefab;
    public float projectileSpeed;
    public float projectileGravity;

    private Vector3 startPos;
    private Vector3 kickOffset;

    public float kickAmount = 1;
    public float kickSpeed = 5;
    // Use this for initialization
    void Start () {
        startPos = transform.localPosition;
        kickOffset = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        startPos = transform.localPosition;
        kickOffset = Vector3.zero;
        if (Input.GetButtonDown("Fire1"))
        {
            kickOffset.z = kickAmount;
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


            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + kickOffset, Time.deltaTime * kickSpeed);
        }



	}

}
