using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;
using UnityEngine.UI;
public class Grapple : MonoBehaviour {

    private Transform player;
    public LayerMask canGrapple;
    public float grappleSpeed = 20;
    public LightningBoltScript lightning;
    public LineRenderer lineRenderer;
    bool isFlying = false;
    Vector3 grapplePoint;
    HookShotTarget currentTarget;
    public float grappleDistance = 15;
    public Image reticle;
    public float grabTime = 2;
    private float timer;

    private Vector3 startPos;
    private Vector3 kickOffset;

    public float kickAmount = 1;
    public float kickSpeed = 5;
    public float muzzleRotation = 360;
    private Spin muzzleSpin;
    public Transform muzzlePosition;

    // Use this for initialization
    void Start () {
        startPos = Vector3.zero;
        muzzleSpin = muzzlePosition.gameObject.GetComponent<Spin>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, grappleDistance))
        {

            if (hit.transform.gameObject.CompareTag("Hookshot"))
            {
                reticle.color = Color.green;
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

        if (Input.GetButtonDown("Fire1") && !isFlying)
        {
            if (Physics.Raycast(ray, out hit, grappleDistance))
            {
                if (hit.transform.gameObject.CompareTag("Hookshot"))
                {
                    kickOffset.z = kickAmount;
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

        if (Input.GetButtonDown("Fire1") && isFlying)
        {
            if (Physics.Raycast(ray, out hit, grappleDistance))
            {
                if (hit.transform.gameObject.CompareTag("Hookshot"))
                {
                    kickOffset.z = kickAmount;
                    CancelHook();
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
        kickOffset = Vector3.Lerp(kickOffset, Vector3.zero, Time.deltaTime * kickSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + kickOffset, Time.deltaTime * kickSpeed);

        if (Input.GetButtonDown("Jump") && isFlying)
        {
            CancelHook();
        }

        if (Input.GetButtonUp("Fire1"))
        {

        }

    }


    private void FixedUpdate()
    {
        if (isFlying)
        {
            player.GetComponent<FirstPersonDrifter>().isHooked = true;
            if (muzzleSpin != null)
            {
                muzzleSpin.SpinMe(Vector3.forward * muzzleRotation);
            }
            Flying();
        }
        else
        {
            player.GetComponent<FirstPersonDrifter>().isHooked = false;
        }
    }

    void CancelHook()
    {
        timer = 0;
        isFlying = false;
        lightning.enabled = false;
        lineRenderer.enabled = false;
        if(currentTarget)
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
        lightning.StartPosition =  muzzlePosition.position;

        if (Vector3.Distance(player.transform.position, grapplePoint) < 0.5f)
        {
            if (timer < grabTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                CancelHook();
                
            }
        }
    }


}
