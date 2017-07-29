using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour {

    public int currentlySelected = 0;
    public Transform[] weapons;
    public bool switching = false;
    public bool swapped = true;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Mouse ScrollWheel") > Mathf.Epsilon && !switching)
        {
            StartCoroutine(PutDown(weapons[currentlySelected]));
            currentlySelected++;
            if(currentlySelected >= weapons.Length)
            {
                currentlySelected = 0;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < -Mathf.Epsilon && !switching)
        {
            StartCoroutine(PutDown(weapons[currentlySelected]));
            currentlySelected--;
            if (currentlySelected < 0)
            {
                currentlySelected = weapons.Length -1 ;
            }
        }

        if (!switching && !swapped)
        {
            StartCoroutine(PullOut(weapons[currentlySelected]));
        }
    }

    IEnumerator PutDown(Transform toMove)
    {
        switching = true;
        swapped = false;
        Vector3 fromPosition = toMove.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / 0.1f)
        {
            toMove.localPosition = Vector3.Lerp(fromPosition, fromPosition - toMove.up, t);
            yield return null;
        }
        switching = false;
        toMove.gameObject.SetActive(false);
    }


    IEnumerator PullOut(Transform toMove)
    {
        toMove.gameObject.SetActive(true);
        Vector3 toPosition = Vector3.zero;
        Vector3 fromPosition = toMove.localPosition - toMove.up;
        for (var t = 0f; t < 1; t += Time.deltaTime / 0.1f)
        {
            toMove.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }
        toMove.localPosition = toPosition;
        swapped = true;
        
    }
}
