using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.LightningBolt;

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
    bool reloading = false;
    public int shotCharge = 0;
    int shotCapacity = 100;

    public PlayerStats stats;

    private Vector3 target;
    private LightningBoltScript lightning;
    private LineRenderer lineRenderer;

    public Image reticle;

    public AudioSource bulletAudio;
    public AudioSource muzzleAudio;
    float pitchMin = .8f;
    float pitchMax = 1f;


    // Use this for initialization
    void Start () {
        startPos = transform.localPosition;
        kickOffset = Vector3.zero;
        muzzleSpin = muzzlePosition.gameObject.GetComponent<Spin>();
        lineRenderer = GetComponent<LineRenderer>();
        lightning = GetComponent<LightningBoltScript>();
    }
	
	// Update is called once per frame
	void Update () {

        if(chargingShot && shotCharge < shotCapacity)
        {
            shotCharge += 5;
        }

        reloading = false;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 15))
        {
            if (hit.transform.gameObject.CompareTag("EnergyWell") || hit.transform.gameObject.CompareTag("Friendly"))
            {
                reticle.color = Color.green;
            }
            else if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                reticle.color = Color.red;
            }
            else
            {
                reticle.color = Color.white;
            }
        }
        else
        {
            reticle.color = Color.white;
        }
        
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
            else if(stats.ModifyEnergy(-1))
            {
                projectile = Instantiate(projectilePrefab, muzzlePosition.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().isKinematic = true;
                chargingShot = true;
            }
            
        }

        if (Input.GetButton("Fire2"))
        {

            if (muzzleSpin != null)
            {
                muzzleSpin.SpinMe(Vector3.forward * -muzzleRotation);
            }

            if (Physics.Raycast(ray, out hit, 50))
            {
                //suck up some energy
                if (hit.transform.gameObject.CompareTag("EnergyWell") && stats.ModifyEnergy(Time.deltaTime * 10))
                {
                    target = hit.transform.position;
                    reloading = true;
                    lightning.enabled = true;
                    lineRenderer.enabled = true;
                    lightning.StartPosition = muzzlePosition.position;
                    lightning.EndPosition = target;
                    if (!muzzleAudio.isPlaying)
                    {
                        muzzleAudio.volume = 1;
                        muzzleAudio.Play();
                    }
                }
                else
                {
                    lightning.enabled = false;
                    lineRenderer.enabled = false;
                }
            }
            else
            {
                lightning.enabled = false;
                lineRenderer.enabled = false;
            }
        }
        else
        {
            lightning.enabled = false;
            lineRenderer.enabled = false;
        }
        if (Input.GetButtonUp("Fire1") && chargingShot)
        {
            kickOffset.z = kickAmount;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            chargingShot = false;
            // Spawn projectile and apply forces
            if (Physics.Raycast(ray, out hit, 1000))
            {
                projectile.GetComponent<Rigidbody>().AddForce((hit.point - muzzlePosition.position).normalized * projectileSpeed);
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * projectileSpeed);
            }

            // Play audio
            bulletAudio.pitch = pitchMax - ((pitchMax - pitchMin) * (shotCharge / 100));
            bulletAudio.Play();

            shotCharge = 0;
            
        }

        if (!reloading)
        {
            muzzleAudio.volume -= .1f;
            if(muzzleAudio.volume == 0)
            {
                muzzleAudio.Stop();
            }
        }

        kickOffset = Vector3.Lerp(kickOffset, Vector3.zero, Time.deltaTime * kickSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + kickOffset, Time.deltaTime * kickSpeed);

    }

}
