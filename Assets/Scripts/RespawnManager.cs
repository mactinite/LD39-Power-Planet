using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    private Checkpoint activeCheckpoint;
    public float resetBelowThisY = -100f;
	public bool fadeInOnReset = true;
	
    private Vector3 startingPosition;
    private PlayerStats stats;
	void Awake()
	{
		startingPosition = transform.position;
        stats = GetComponent<PlayerStats>();
	}
	
	void Update ()
	{
		if( transform.position.y < resetBelowThisY )
		{
            Respawn();
		}
	}

    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        if (activeCheckpoint)
        {
            activeCheckpoint.active = false;
        }
        activeCheckpoint = checkpoint;
        startingPosition = checkpoint.transform.parent.position + transform.up * 2;
    }

	public void Respawn()
	{	
		// reset the player
		transform.position = startingPosition;
        stats.SetHealth(100);
        if ( fadeInOnReset )
		{
			// see if we already have a "camera fade on start"
			CameraFadeOnStart fade = GameObject.Find("Main Camera").GetComponent<CameraFadeOnStart>();
			if( fade != null )
			{
				fade.Fade();
			}
			else
			{
				Debug.LogWarning("CheckIfBelowLevel couldn't find a CameraFadeOnStart on the main camera");
			}
		}
		
		// alternatively, you could just reload the current scene using this line:
		//Application.LoadLevel(Application.loadedLevel);
	}
}
