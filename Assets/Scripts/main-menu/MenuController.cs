using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
    public GameObject opt, main, cre;
	
    public void showMain()
    {
        opt.active = false;
        cre.active = false;
        main.active = true;
    }

    public void showopt()
    {
        opt.active = true;
        cre.active = false;
        main.active = false;
    }

    public void showcre()
    {
        opt.active = false;
        cre.active = true;
        main.active = false;
    }

}
