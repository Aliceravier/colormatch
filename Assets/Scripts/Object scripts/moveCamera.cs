﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : ExtendedBehaviour {

    [SerializeField]
	Team playerTeam = Team.neutral;
    private GameObject player;
    GameObject[] Rooms;
    public bool isLeaving = false;

    [HideInInspector]
    public GameObject newRoom;

    RoomManager rm;
    Camera cam;


	// Use this for initialization
	void Awake () {
        
        Rooms = GameObject.FindGameObjectsWithTag("Room");
        player = getPlayerOfTeam (playerTeam);
        cam = GetComponent<Camera> ();
		setAspectRatio (playerTeam);
    }

	// Update is called once per frame
	void FixedUpdate () {

	}

	public bool isInScope(GameObject obj){ //not using now
		Vector3 screenPoint = cam.WorldToViewportPoint (obj.transform.position); //LOOKS FOR THING
		return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
	}

    public GameObject getPlayersRoom()
    {        
		/*Returns room which a player is in (REFACTOR) */
        foreach (GameObject room in Rooms)
        {
            Vector2 roomDims = maxRoom(room);
            if (Mathf.Abs(player.transform.position.x - room.transform.position.x) < (roomDims.x / 2) &&
                Mathf.Abs(player.transform.position.y - room.transform.position.y) < (roomDims.y / 2))
            {
                return room;
            }
        }
		//no room found, just return null
        return null;
    }


    public bool cameraFocusIsOn(GameObject room)
    {
		/*returns true if camera is focused on the given room, false otherwise*/
        rm = room.GetComponent<RoomManager>();
        Vector2 minCoords = rm.getMinPoint();
        Vector2 maxCoords = rm.getMaxPoint();
        if (transform.position.x > minCoords.x && transform.position.y > minCoords.y
            && transform.position.x < maxCoords.x && transform.position.y < maxCoords.y)
            return true;
        else
            return false;
    }

	public void focusOnRoom(GameObject room){
        this.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, -10);
		Vector2 roomDims = maxRoom(room);
		//print (playerTeam.ToString () + " " + room);
		cam.orthographicSize = roomDims.y/2;
	}

	void setAspectRatio(Team team){
		//set aspect wanted
		float targetaspect = 1.0f / 1.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (((float)Screen.width / (float)Screen.height))/2;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = cam.rect;

			rect.width = 1.0f/2;
			rect.height = scaleheight;

			if (team == Team.blue)
				rect.x = 0;
			else
				rect.x = 0.5f;
			
			rect.y = (1.0f - scaleheight) / 2.0f;

			cam.rect = rect;
		}
		else // else add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = cam.rect;

			rect.width = scalewidth/2;
			rect.height = 1.0f;
			if (team == Team.blue)
				rect.x = (1.0f - scalewidth) / 2.0f;
			else
				rect.x = (1.0f - scalewidth) / 2.0f + 0.5f;
			rect.y = 0;

			cam.rect = rect;
		}

	}

    //get dimensions of room
	public Vector2 maxRoom(GameObject room) {
		return room.GetComponent<RoomManager>().roomSize;
	}
	
}



