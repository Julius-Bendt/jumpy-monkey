using UnityEngine;
using UnityEngine.UI;


public class Options_button_controller : MonoBehaviour {

    public t type;
    
    public enum t
    {
        bs_v,
        bs_p,
        se_v
    };

   public void changed()
    {
        float value = this.GetComponent<Slider>().value;
    
        switch(type)
        {
            case t.bs_p:
                background_music.bs_pitch = value;
                break;
            case t.bs_v:
                background_music.bs_volume = value;
                break;
            case t.se_v:
                background_music.se_volume = value;
                break;
        }
    }
}
