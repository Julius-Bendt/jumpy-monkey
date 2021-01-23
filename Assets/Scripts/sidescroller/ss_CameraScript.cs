using UnityEngine;
using System.Collections;

public class ss_CameraScript : MonoBehaviour
{
   public Color[] b;
   public float time;

   public float xOffset = 2.5f,yOffset = 1f;

   public GameObject player,playerPref;
    public Vector3 spawnloc;

    private Color theColor;
    public static float startx;

    void Start()
    {
        theColor = GetComponent<Camera>().backgroundColor;
    }

    public void NewLevel()
    {
        ThemeChoser tc = GetComponent<ThemeChoser>();

        b = tc.Themes[tc.id].cameraColors;

        theColor = b[Random.Range(0, b.Length - 1)];

    }
    void Update()
    {

        if(prog_bar.exits)
        {
            prog_bar.x = transform.position.x-startx;
        }

        if(ui_scoreText.exits)
        {
            ui_scoreText.x = Mathf.RoundToInt(transform.position.x - startx);
        }


        if (player == null)
        {
            return;
        }

        float fixedX = player.GetComponent<Movement>().ld * xOffset;
        float fixedY = player.transform.position.y - 0.5f;
        Vector3 pos = new Vector3(player.transform.position.x + fixedX, fixedY + yOffset, -10f);

        float i_dir = 1;


        if (Input.GetAxisRaw("Vertical") == -1)
        {
            i_dir = -3 + yOffset;
            pos = new Vector3(player.transform.position.x + fixedX, fixedY + i_dir, -10f);
        }






        transform.position = Vector3.Lerp(transform.position, pos, 6 * Time.deltaTime);

        GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, theColor, time * Time.deltaTime);


        if (b.Length > 1)
        {
            if (GetComponent<Camera>().backgroundColor == theColor)
            {
                theColor = b[Random.Range(0, b.Length - 1)];
                Debug.Log("New color!");
            }
        }

    }



}
