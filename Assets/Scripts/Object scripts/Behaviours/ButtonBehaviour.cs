﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButtonBehaviour : ExtendedBehaviour {

    public GameObject locker;

    private Animator anim;
    private GameObject button;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();	
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D c)
    {
		//if player touches button
		if (c.transform.gameObject.CompareTag("Player") && anim.GetBool("ButtonOn") == false)
        {
            //change anim + change room colour
            GameObject player = c.transform.gameObject;
            anim.SetBool ("ButtonOn", true); //changes anim to pushed
			Wait (0.5f, () => { //changes room color				           
				setRoom (player);
			});

			updateInfo (player);
                    
        }
    }

    public void makeLock()
    {
        Instantiate(locker, transform);
    }
    
	void setRoom(GameObject player){//RECOTJJAJNFJOR COLLOOUR
		/* Takes a player, sets the room to be their team and colour */
		RoomManager rm = this.transform.parent.GetComponent<RoomManager> ();
		rm.setRoomTeam (player.GetComponent<Health> ().getTeam());
		rm.ChangeTiles(player.GetComponent<SpriteRenderer>().color, "Floor");

	}

	void updateInfo(GameObject player){
		/*Tells God what room was got by which player*/
		GameObject god = GameObject.FindGameObjectWithTag("God");
		whoWon ww = god.GetComponent<whoWon>();
		int roomValue = this.GetComponentInParent<RoomManager>().roomValue;
		ww.updateInfo(player, roomValue);  //updates the players' value tables and checks if someone won  

	}


}
