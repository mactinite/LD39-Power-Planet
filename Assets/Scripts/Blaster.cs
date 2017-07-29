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
    public float muzzleRotation = 360;
    private Spin muzzleSpin;
    Transform projectile;
    bool chargingShot = false;
    // Use this for initialization
    void Start () {
        startPos = transform.localPosition;
        kickOffset = Vector3.zero;
        muzzleSpin = muzzlePosition.gameObject.GetComponent<Spin>();
    }
	
	// Update is called once per frame
	void Update () {
        startPos = transform.localPosition;
        kickOffset = Vector3.zero;
        
        if (Input.GetButton("Fire1"))
        {
            
            if (muzzleSpin != null)
            {
                muzzleSpin.SpinMe(Vector3.forward * muzzleRotation);
            }
            if (chargingShot)
            {
                projectile.position = muzzlePosition.position;
            }
            else
            {
                projectile = Instantiate(projectilePrefab, muzzlePosition.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().isKinematic = true;
            }
            chargingShot = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            kickOffset.z = kickAmount;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            chargingShot = false;
            // Spawn projectile and apply forces

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                projectile.GetComponent<Rigidbody>().AddForce((hit.point - muzzlePosition.position).normalized * projectileSpeed);
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * projectileSpeed);
            }


            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + kickOffset, Time.deltaTime * kickSpeed);
        } 




    }

}
