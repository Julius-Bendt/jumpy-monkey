using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ss_ai : MonoBehaviour {

    public currentState state;
    public Transform player;
    int dir;

    public float sizeX, sizeY;

    public BoxCollider2D collider;

	void FixedUpdate () 
    {
	    switch(state)
        {
            case currentState.chashing:
                chashing();
            break;

            case currentState.idle:
                idle();
            break;

            case currentState.patroling:
                idle();
            break;

            default:
                idle();
            break;
                
        }
	}


  
    bool isSomethingInFront()
    {
        return true;
    }

    void chashing()
    {

    }
    
    void idle()
    {

    }

    public enum currentState
    {
        chashing,
        patroling,
        idle
    };
}
