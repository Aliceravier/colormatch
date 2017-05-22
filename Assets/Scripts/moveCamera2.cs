﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera2 : MonoBehaviour {

    
    private GameObject player;
	
	float offset = 20;

	Camera camera;


	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player2");
        camera = GetComponent<Camera> ();
        focusOnRoom();
    }

	// Update is called once per frame
	void FixedUpdate () {
        //focus on centre of that room
        if (!isInScope(player)){
            GameObject newRoom = getPlayersRoom();
            moveToNextRoom(newRoom);
        }
	}

	bool isInScope(GameObject obj){
		Vector3 screenPoint = camera.WorldToViewportPoint (obj.transform.position); //LOOKS FOR THING
		return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
	}

    public GameObject getPlayersRoom()
    {
        GameObject[] Rooms;
        Rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject room in Rooms)
        {
            Vector2 roomDims = maxRoom(room);
            if (Mathf.Abs(player.transform.position.x - room.transform.position.x) < (roomDims.x / 2) &&
                Mathf.Abs(player.transform.position.y - room.transform.position.y) < (roomDims.y / 2))
            {
                return room;
            }
        }
        return player;
    }

    void focusOnRoom(){
        GameObject room = GameObject.FindGameObjectsWithTag("Room")[0];
       
        Vector2 roomDims = maxRoom(room);
        this.transform.position = new Vector3 (room.transform.position.x, room.transform.position.y, -10);
        camera.orthographicSize = roomDims.y/2;

        //set aspect wanted
        float targetaspect = 1.0f / 1.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (((float)Screen.width / (float)Screen.height))/2;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f/2;
            rect.height = scaleheight;
            rect.x = 0.5f;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // else add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth/2;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f+0.5f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

	public void moveToNextRoom(GameObject room){
        this.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, -10);
	}

    //get dimensions of room
	public Vector2 maxRoom(GameObject Room) {
		return Room.GetComponent<RoomManager> ().getSize ();
	}
	

}



