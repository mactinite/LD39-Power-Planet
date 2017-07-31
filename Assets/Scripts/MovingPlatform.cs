using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {


    public Transform[] path;
    public bool active = false;
    public float moveTime = 10;
    private int currentTarget;
    private int lastTarget;
    private bool backwards;
    private float timer = 0;
    private float waitTimer;
    public float waitTime = 1;
    // Use this for initialization
    void Start () {
        currentTarget = 0;
        lastTarget = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (Vector3.Distance(transform.position, path[currentTarget].position) > 0.25f)
            {
                waitTimer = 0;
                if (Time.timeScale != 0)
                {
                    transform.localPosition = Vector3.Lerp(path[lastTarget].localPosition, path[currentTarget].localPosition, timer / moveTime);
                    timer += Time.deltaTime;
                }
            }
            else
            {
                timer = 0;
                waitTimer += Time.deltaTime;
                if (backwards && waitTimer > waitTime)
                {
                    if (currentTarget - 1 > 0)
                    {
                        lastTarget = currentTarget;
                        currentTarget--;
                    }
                    else
                    {
                        backwards = false;
                        lastTarget = currentTarget;
                        currentTarget++;
                    }
                }
                else if(!backwards && waitTimer > waitTime)
                {
                    if (currentTarget + 1 < path.Length)
                    {
                        lastTarget = currentTarget;
                        currentTarget++;
                    }
                    else
                    {
                        backwards = true;
                        lastTarget = currentTarget;
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
