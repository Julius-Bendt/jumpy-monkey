using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    public float speed = 0.2f, jumpHeight = 5;

    public GameObject blood;

    Rigidbody2D r;
    [HideInInspector]
    public float ld = 0;

    bool canjump = true, jump2 = true;

    [HideInInspector]
    public bool canwalk = true;

    public Sprite standing, jumping;

    public GameObject theCamera;

    public static bool isdead = false;

	public AudioClip jumpclip;

    RaycastOrigins raycastOrigins;


    // Jump region
    BoxCollider2D collider;

    const float skinWidth = .05f;

    [Range(3,15)]
    public int rayCount = 5;

    public LayerMask layer;

    float raySpacing;

    public mt movetype = mt.translate;

    public enum mt
    {
        rigid,
        translate
    };

    Animator anim;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        ss_CameraScript.startx = transform.position.x;
        r = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

	void Update () 
    {           
        float dir = Input.GetAxisRaw("Horizontal");

    /*
        if(EasySave.Manager.KeyExist("NewStartpos"))
        {
            bool np = System.Convert.ToBoolean(EasySave.Manager.Load("NewStartpos"));

            if(np)
            {
                EasySave.Manager.Save("NewStartpos", "false");
                float x = float.Parse(EasySave.Manager.Load("startposX"));
                float y = float.Parse(EasySave.Manager.Load("startposY"));

                transform.position = new Vector2(x, y);
            }
        }
        */

        anim.SetBool("walking", System.Convert.ToBoolean(dir));

        if (dir != 0)
        {
            ld = dir;

        }

        if (ld == 1)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (Input.GetButton("Horizontal") && canwalk)
        {
            switch (movetype)
            {
                case mt.rigid:
                    if (GetComponent<Rigidbody2D>().velocity.x < (speed * 100))
                    {
                        GetComponent<Rigidbody2D>().AddForce(transform.right * (speed * 100), ForceMode2D.Impulse);
                    }
                    break;
                case mt.translate:
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    break;
            }

        }

        checkJump();

        if (Input.GetButtonDown("Jump"))
        {

            if (canjump)
            {
                GetComponent<AudioSource>().clip = jumpclip;
                GetComponent<AudioSource>().pitch = 0.75f;
                GetComponent<AudioSource>().Play();
                canjump = false;
                r.AddForce(Vector2.up * jumpHeight);
                r.AddForce(Vector2.right * ld * 3f);
                GetComponent<SpriteRenderer>().sprite = jumping;
            }
            else
            {
                if (jump2)
                {
                    GetComponent<AudioSource>().clip = jumpclip;
                    GetComponent<AudioSource>().pitch = 0.9f;
                    GetComponent<AudioSource>().Play();
                    jump2 = false;
                    r.AddForce(Vector2.up * jumpHeight);
                    r.AddForce(Vector2.right * ld * 5f);
                }
            }



        }

        if (r.velocity.y > 11f)
        {
            r.velocity = new Vector2(r.velocity.x, 10.5f);
        }

    }

    void checkJump()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        for(int i = 0; i < rayCount; i++)
        {
            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * raySpacing * i, Vector2.up * -0.09f,Color.yellow);

            if(Physics2D.Raycast(raycastOrigins.bottomLeft + Vector2.right * raySpacing * i, -Vector2.up, .25f,layer))
            {
                canjump = jump2 = true;
            }
        }
    }

   struct RaycastOrigins
    {
        public Vector2 bottomLeft, bottomRight;
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);    

    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        rayCount = Mathf.Clamp(rayCount, 3, 15);

        raySpacing = bounds.size.x / (rayCount - 1);
    }

    public void Die()
    {
        GameObject g = Instantiate(blood, transform.position, new Quaternion(0, 180, 180, 1)) as GameObject;
        g.transform.rotation = new Quaternion(1,0,0,0);
        isdead = true;
        Destroy(gameObject);
            

    }

}
