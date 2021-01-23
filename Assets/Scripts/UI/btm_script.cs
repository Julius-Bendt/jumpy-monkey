using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class btm_script : MonoBehaviour {

	public AudioClip clip;

	public void OnPressed()
	{
		GetComponent<AudioSource>().clip = clip;
		GetComponent<AudioSource>().Play();

	}
}
