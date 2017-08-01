using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {


    public void ResumeGame()
    {
        Time.timeScale = 1;
        Camera.main.GetComponent<SimpleSmoothMouseLook>().canMove = true;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
