﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whoWon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void playerWinState(GameObject player)
    {
        List<int> values = player.GetComponent<Movement>().values;
        foreach (int value1 in values)
        {
            foreach (int value2 in values)
            {
                if (value2 == value1)
                    continue;
                foreach (int value3 in values)
                {
                    if (value3 == value2 || value3 == value1)
                        continue;
                    if (value1 + value2 + value3 == 15)
                    {
                        print("Team " + player.GetComponent<Movement>().playerTeam.ToString() + " wins!");
                        return;
                    }
                }
            }
        }
    }

    public void updateInfo(GameObject player, int roomValue)
    {
        player.GetComponent<Movement>().values.Add(roomValue);
        GameObject otherPlayer = findOtherPlayer(player);
        otherPlayer.GetComponent<Movement>().values.Remove(roomValue);
        playerWinState(player);
    }

    GameObject findOtherPlayer(GameObject player)
    {
        if (player.transform.gameObject.CompareTag("Player1"))
            return GameObject.FindGameObjectWithTag("Player2");
        else
            return GameObject.FindGameObjectWithTag("Player1");
    }
}