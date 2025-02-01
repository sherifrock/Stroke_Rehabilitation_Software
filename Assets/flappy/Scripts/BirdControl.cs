




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using PlutoDataStructures;

using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.IO.Enumeration;
//using static UnityEngine.Rendering.DebugUI;
//using static UnityEditor.PlayerSettings;





public class BirdControl : MonoBehaviour
{
    public Done_Boundary boundary;
    private float[] angXRange = { -40, 40 };
    //private float[] angZRange = { -200, 200 };

    private float[] angZRange = { -200, 200 };
    public float tilt;
    static float topBound = 6F;
    static float bottomBound = -3.2F;

    // max_x_init=681;
    //         min_x_init=81;
    //         max_y_init=-33;
    //         min_y_init=-633;

    public static BirdControl instance;
    private bool isDead = false;
    public static Rigidbody2D rb2d;
    Animator anime;
    // player controls

    public int controlValue;
    public static float playSize;
    public static int FlipAngle = 1;
    public static float tempRobot, tempBird;
    public bool set = false;
    public TMPro.TMP_Dropdown ControlMethod;
    public float angle1;

    int totalLife = 5;
    int currentLife = 0;
    bool columnHit;
    public Image life1;
    public Image life2;
    public Image life3;

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 2f;
    public bool startBlinking = false;

    public float happyTimer = 200.0f;

    public float speed = 0.001f;
    //public float down_speed = 3f;
    //public float up_speed = -3f;
    public float Player_translate_UpSpeed = 0.03f;
    public float Player_translate_DownSpeed = -0.03f;

    //static float topbound = 5.5F;
    //static float bottombound = -3.5F;
    //public static float playSize;
    public static float spawntime = 3f;
    private Vector2 direction;

    float startTime;
    float endTime;
    float loadcell;
    // Start is called before the first frame update

    float targetAngle;

    public FlappyGameControl FGC;

    //flappybird style for hand grip
    private Vector3 Direction;
    public float gravity = -9.8f;
    public float strength;

    long temp_ms = 0;

    int collision_count;
    public int total_life = 3;
    public int overall_life = 3;

    string date_now;
    string hospno;
    int hand_use;
    int count_session;

    public static int hit_count = 0;

    public static bool reduce_speed;

    StringBuilder csv = new StringBuilder();

    List<Vector3> paths;
    float max_y;
    float min_y;
    float y_c;

    private string filePath;
    private string welcompath = Staticvlass.FolderPath;
    public static string fileName;
    private FlappyColumn flappyColumn;
    public static string birdTime;

    //float theta1, theta2, theta3, theta4;

    // void Awake()
    // {
    //     if (instance == null)
    //         instance = this;
    //     else if (instance != this)
    //         Destroy(gameObject);
    // }

    public static class flappyclass
    {
        public static string flappypath;
        public static string relativepath;
    }

    void Start()
    {
        // Debug.Log("START");
        collision_count = 0;
        startTime = 0;
        endTime = 0;
        currentLife = 0;
        anime = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //anime.SetTrigger("Flap");
        //playSize = Camera.main.orthographicSize;

        // playSize = 2.3f + 5.5f;

        reduce_speed = false;
        paths = Drawlines.paths_pass;
        paths = new List<Vector3>();




        string welcompath = Staticvlass.FolderPath;
        string flappyfile = welcompath + "\\" + "flappy_Data";
        //if (!Directory.Exists(flappyfile))
        //{
        //    Directory.CreateDirectory(flappyfile);
        //}

        if (Directory.Exists(flappyfile))
        {
            fileName = Path.Combine(flappyfile, "FlappyGameData_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }
        else
        {
            Directory.CreateDirectory(flappyfile);
            fileName = Path.Combine(flappyfile, "FlappyGameData_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }

        //fileName = "FlappyGameData_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv";


        flappyclass.flappypath = fileName;

        string fullFilePath = flappyclass.flappypath;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
         flappyclass.relativepath = relativePath;

        flappyColumn = FindObjectOfType<FlappyColumn>();
        WriteHeader();




        playSize = topBound - bottomBound;




    }

    void WriteHeader()
    {
        if (!File.Exists(filePath))
        {

            string header = "CurrentDateTime,PlayerX,PlayerY,ColumnX,ColumnY,HitData\n";
            File.WriteAllText(flappyclass.flappypath, header);

        }
    }
    // Update is called once per frame

    private void Update()
    {
        if (startBlinking == true)
        {
            hit_count = collision_count;

            if (collision_count < total_life)
            {
                SpriteBlinkingEffect();
            }
            else
            {
                overall_life = overall_life - 1;
                // Debug.Log(overall_life+" :gameOver__");
                FlappyGameControl.instance.BirdDied();
                // Debug.Log("over!");
                collision_count = 0;
                life1.enabled = true;
                life2.enabled = true;
                life3.enabled = true;
                //Destroy(gameObject);
                gameObject.SetActive(false);
                if (overall_life == 0)
                {
                    // FlappyGameControl.instance.BirdDied();
                    reduce_speed = true;
                    overall_life = 3;
                    // Debug.Log(overall_life + ".." +reduce_speed+" :reduce_speed");
                }
                else
                {
                    reduce_speed = false;
                    // Debug.Log(reduce_speed+" :reduce_speed");
                }

            }

        }



    }


    // WORKING CODE
    void FixedUpdate()
    {
        // float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");




        float y_max = PlayerPrefs.GetFloat("y max");
        float y_min = PlayerPrefs.GetFloat("y min");

        Debug.Log(y_max);
        Debug.Log(y_min);


        float l1 = 333;
        float l2 = 381;
        float x2, y2;
        float theta1 = Dof.theta1;
        float theta2 = Dof.theta2;


        //Debug.Log(Dof.theta1 + "        " + Dof.theta2);

        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);



        x2 = ((x1) / (333 + 381) * 700f);
        y2 = ((y1 + 350) / (400 * 2)) * 4.8f;

        //x2 = ((x1) / (333 + 381) );
        //y2 = ((y1 + 350) / (400 * 2)) ;





        // Debug.Log("xaxis" + "   " + theta1 + "    " + thetaa + "   " + x1 + "    " + x2 + "    " + "yaxis" + theta2 + "    " + thetab + "   " + y1 + "    " + y2);


        this.transform.position = new Vector2(this.transform.position.x, (Angle2ScreenZ((y2), y_min, y_max)));
        Debug.Log(y_max + "  " + y_min);

        // Debug.Log(Angle2ScreenZ((Mathf.Sin(3.14f / 180 * theta1) * (475 * Mathf.Cos(3.14f / 180 * theta2) + 291 * Mathf.Cos(3.14f / 180 * theta2 + 3.14f / 180 * theta3))), y_min, y_max));
        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, 0);
        }
        else if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }


        int control = BirdControl.hit_count;
        //birdTime = DateTime.Now.ToString(" HH:mm:ss.fff");

        if (control < 3)
        {
            AutoData();
        }
       


    }
    public void AutoData()
    {


        //if (BirdControl.hit_count >= 3)
        //{
        //    return;
        //}

        // Get the current data to be logged
        float playerX = transform.position.x;
        float playerY = transform.position.y;
        // float columnX = /* Add your column X value here */1;
        //float columnY = /* Add your column Y value here */2;
        float columnX = flappyColumn.transform.position.x;
        float columnY = flappyColumn.transform.position.y;



        int hitData = BirdControl.hit_count; // Assuming hit_count is the hit data

        string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
         
        // Console.WriteLine(currentTime);


        // Append the data to the CSV file
        string newData = $"{currentTime}, {playerX}, {playerY}, {columnX}, {columnY}, {hitData}\n";
        File.AppendAllText(flappyclass.flappypath, newData);

       
    }
    public static float Angle2ScreenZ(float angleZ, float y_min, float y_max)
    {
        float y_Max = PlayerPrefs.GetFloat("y max");
        float y_Min = PlayerPrefs.GetFloat("y min");
        return Mathf.Clamp(bottomBound + (angleZ - y_Min) * ((playSize) / (y_Max - y_Min)), -3.6f * playSize, 3.6f * playSize);
    }




    public void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   // according to 
                                                                             //your sprite
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        startBlinking = true;
        collision_count++;
        // Debug.Log(collision_count+" :collision");
        if (collision_count == 1)
        {
            life1.enabled = false;
        }
        else if (collision_count == 2)
        {
            life2.enabled = false;
        }
        else if (collision_count == 3)
        {
            life3.enabled = false;
            
        }


    }



}



