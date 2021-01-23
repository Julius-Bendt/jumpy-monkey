using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class prog_bar : MonoBehaviour {

    public static float x = 0, max;
    public static bool exits;

    private Image bar;

    void Start()
    {
        bar = GetComponent<Image>();
        exits = true;

        if (max <= 1)
        {
            max = ss_genv2.currentX;
        }
    }

	void LateUpdate ()
    {

        

        

        float current = x / max;
        bar.fillAmount = current;
	}

    void OnDestroy()
    {
        exits = false;
    }
}
