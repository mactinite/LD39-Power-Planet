using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour {

    public int currentlySelected = 0;
    public Transform[] weapons;
    public bool[] unlocked;
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

        if (weapon >= weapons.Length)
        {
            weapon = 0;
        }
        else if (weapon < 0)
        {
            weapon = weapons.Length - 1;
        }

        if (unlocked[weapon])
        {
            if (weapon != currentlySelected)
            {
                StartCoroutine(PutDown(weapons[currentlySelected]));
                currentlySelected = weapon;
                weapons[currentlySelected].gameObject.SetActive(true);
                weapons[currentlySelected].GetComponent<WeaponSway>().startPos.y = 0;
                weapons[currentlySelected].localPosition = Vector3.down * 3;
            }
        }
        else
        {
            //Loop to next unlocked weapon
        }
    }


    public bool UnlockWeapon(int weapon)
    {
        if (weapon >= weapons.Length)
        {
            return false;
        }
        else if (weapon < 0)
        {
            return false;
        }
        if (unlocked[currentlySelected])
        {
            StartCoroutine(PutDown(weapons[currentlySelected]));
        }
        unlocked[weapon] = true;
        currentlySelected = weapon;
        weapons[currentlySelected].gameObject.SetActive(true);
        weapons[currentlySelected].GetComponent<WeaponSway>().startPos.y = 0;
        weapons[currentlySelected].localPosition = Vector3.down * 3;
        return true;

    }

    IEnumerator PutDown(Transform toMove)
    {
        toMove.GetComponent<WeaponSway>().startPos.y = -3;
        if (Vector3.Distance(toMove.localPosition, toMove.GetComponent<WeaponSway>().startPos) > 0.25f)
            yield return null;
        toMove.gameObject.SetActive(false);
        
    }

}
