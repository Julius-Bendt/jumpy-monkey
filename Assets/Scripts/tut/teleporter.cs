using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class teleporter : MonoBehaviour {

    public teleportTypes teleport_to;
    
	public AudioClip clip;

	bool teleport = false;
	
    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.tag == "Player")
        {

			if(!teleport)
			{
				GetComponent<AudioSource>().clip = clip;
				GetComponent<AudioSource>().Play();
				background_music_ui.show(background_music_ui.name);
				teleport = true;
			}


        }
    }

	void Update()
	{
		if(teleport)
		{


			if(!GetComponent<AudioSource>().isPlaying)
			{
				if(teleport_to == teleportTypes.heaven)
				{
					Application.LoadLevel("stair_of_dead");
				}
				else
				{
					Application.LoadLevel("sidescroller");
				}
			}
		}

	}

    public enum teleportTypes
    {
        hell,
        heaven
    };
}
