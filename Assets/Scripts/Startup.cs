/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

    public static bool inited = false;

	void Start () {
	
        if(inited)
        {
            inited = true;
            //EasySave.Manager.LoadFile();
        }

	}
	
	void OnApplicationQuit()
    {
       // EasySave.Manager.SaveFile();
    }
}
