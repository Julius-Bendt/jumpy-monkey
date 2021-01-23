using UnityEngine;
using System.Collections;

public class ss_trap : MonoBehaviour {

	public t type;
    

    void OnTriggerEnter2D(Collider2D o)
    {

	    if(o.tag == "Player")
	    {
			switch(type)
			{
				case t.fall:
					gameObject.AddComponent<Rigidbody2D>();
					GetComponent<Rigidbody2D>().AddForce(Vector2.right);
				break;
				case t.spike:
                    o.GetComponent<Movement>().Die();
				break;
			}      
	    }

        if(o.tag == "demon")
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
    }

	public enum t
	{
		fall,
		spike,
        normal
	};
}
