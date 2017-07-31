using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	[Range(0,100)]
	public float health = 30;

	// Use this for initialization
	void Start () {
		
	}
		
	public bool ModifyHealth(float amount)
	{
		if(health + amount <= 100 && health + amount >= 0)
		{
			health += amount;
			return true;
		}
		else
		{
			return false;
		}
	}
}
