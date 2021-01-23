using UnityEngine;
using System.Collections;

public class ss_Gamecontroller : MonoBehaviour {

    public float respawnDelay;

    public GameObject player;
    public Vector3 respawnPos;
	public static bool bybassWaitTime;
    float i;

    void Update()
    {
        if(Movement.isdead)
        {
            i += Time.deltaTime;

            if(i >= respawnDelay || bybassWaitTime)
            {
                GameObject[] og = GameObject.FindGameObjectsWithTag("Player");

                for(int j = 0; j < og.Length; j++)
                {
                    Destroy(og[j]);
                }

				bybassWaitTime = false;
				GameObject g = Instantiate(player,respawnPos,Quaternion.identity) as GameObject;
                g.GetComponent<Movement>().enabled = true;
                GetComponent<ss_CameraScript>().player = g;
				GetComponent<ss_genv2>().GenNewLevel();

                Movement.isdead = false;
                i = 0;

            }
        }
    }

}
