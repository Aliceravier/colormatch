﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour {

	[SerializeField]
	float damage = 300;
	[SerializeField]
	bool isStay = true;
	[SerializeField]
	float pushback = 20;
	[SerializeField]
	float startHurt = 500;
	[SerializeField]
	float endHurt = 0.1f;
	Health theirHealth;
	Team myTeam;



	// Use this for initialization
	void Start () {
		myTeam = GetComponent<Health> ().getTeam ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D c){
		if (!isStay) {
			theirHealth = c.gameObject.GetComponent<Health> ();
			if (theirHealth != null && theirHealth.getTeam () != myTeam) {
				theirHealth.rigidBodyHurt (damage, pushback, transform);
				theirHealth.hitFlash (startHurt, endHurt);
			}
		}
	}

	void OnCollisionStay2D (Collision2D c){
		if (isStay) {
			theirHealth = c.gameObject.GetComponent<Health> ();
			if (theirHealth != null && theirHealth.getTeam () != myTeam) {
				theirHealth.rigidBodyHurt (damage, pushback, transform);
				theirHealth.hitFlash (startHurt, endHurt);
			}
		}
	}


}
