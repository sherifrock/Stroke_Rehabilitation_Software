//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class Paddle : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    private Vector2 direction;

//    public float speed = 30f;
//    public float maxBounceAngle = 75f;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    private void Start()
//    {
//        ResetPaddle();
//    }

//    public void ResetPaddle()
//    {
//        rb.velocity = Vector2.zero;
//        transform.position = new Vector2(0f, transform.position.y);
//    }

//    private void Update()
//    {
//        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
//            direction = Vector2.left;
//        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
//            direction = Vector2.right;
//        } else {
//            direction = Vector2.zero;
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (direction != Vector2.zero) {
//            rb.AddForce(direction * speed);
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (!collision.gameObject.CompareTag("Ball")) {
//            return;
//        }

//        Rigidbody2D ball = collision.rigidbody;
//        Collider2D paddle = collision.otherCollider;

//        // Gather information about the collision
//        Vector2 ballDirection = ball.velocity.normalized;
//        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

//        // Rotate the direction of the ball based on the contact distance
//        // to make the gameplay more dynamic and interesting
//        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
//        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

//        // Re-apply the new direction to the ball
//        ball.velocity = ballDirection * ball.velocity.magnitude;
//    }

//}

using System.IO;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]






public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;

    public static Paddle instanse;

    public Done_Boundary boundary;

    // Boundary limits for the paddle movement
    //public float xMin = -12f;
    // public float xMax = 12f;


    private float[] angXRange = { -150, 150 };
    private float[] angZRange = { -40, -120 };
    double x_c;
    double y_c;
    float x_val, z_val;
    // These are the values you will set externally
    public float theta1;
    public float theta2;

    public float l1 = 333f; // Length of the first segment
    public float l2 = 381f; // Length of the second segment

    public float speed = 30f;
    public float maxBounceAngle = 75f;
    public static float topBound = 15;
    public static float bottomBound = -15;
    public static float playSize;
    private string welcompath = Staticvlass.FolderPath;
    private string filePath;
    public static float player_x;
    public static float player_y;
    public static float gamescore;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //public void start()
    //{

    //    // ResetPaddle();
    //}
    public static class brikclass
    {
        public static string brikpath;
        public static string relativepath;
    }

    private void Start()
    {


        string brikfile = welcompath + "\\" + "brik_Data";
        if (Directory.Exists(brikfile))
        {
            filePath = Path.Combine(brikfile, "brik_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }
        else
        {
            Directory.CreateDirectory(brikfile);
            filePath = Path.Combine(brikfile, "brik_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }
        brikclass.brikpath = filePath;

        string fullFilePath = brikclass.brikpath;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
        brikclass.relativepath = relativePath;

        //pongclass.gamepath= 
        WriteHeader();







        angXRange[0] = Drawpath.instance.max_x;
        angXRange[1] = Drawpath.instance.min_x;

        Debug.Log(angXRange[0] + "   " + angZRange[1]);
        boundary = new Done_Boundary(-15, 15, -4, 12);


        x_c = (angXRange[0] + angXRange[1]) / 2;
        y_c = (angZRange[0] + angZRange[1]) / 2;


        boundary = new Done_Boundary(-12, 12, -4, 12);
        ResetPaddle();
    }

    void WriteHeader()
    {
        if (!File.Exists(filePath))
        {
            string header = "Time,PlayerX,PlayerY,BallX,BallY,PlayerScore,lives\n";
            File.WriteAllText(brikclass.brikpath, header);
        }

        //if (!File.Exists(gamedata))
        //{

        //    //string header = "CurrentDateTime,PlayerX,PlayerY,ColumnX,ColumnY,HitData\n";
        //    string header = "session,datetime,device,assessment,starttime,stoptime,gamename,datafilelocation\n";
        //    File.WriteAllText(gameclass.gamePath, header);

        //}
    }

    void LogData()
    {
        // if (gameWon) return; // Stop logging data if the game is won

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        // GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        float ball_x = ball.transform.position.x;
        float ball_y = ball.transform.position.y;

        float gamescore = GameManager.gamescore;
        float live = GameManager.lives ;
        //float enemy_x = enemy.transform.position.x;
        //float enemy_y = enemy.transform.position.y;

        string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
        string data = $"{currentTime},{player_x},{player_y},{ball_x},{ball_y},{gamescore},{live}\n";

        File.AppendAllText(brikclass.brikpath, data);

        // Check for winning conditions
        // if (scoreclass.playerpoint >= 10 || scoreclass.enemypoint >= 10) // Example winning condition

        //if (scoreclass.playerpoint <= 5|| scoreclass.enemypoint <= 5)
        //if (scoreclass.playerpoint == 5 || scoreclass.enemypoint == 5) // Example winning condition
        //{
        //    gameWon = true; // Set the gameWon flag to true
        //}
    }


    public void ResetPaddle()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    private void Update()
    {

        playSize = topBound - bottomBound;
        float y_max = PlayerPrefs.GetFloat("x max");
        float y_min = PlayerPrefs.GetFloat("x min");

        float l1 = 333;
        float l2 = 381;
        theta1 = Dof.theta1;
        theta2 = Dof.theta2;

        float thetaa = Dof.theta1 * Mathf.Deg2Rad;
        float thetab = Dof.theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos(thetaa) * l1 + Mathf.Cos(thetaa + thetab) * l2);
        float x1 = -(Mathf.Sin(thetaa) * l1 + Mathf.Sin(thetaa + thetab) * l2);

        float y_val = (x1 / (333 + 381) * 12f);
        //float y_val = ((y1 + 350) / (400 * 2)) * 4.8f;
        Debug.Log(y_val);

        transform.position = new Vector2(Angle2ScreenZ(y_val, y_min, y_max), -8);
        Debug.Log(transform.position);
        if (transform.position.x > topBound)
        {
            transform.position = new Vector3(topBound, transform.position.y, 0);
        }
        else if (transform.position.x < bottomBound)
        {
            transform.position = new Vector3(bottomBound, transform.position.y, 0);
        }

        player_x = transform.position.x;

        player_y = transform.position.y;

        LogData();
    }

    public static float Angle2ScreenZ(float angleZ, float y_min, float y_max)
    {
        return Mathf.Clamp(bottomBound + (angleZ - y_min) * (playSize / (y_max - y_min)), -2.6f * playSize, 2.6f * playSize);
    }

    //3.6

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;

        // Gather information about the collision
        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

        // Rotate the direction of the ball based on the contact distance
        // to make the gameplay more dynamic and interesting
        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

        // Re-apply the new direction to the ball
        ball.velocity = ballDirection * ball.velocity.magnitude;
    }
}









