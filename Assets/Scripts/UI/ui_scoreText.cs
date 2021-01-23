using UnityEngine;
using UnityEngine.UI;

public class ui_scoreText : MonoBehaviour {

    public static bool exits;

    public static float x;

    void Start()
    {
        exits = true;
    }

	// Update is called once per frame
	void Update ()
    {
        GetComponent<Text>().text = "Score: " + x + "/" + Mathf.Round(ss_genv2.currentX);
	}

    void OnDestroy()
    {
        exits = false;
    }
}
