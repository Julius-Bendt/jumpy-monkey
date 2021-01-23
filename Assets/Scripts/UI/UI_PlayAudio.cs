/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UI_PlayAudio : MonoBehaviour
{

    public AudioClip clip;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        audio.clip = clip;
        audio.Play();
    }

}
