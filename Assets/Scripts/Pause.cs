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

        void Start()
        {
            pauseMenu.SetActive(false);
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonDrifter>();
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
            playerController.canMove = false;
            paused = true;
            pauseMenu.SetActive(true);
        }
        public void continueGame()
        {
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
    }
}
