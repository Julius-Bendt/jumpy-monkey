using UnityEngine;
using System.Collections.Generic;

public class ThemeChoser : MonoBehaviour {

    public List<holderList> Themes = new List<holderList>();

    //[HideInInspector]
    public int id;
}


[System.Serializable]
public class holderList
{
    public string name = "Unnamed theme";
    public GameObject[] grounds, traps, end_doors;
    public Color[] cameraColors;
}
