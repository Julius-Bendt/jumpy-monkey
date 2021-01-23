/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_buttonShaker : MonoBehaviour {

    public float speed;

    public int dir;
	
	void Update () {

        float angle = GetComponent<RectTransform>().rotation.z;

        if(dir == 1)
        {
            if(angle < 8.5)
            {
                GetComponent<RectTransform>().rotation = Quaternion.Lerp(new Quaternion(GetComponent<RectTransform>().rotation.x, GetComponent<RectTransform>().rotation.y, -8.5f, GetComponent<RectTransform>().rotation.w), GetComponent<RectTransform>().rotation, speed * Time.deltaTime);
            }
            else
            {
                dir = -1;
            }
        }

        if(dir == -1 && angle > -8.5)
        {
            if (angle > -8.5)
            {
                GetComponent<RectTransform>().rotation = Quaternion.Lerp(new Quaternion(GetComponent<RectTransform>().rotation.x, GetComponent<RectTransform>().rotation.y, 8.5f, GetComponent<RectTransform>().rotation.w), GetComponent<RectTransform>().rotation, speed * Time.deltaTime);
            }
            else
            {
                dir = 1;
            }
        }
	}
}
