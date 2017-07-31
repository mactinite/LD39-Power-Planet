using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizer : MonoBehaviour
{
    public float capacity = 100;
    public float charge = 100;
    public LineRenderer lineRenderer;
    private ScrollingUVs scrollUVs;
    public Transform muzzlePosition;
    Vector3 lineTarget;
    public Sizeable target;
    public PlayerStats stats;

    public AudioClip shrinkSound;
    public AudioClip growSound;
    private void Start()
    {
        scrollUVs = GetComponent<ScrollingUVs>();
        charge = stats.Mass;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
        {
            GetComponent<AudioSource>().Stop();
            lineRenderer.enabled = false;
        }
        // Growing
        if (Input.GetButton("Fire1") && charge > 0)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 15))
            {
                if (hit.transform.gameObject.CompareTag("Sizeable"))
                {
                    target = hit.transform.gameObject.GetComponent<Sizeable>();
                    lineTarget = hit.point;
                    scrollUVs.uvAnimationRate.x = -10;
                    lineRenderer.SetPosition(0, muzzlePosition.position);
                    lineRenderer.SetPosition(1, lineTarget);
                    if (target.grow() && stats.ModifyMass(-1))
                    {
                        if (lineRenderer.enabled == false)
                        {
                            GetComponent<AudioSource>().PlayOneShot(growSound);
                        }
                        charge = stats.Mass;
                        lineRenderer.enabled = true;
                    }
                    else
                    {
                        GetComponent<AudioSource>().Stop();
                        lineRenderer.enabled = false;
                    }
                }
                else
                {
                    GetComponent<AudioSource>().Stop();
                    lineRenderer.enabled = false;
                }
            }
        }


        // Shrinking
        if (Input.GetButton("Fire2") && charge < capacity)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 15))
            {
                if (hit.transform.gameObject.CompareTag("Sizeable"))
                {
                    target = hit.transform.gameObject.GetComponent<Sizeable>();
                    lineTarget = hit.point;
                    scrollUVs.uvAnimationRate.x = +10;
                    lineRenderer.SetPosition(0, muzzlePosition.position);
                    lineRenderer.SetPosition(1, lineTarget);
                    if (target.shrink() && stats.ModifyMass(1))
                    {
                        charge = stats.Mass;
                        if (lineRenderer.enabled == false)
                        {
                            GetComponent<AudioSource>().PlayOneShot(shrinkSound);
                        }
                        lineRenderer.enabled = true;

                    }
                    else
                    {
                        lineRenderer.enabled = false;
                        GetComponent<AudioSource>().Stop();
                    }
                }
                else
                {
                    lineRenderer.enabled = false;
                    GetComponent<AudioSource>().Stop();
                }
            }

        }
    }
    private void OnEnable()
    {
        lineRenderer.enabled = false;
        GetComponent<AudioSource>().Stop();
    }
}
