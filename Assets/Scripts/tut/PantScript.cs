/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2016
 **/


using UnityEngine;
using System.Collections;

public class PantScript : MonoBehaviour {


    public Transform player;

    Dialog d;

	void Start () {
        d = GetComponent<Dialog>();
	}
	
	void Update ()
    {
	    
        if(Vector2.Distance(transform.position,player.position) < 3)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                d.StartDialouge("Pants");
                player.GetComponent<Movement>().canwalk = false;
            }
        }

        if(d.finished)
        {
            player.GetComponent<Movement>().canwalk = true;
            Destroy(gameObject);
        }
    }
}
