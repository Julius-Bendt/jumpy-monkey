using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class background_music : MonoBehaviour {

    public AudioClip[] music;

    private AudioSource aus;


	public static int[] last = new int[4];

    int index;

    public static float bs_volume = 1, bs_pitch = 1, se_volume = 1;
    public static bool changed;

    void Start()
    {
		DontDestroyOnLoad(gameObject);
        aus = GetComponent<AudioSource>();
        playRandom();
    }


    void Update()
    {

        if(changed)
        {
            changed = false;
            aus.volume = bs_volume;
            aus.pitch = bs_pitch;
        }

        if(!aus.isPlaying)
        {
            playRandom();
        }
       
        
    }

    public void playRandom()
    {
        int r = Random.Range(0, music.Length - 1);
        aus.clip = music[r];
        aus.Play();
        index = r;

        background_music_ui.show(music[index].name);
    }

    public void playNext()
    {
        index++;

        if(index == music.Length)
        {
            index = 0;
        }
        aus.clip = music[index];
        background_music_ui.show(music[index].name);
        aus.Play();
    }

    public void playLast()
    {
        index--;

        if (index == -1)
        {
            index = music.Length-1;
        }

        aus.clip = music[index];
        background_music_ui.show(music[index].name);
        aus.Play();
    }
}
