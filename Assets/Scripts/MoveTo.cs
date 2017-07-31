using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveTo : MonoBehaviour {

	public Transform goal;

	public NavMeshAgent enemy;

	public Rigidbody rb;

	public Vector3 currentDestination;

	public NavMeshPathStatus pathStatus;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		rb.detectCollisions = false;
		currentDestination = enemy.transform.position;
		detectPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		detectPlayer ();
	}

	private void detectPlayer() {
		goal = GameObject.FindGameObjectWithTag ("Player").transform;
		NavMeshPath path = new NavMeshPath ();
		Vector3 position = goal.position;
		enemy.CalculatePath (position, path);
		pathStatus = path.status;
		if (path.status == NavMeshPathStatus.PathInvalid) {
			goal = null;
			if (enemy.transform.position.x == currentDestination.x && enemy.transform.position.z == currentDestination.z) {
				rb.isKinematic = true;
				rb.detectCollisions = false;
				currentDestination = randomNavSphere (enemy.transform.position, 5.0f, -1);
				enemy.destination = currentDestination;
				return;
			}
		} else {
			rb.isKinematic = false;
			rb.detectCollisions = true;
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
		if (col.gameObject.tag == "Player") {
			GameObject player = col.gameObject;
			player.GetComponent<PlayerStats> ().ModifyHealth (-10.0f);
		}

		if (col.gameObject.tag == "Player Projectile") {
			this.GetComponent<EnemyStats> ().ModifyHealth (-10.0f);
			if (this.GetComponent<EnemyStats> ().health == 0) {
				Destroy (this.gameObject);
			}

		}
	}
		
}
