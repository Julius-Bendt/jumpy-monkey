using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class background_music_ui : MonoBehaviour {

	Color c;
	private static bool started,showName;
	bool fade;

	float showTime = 10f;
	static float i;

	public static string name;
	void Start()
	{
		c = GetComponent<Text>().color;
		c.a = 0;

	}

	public void Update()
	{
		if(started)
		{
			GetComponent<Text>().text = "Now playing: " + name;


			if(i >= showTime && !fade)
			{
				fade = true;
				i = 0;
			}
			else
			{
				i += Time.deltaTime;
			}

			if(fade)
			{
				GetComponent<Text>().color = Color.Lerp(GetComponent<Text>().color,c,2.5f * Time.deltaTime);
			}

			if(GetComponent<Text>().color.a == 0)
			{
				fade = false;
				started = false;
			}
		}

		if(showName)
		{
			GetComponent<Text>().color = new Color(GetComponent<Text>().color.r,GetComponent<Text>().color.g,GetComponent<Text>().color.b,255);
			showName = false;
			i = 0;
		}

	}

	public static void show(string theName)
	{
		name = theName;
		started = true;
		showName = true;
		i = 0;


	}
}
