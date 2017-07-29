using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizer : MonoBehaviour
{
    public int capacity = 100;
    public int charge = 50;
    public LineRenderer lineRenderer;
    Vector3 lineTarget;
    public Sizeable target;

    // Update is called once per frame
    void Update()
    {

        // Growing
        if (Input.GetButtonUp("Fire1") && charge > 0)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.transform.gameObject.CompareTag("Sizeable"))
                {
                    target = hit.transform.gameObject.GetComponent<Sizeable>();
                    lineTarget = hit.point;
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(1, lineTarget);
                    if (target.grow())
                    {
                        charge--;
                    }
                }
            }
        }

        // Shrinking
        else if (Input.GetButtonUp("Fire2") && charge < capacity)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.transform.gameObject.CompareTag("Sizeable"))
                {
                    target = hit.transform.gameObject.GetComponent<Sizeable>();
                    lineTarget = hit.point;
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(1, lineTarget);
                    if (target.shrink())
                    {
                        charge++;
                    }
                }
            }
        }



    }
}
