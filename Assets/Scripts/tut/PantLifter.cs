/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System.Collections;

public class PantLifter : MonoBehaviour {

    public Vector2 force;

    void OnTriggerStay2D(Collider2D o)
    {
        if (o.tag == "pants")
        {
            o.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
}
