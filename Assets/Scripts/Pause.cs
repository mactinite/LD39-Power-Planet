using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pause
{

    public class Pause : MonoBehaviour
    {

        [SerializeField] private GameObject pauseMenu;
        private FirstPersonDrifter playerController;
        bool paused = false;
        private SimpleSmoothMouseLook mouseLook;

        void Start()
        {
            pauseMenu.SetActive(false);
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonDrifter>();
            mouseLook = Camera.main.GetComponent<SimpleSmoothMouseLook>();
            
        }

        void Update()
        {
            if (Input.GetButtonUp("Pause"))
            {
                if (!pauseMenu.activeInHierarchy)
                {
                    pauseGame();
                }
                else if (pauseMenu.activeInHierarchy)
                {
                    continueGame();
                }
            }
        }
        public void pauseGame()
        {
            Time.timeScale = 0;
            mouseLook.canMove = false;
            playerController.canMove = false;
            paused = true;
            pauseMenu.SetActive(true);
        }
        public void continueGame()
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseLook.canMove = true;
            Time.timeScale = 1;
            playerController.canMove = true;
            paused = false;
            pauseMenu.SetActive(false);
        }

        public void QuitGame()
        {
            Time.timeScale = 1;
            Application.Quit(); // TODO: Instead navigate to main menu scene
        }

        public bool getPaused ()
        {
            return paused;
        }

        public void SetSensitivity(float amount)
        {
            mouseLook.sensitivity = new Vector2(amount, amount);
        }
    }
}
