using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;
public class Grapple : MonoBehaviour {

    private Transform player;
    public LayerMask canGrapple;
    public float grappleSpeed = 20;
    public LightningBoltScript lightning;
    public LineRenderer lineRenderer;
    bool isFlying = false;
    Vector3 grapplePoint;
    HookShotTarget currentTarget;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && !isFlying)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.transform.gameObject.CompareTag("Hookshot"))
                {
                    currentTarget = hit.transform.GetComponent<HookShotTarget>();
                    currentTarget.Active(true);
                    isFlying = true;
                    grapplePoint = hit.point;
                    lightning.enabled = true;
                    lineRenderer.enabled = true;
                    lightning.EndPosition = grapplePoint;
                    player.GetComponent<FirstPersonDrifter>().canMove = false;
                }
            }
        }

        if (isFlying)
        {
            Flying();
        }

        if (Input.GetButtonDown("Jump") && isFlying)
        {
            CancelHook();
        }

        if (Input.GetButtonUp("Fire1"))
        {

        }

    }

    void CancelHook()
    {
        isFlying = false;
        lightning.enabled = false;
        lineRenderer.enabled = false;
        currentTarget.Active(false);
        player.GetComponent<FirstPersonDrifter>().canMove = true;
    }


    void OnDisable()
    {
        CancelHook();
    }


    public void Flying()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, grapplePoint, grappleSpeed * Time.deltaTime / Vector3.Distance(player.transform.position, grapplePoint));
        lightning.StartPosition =  transform.position;

        if (Vector3.Distance(player.transform.position, grapplePoint) < 0.5f)
        {
            CancelHook();

        }
    }

}
