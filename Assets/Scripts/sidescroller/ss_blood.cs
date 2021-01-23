using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ss_blood : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 10f);
		//GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500,ForceMode2D.Force);
    }

	void Update ()
    {
        transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one * 0.05f, Time.deltaTime * 1.5f);
	}
}
