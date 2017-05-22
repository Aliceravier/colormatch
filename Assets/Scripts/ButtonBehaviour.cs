﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButtonBehaviour : ExtendedBehaviour {
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
		if (c.transform.gameObject.CompareTag("Player1") || c.transform.gameObject.CompareTag("Player2"))
        {
            GameObject player = c.transform.gameObject;
            anim.SetTrigger ("ButtonPressed"); 
			Wait (0.5f, () => {
				           
				setRoom (player);
			});

            GameObject god = GameObject.FindGameObjectWithTag("God");
            whoWon ww = god.GetComponent<whoWon>();
            int roomValue = this.GetComponentInParent<RoomManager>().roomValue;
            ww.updateInfo(player, roomValue);            
        }
    }
    
	void setRoom(GameObject player){
		RoomManager rm = this.transform.parent.GetComponent<RoomManager> ();
		rm.setRoomTeam (player.GetComponent<Movement> ().playerTeam);
		rm.ChangeTiles(player.GetComponent<SpriteRenderer>().color, "Tile");

	}


}
