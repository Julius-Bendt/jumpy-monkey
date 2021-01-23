using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ThemeChoser))]
public class ss_genv2 : MonoBehaviour {

    public float spacingX, spacingY;

	public static float currentX, currentY,roofY;

    public GameObject coin;
    public GameObject[] gnd, trap, end_door;

    List<GameObject> a_gen = new List<GameObject>();

    public t type;

    private GameObject p_door;

    ThemeChoser tc;

    int time_to_whitespace;

    public bool useRandomSeed = true;

    public string seed;

    public enum t
    {
        sidescroller,
        stair_of_dead,
    };
	
    void Start()
    {
        GenNewLevel();
    }



    public string GetSeed()
    {
        System.DateTime time = System.DateTime.Now;
        System.DayOfWeek day = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= System.DayOfWeek.Monday && day <= System.DayOfWeek.Wednesday)
        {
            time = time.AddDays(3);
        }
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday).ToString();
    }

    public void GenNewLevel()
	{
        Debug.Log("genNewLevel");
		clear();

        initTC();

        switch (type)
		{
		case t.sidescroller:
                Gen_World();
            GetComponent<ss_CameraScript>().NewLevel();
			break;
			
		case t.stair_of_dead:
			gen_stair_of_dead();
            GetComponent<ss_CameraScript>().NewLevel();
			break;
		}
		
		if(prog_bar.exits)
		{
			prog_bar.max = currentX;
		}
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !Movement.isdead)
        {
            initTC();
			destroyPlayers();
        }
    }

	void destroyPlayers()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++)
		{
			Destroy(players[i]);
		}
		
		ss_Gamecontroller.bybassWaitTime = true;
		Movement.isdead = true;
	}

    
    void gen_stair_of_dead()
    {

        seed = GetSeed();

        /*
        EasySave.Manager.Save("randomSeed", useRandomSeed.ToString());

        if (EasySave.Manager.KeyExist("seed"))
        {
            if(!useRandomSeed)
            {
                string EZSeed = EasySave.Manager.Load("seed");

                if (EZSeed == seed)
                {
                    if (EasySave.Manager.KeyExist("TimesComplete"))
                    {
                        seed += EasySave.Manager.Load("TimesComplete");
                    }
                }
            }
           
        }
        else
        {
            EasySave.Manager.Save("seed", seed);
        }
        */
        

        if(useRandomSeed)
        {
            seed = System.DateTime.Now.ToString("h:mm:ss");
        }

        System.Random s = new System.Random(seed.GetHashCode());

        CreateWall(5,1, s);
        CreateGround(10,false, 0, s);

        for(int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateStair(Random.Range(25, 35), -1, true,s);
            }

            CreateGround(Random.Range(2, 5),true, 20, s);
            CreateGround(Random.Range(5, 15),true, 20, s);
        }

        CreateEnd(s);
        CreateWall(Random.Range(5, 10),1, s);
        p_door.GetComponent<ss_teleporter>().teleport_to = ss_teleporter.tp.sidescroller;

    }
    

    public void Gen_World(int length = 0)
    {
        clear();

        seed = GetSeed();

        /*
        EasySave.Manager.Save("randomSeed", useRandomSeed.ToString());

        if (EasySave.Manager.KeyExist("seed"))
        {
            if(!useRandomSeed)
            {
                string EZSeed = EasySave.Manager.Load("seed");

                if (EZSeed == seed)
                {
                    if (EasySave.Manager.KeyExist("TimesComplete"))
                    {
                        seed += EasySave.Manager.Load("TimesComplete");
                    }
                }
            }
           
        }
        else
        {
            EasySave.Manager.Save("seed", seed);
        }
        */

        if(useRandomSeed)
        {
            seed = System.DateTime.Now.ToString("h:mm:ss");
        }

        System.Random s = new System.Random(seed.GetHashCode());


        if(length == 0) //By default
        {
            length = s.Next(30, 70); //Size of the map blocks.
        }

        currentY = spacingY * 10;
        CreateGround(10,false, 0, s);

        currentY = 0;
        currentX = 0;

        CreateWall(10, 1, s);
        CreateGround(7,false, 0, s);

        //EasySave.Manager.Save("startposX", (currentX / 2).ToString());
        //EasySave.Manager.Save("startposY", (currentY + spacingY).ToString());
        //EasySave.Manager.Save("NewStartpos", "true");

        for(int i = 0; i < length; i++)
        {
            int num = s.Next(0, 190);
            
            if(num > 0 && num < 30)
            {
                int l = s.Next(4, 9);
                bool t = System.Convert.ToBoolean(s.Next(0, 1));
                CreateGround(l,t, 50, s);
            }

            if (num > 30 && num < 50)
            {
                int l = s.Next(5,13);
                int d = s.Next(-1,1);

                if(d == 0)
                {
                    d = 0;
                }

                bool t = System.Convert.ToBoolean(s.Next(0, 1));
                CreateStair(l, d, t, s);
            }

            if (num > 50 && num < 70)
            {
                int l = s.Next(4, 8);
                int h = s.Next(4, 10);

                bool t = System.Convert.ToBoolean(s.Next(0, 1)); 

                CreateTrap(l, h, t, s);
            }

            if (num > 70 && num < 90)
            {
                int l = s.Next(4, 8);
                int h = s.Next(4, 10);

                bool t = System.Convert.ToBoolean(s.Next(0, 1));

                CreateHalfRoom(l, h, t, s);
            }

            if (num > 90 && num < 100)
            {
                int l = s.Next(1, 5);
                int h = s.Next(1, 3);

                CreateSpace(l, h, s);
            }

            if (num > 100 && num < 125)
            {
                int l = s.Next(1, 5);

                int d = s.Next(-1, 1);

                if (d == 0)
                {
                    d = 1;
                }

                CreateWall(l, d, s);
            }

            if (num > 125 && num < 150)
            {
                int l = s.Next(1, 5);
                int h = s.Next(1, 3);

                bool t = System.Convert.ToBoolean(s.Next(0, 1));

                CreateWallStair(l, h, t, s);
            }

            if (num > 150 && num < 170)
            {
                bool t = System.Convert.ToBoolean(s.Next(0, 1));

                CreatePolygon(t, s);
            }

            if(num > 170 && num < 190)
            {
                int l = s.Next(3, 9);
                CreateBars(l, s);
            }


        }

        CreateEnd(s);
    }

    public void clear()
    {
        currentX = 0;
        currentY = 0;

        for (int i = 0; i < a_gen.Count; i++)
        {
            Destroy(a_gen[i].gameObject);
        }
        a_gen.Clear();
    }

    void CreateEnd(System.Random s)
    {
        float x = currentX;
        CreateGround(7, false, 0, s);

        GameObject door = end_door[s.Next(0, end_door.Length - 1)].gameObject;

        door = Instantiate(door,new Vector2(currentX - spacingX,currentY + spacingY), Quaternion.identity) as GameObject;

        door.GetComponent<ss_teleporter>().teleport_to = ss_teleporter.tp.sidescroller;

        CreateWall(7, 1, s);

        //Roof
        currentX = x;
        currentY = 8 * spacingY;
        CreateGround(7, false,0, s);
    }

    void CreateGround(int length,bool traps,float chance_for_traps,System.Random seed)
    {
        for(int i = 0; i < length; i++)
        {
            GameObject g = gnd[seed.Next(0, gnd.Length - 1)];

            g = (GameObject)Instantiate(g, new Vector2(currentX + spacingX, currentY), Quaternion.identity);

            currentX = g.transform.position.x;
            currentY = g.transform.position.y;

            a_gen.Add(g);

            if (traps && i > 1)
            {
                if(seed.Next(0,100) < chance_for_traps)
                {
                    GameObject t = trap[seed.Next(0, trap.Length - 1)];
                    t = (GameObject)Instantiate(t, new Vector2(currentX, currentY + spacingY), Quaternion.identity);
                    a_gen.Add(t);
                }
            }

            

           
        }
    }

    void CreateStair(int length,int dir, bool traps,System.Random seed)
    {
        if(dir == 0)
        {
            dir = 1;
        }

        for (int i = 0; i < length; i++)
        {
            GameObject g = gnd[seed.Next(0, gnd.Length - 1)];

            g = (GameObject)Instantiate(g, new Vector2(currentX + spacingX, currentY + (spacingY * dir)), Quaternion.identity);

            currentX = g.transform.position.x;
            currentY = g.transform.position.y;

            a_gen.Add(g);
        }
    }

    void CreateTrap(int length,int height, bool traps,System.Random seed)
    {
        CreateWall(height + 1, -1, seed);
        float y = currentY;
        currentY = a_gen[a_gen.Count - 1].transform.position.y;

        CreateGround(length, traps,seed.Next(25,75), seed);

        currentY = y;

        CreateWall(height, -1, seed);

        currentY = a_gen[a_gen.Count - 1].transform.position.y;
    }

    void CreateHalfRoom(int length, int height, bool traps, System.Random seed)
    {
        if(seed.Next(0,100) >= 50)
        {
            CreateWall(height, -1, seed);
            CreateGround(length, traps, seed.Next(25, 75), seed);
        }
        else
        {
            CreateGround(length, traps, seed.Next(25, 75), seed);
            CreateWall(height, -1, seed);
        }
    }

    void CreateSpace(int length,int height, System.Random seed)
    {
        currentX += (spacingX * length);
        currentY += (spacingY * height);
    }

    void CreateWall(int length, int dir, System.Random seed)
    {
        GameObject f = null;
        for (int i = 0; i < length; i++)
        {
            GameObject g = gnd[seed.Next(0, gnd.Length - 1)];

            g = (GameObject)Instantiate(g, new Vector2(currentX, currentY + spacingY * dir), Quaternion.identity);

            currentX = g.transform.position.x;
            currentY = g.transform.position.y;

            a_gen.Add(g);

            if(i == 0)
            {
                f = g;
            }
        }

        currentX = f.transform.position.x;
        currentY = f.transform.position.y;
    }

    void CreateWallStair(int length, int height,bool traps,System.Random seed)
    {
        int dir = seed.Next(-1, 1);

        if(dir == 0)
        {
            dir = 1;
        }

        if(seed.Next(0,100) > 50)
        {
            CreateWall(height, dir, seed);
            CreateStair(length, -dir, traps, seed);
        }
        else
        {
            CreateStair(length, -dir, traps, seed);
            CreateWall(height, dir, seed);
        }
    }

    void CreatePolygon(bool traps,System.Random seed)
    {
        int length = seed.Next(4, 9);

        CreateGround(length, traps, seed.Next(25, 75), seed);
        CreateSpace(0, -1, seed);
        CreateGround(length, traps, seed.Next(25, 75), seed);
        CreateSpace(0, 1, seed);
        CreateGround(length, traps, seed.Next(25, 75), seed);
    }

    void CreateBars(int length, System.Random seed)
    {
        for(int i = 0; i < length; i++)
        {
            CreateSpace(seed.Next(5, 11), seed.Next(-4, 2), seed);

            int height = seed.Next(5, 9);
            CreateWall(height, 1, seed);

            CreateSpace(-2, height, seed);
            CreateGround(length, true, seed.Next(5, 10), seed);

            CreateSpace(2, -height, seed); // normalize
        }
    }

    void spawnCoin(System.Random seed, int chance = 2)
    {
        int r = seed.Next(0, 100);

        if(r < chance)
        {
            Instantiate(coin, new Vector2(currentX,currentY + spacingY), Quaternion.identity);
        }
    }


    bool rndbool(int chanceForTrue = 65)
    {
        float r = Random.Range(0,100);

        if (r > chanceForTrue && r < 100)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    int rnddir(int chanceForPositive = 65)
    {
        float r = Random.Range(0,100);

        if(r > 0 && r < chanceForPositive)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    void initTC()
    {
        tc = GetComponent<ThemeChoser>();
        tc.id = Random.Range(0, tc.Themes.Count);

        gnd = tc.Themes[tc.id].grounds;
        trap = tc.Themes[tc.id].traps;
        end_door = tc.Themes[tc.id].end_doors;

        GetComponent<ss_CameraScript>().NewLevel();
    }
}
