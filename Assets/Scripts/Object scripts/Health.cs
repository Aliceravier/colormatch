﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class Health : ExtendedBehaviour {

	[SerializeField]
	Team objectTeam = Team.neutral;
	[SerializeField]
	float maxHealth  = 300;
	float health;
	bool isDead;

	// Use this for initialization
	void Start () {
		setUp ();
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            isDead = true;
        }
	}

    public float getHealth()
    {
        return health;
    }

	public float getMaxHealth(){
		return maxHealth;
	}

	public void hurt(float damage){
		if (!isDead)
			health -= damage;
	}


	public void rigidBodyHurt(float damage, float force, Transform other){
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		if (rb !=null){
			rb.AddForce ((transform.position - other.position).normalized * force);
		}
		hurt (damage);

	}

	public void hitFlash(float speed, float time){
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		if (sr != null) {
			Color original = sr.color;
			print (sr.color);
			sr.color = new Color (Mathf.Lerp (original.r, 1f, speed), Mathf.Lerp (original.g, 0f, speed), Mathf.Lerp (original.b, 0f, speed));
			print (sr.color);
			Wait (time, () => {
				sr.color = original;
			});
		}

	}
	public bool getDeath(){
		return isDead;
	}

	public void setDeath(bool deathState){
		isDead = deathState;
	}

	public void setUp(){
		health = maxHealth;
		isDead = false;
	}

	public Team getTeam(){
		return objectTeam;
	}

	public void setTeam(Team t){
		objectTeam = t;

	}

	public void colourByTeam(){
		/*finds a player of team t, gets their color, sets thing to be that color. Else, white*/
		GameObject player = getPlayerOfTeam (objectTeam);
		if (player != null)
			GetComponent<SpriteRenderer> ().color = player.GetComponent<SpriteRenderer> ().color;
		else
			GetComponent<SpriteRenderer> ().color = Color.white;
	}


}