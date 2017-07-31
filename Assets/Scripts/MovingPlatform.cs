using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {


    public Transform[] path;
    public bool active = false;
    public float moveTime = 10;
    private int currentTarget;
    private bool backwards;
    private float timer = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (Vector3.Distance(transform.position, path[currentTarget].position) > 0.25f)
            {
                transform.localPosition =  Vector3.Lerp(transform.localPosition, path[currentTarget].localPosition , timer/moveTime );
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                if (backwards)
                {
                    if (currentTarget - 1 > 0)
                    {
                        currentTarget--;
                    }
                    else
                    {
                        backwards = false;
                        currentTarget++;
                    }
                }
                else
                {
                    if (currentTarget + 1 < path.Length)
                    {
                        currentTarget++;
                    }
                    else
                    {
                        backwards = true;
                        currentTarget--;
                    }
                }
            }
        }
	}

    public void Activate(bool state)
    {
        active = state;
    }
}
