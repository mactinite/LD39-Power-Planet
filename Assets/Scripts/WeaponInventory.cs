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
		if(Input.GetAxis("Mouse ScrollWheel") > Mathf.Epsilon)
        {
            switchWeapons(currentlySelected + 1);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < -Mathf.Epsilon)
        {
            switchWeapons(currentlySelected - 1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            switchWeapons(0);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2) )
        {
            switchWeapons(1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            switchWeapons(2);
        }

        

    }

    public void switchWeapons(int weapon)
    {
        if (weapon != currentlySelected)
        {
            StartCoroutine(PutDown(weapons[currentlySelected]));
            currentlySelected = weapon;
            if (currentlySelected >= weapons.Length)
            {
                currentlySelected = 0;
            }
            else if (currentlySelected < 0)
            {
                currentlySelected = weapons.Length - 1;
            }
            weapons[currentlySelected].gameObject.SetActive(true);
            weapons[currentlySelected].GetComponent<WeaponSway>().startPos.y = 0;
            weapons[currentlySelected].localPosition = Vector3.down * 3;
        }
    }

    IEnumerator PutDown(Transform toMove)
    {
        toMove.GetComponent<WeaponSway>().startPos.y = -3;
        if (Vector3.Distance(toMove.localPosition, toMove.GetComponent<WeaponSway>().startPos) > 0.25f)
            yield return null;
        toMove.gameObject.SetActive(false);
        
    }

}
