using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour {
    public Transform endUI;
    

    private void OnTriggerEnter(Collider other)
    {
        //End Game
        if (other.CompareTag("Player")) {
            Camera.main.GetComponent<SimpleSmoothMouseLook>().canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            endUI.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
