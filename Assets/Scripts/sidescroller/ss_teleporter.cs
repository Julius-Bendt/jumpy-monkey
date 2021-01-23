using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ss_teleporter : MonoBehaviour {

    public tp teleport_to;

	bool teleport = false;

	public AudioClip clip;
	
    public enum tp
    {
        sidescroller,
        stair_of_dead,
		upscroller
    };

    void OnTriggerEnter2D(Collider2D o)
    {
        if(!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
            background_music_ui.show(background_music_ui.name);

            teleport = true;
        }
    }

	void Update()
	{
		if(teleport)
		{
			if(!GetComponent<AudioSource>().isPlaying)
			{
				switch(teleport_to)
				{
				case tp.stair_of_dead:
					Application.LoadLevel("stair_of_dead");
					break;
				case tp.sidescroller:
                /*
                        if (!System.Convert.ToBoolean(EasySave.Manager.Load("randomSeed")))
                        {
                            if(EasySave.Manager.KeyExist("TimesComplete"))
                            {
                                int t = int.Parse(EasySave.Manager.Load("TimesComplete"));

                                EasySave.Manager.Save("TimesComplete", (t + 1).ToString());
                            }
                            else
                            {
                                EasySave.Manager.Save("TimesComplete", "1");
                            }
                        }
                        */
                    Application.LoadLevel("sidescroller");
                        break;
				case tp.upscroller:
					Application.LoadLevel("Scene");
					break;
				}
			}
		}
	}
}
