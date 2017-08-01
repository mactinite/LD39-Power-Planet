using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour {
    public Transform endUI;
    

    private void OnTriggerEnter(Collider other)
    {
        //End Game
        Camera.main.GetComponent<SimpleSmoothMouseLook>().canMove = false;
        endUI.gameObject.SetActive(true);
    }

}
