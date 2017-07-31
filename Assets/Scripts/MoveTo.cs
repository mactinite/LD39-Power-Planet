using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveTo : MonoBehaviour {

	private Transform goal;

	private NavMeshAgent enemy;

    private Rigidbody rb;

    private Vector3 currentDestination;

    public NavMeshPathStatus pathStatus;

    public Transform deathFX;

    public bool damagingPlayer = false;
    public float hitRate = 0.5f;

    private Transform player;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
		currentDestination = enemy.transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        detectPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		detectPlayer ();
        damagePlayer();
	}
    private float timer = 0;

    private void damagePlayer()
    {
        
        if(damagingPlayer)
        {
            timer += Time.deltaTime;
            if (timer >= hitRate)
            {
                player.GetComponent<PlayerStats>().ModifyHealth(-10);
                timer = 0;
            }
        }

        
    }

	private void detectPlayer() {
		goal = player;
		NavMeshPath path = new NavMeshPath ();
		Vector3 position = goal.position;
		enemy.CalculatePath (position, path);
		pathStatus = path.status;
		if (path.status != NavMeshPathStatus.PathComplete) {
			goal = null;
			if (enemy.transform.position.x == currentDestination.x && enemy.transform.position.z == currentDestination.z) {
				currentDestination = randomNavSphere (enemy.transform.position, 10.0f, -1);
				enemy.destination = currentDestination;
				return;
			}
		} else {
            if(Vector3.Distance(enemy.transform.position, player.position) <= 1.75f)
            {
                enemy.isStopped = true;
                damagingPlayer = true;
            }
            else
            {
                enemy.isStopped = false;
                damagingPlayer = false;
            }
		}
		currentDestination = enemy.transform.position;
		enemy.destination = position;
	}

	private Vector3 randomNavSphere (Vector3 origin, float distance, int layermask) {
		Vector3 randomDirection = Random.insideUnitSphere * distance;
		randomDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);
		return navHit.position;
	}


    void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player Projectile") {
			this.GetComponent<EnemyStats> ().ModifyHealth (-10.0f);
			if (this.GetComponent<EnemyStats> ().health == 0) {
                Instantiate(deathFX, transform.position, Quaternion.identity);
                Destroy (this.gameObject);
			}

		}
	}
		
}
