













//using UnityEngine;
////using UnityEngine;
//using System;
//using System.IO;

//public class PongPlayerController : MonoBehaviour
//{
//    // Boundary limits
//    public float speed = 10;
//    public static PongPlayerController Instance;
//    public static float topBound = 3.6f;
//    public static float bottomBound = -3.6f;
//    public static float theta1;
//    public static float theta2;
//    public static float playSize;
//    public static float player_x, player_y;




//    private string welcompath = Staticvlass.FolderPath; // Replace with your folder path
//    private string filePath;
//    private bool isPaused = false;






//    //public static float player_x, player_y;

//   // private string welcompath = Staticvlass.FolderPath;
//    public static class pongclass
//{

//        public static string filepath;
//    }


//    void Start()
//    {

//        string pongfile = welcompath + "\\" + "Pong_Data";
//        if (Directory.Exists(pongfile))
//        {
//            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
//        }
//        else
//        {
//            Directory.CreateDirectory(pongfile);
//            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
//        }
//        pongclass.filepath = filePath;
//        WriteHeader();
//    }

//    void WriteHeader()
//    {
//        if (!File.Exists(filePath))
//        {
//            string header = "Time,PlayerX,PlayerY,EnemyX,EnemyY,BallX,BallY,PlayerScore,EnemyScore\n";
//            File.WriteAllText(pongclass.filepath, header);
//        }
//    }



//    void Update()
//    {
//        Game();
//        playSize = topBound - bottomBound;

//        LogData();
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.tag == "Target")
//        {


//        }
//    }
//    private void OntriggerEnter2D(Collision2D collision)
//    {
//        Debug.Log("hello");
//    }

//    void Game()
//    {
//        // Get paddle positions from external sources (e.g., sensors, inputs)
//        //float theta1 = Dof.theta1; // Example value from external source
//        //float theta2 = Dof.theta2; // Example value from external source

//        //// Calculate paddle's position based on angles or other input mechanisms
//        //float x1 = -Mathf.Sin(theta1 * Mathf.Deg2Rad) * 333 - Mathf.Sin((theta1 + theta2) * Mathf.Deg2Rad) * 381;
//        //float y1 = Mathf.Cos(theta1 * Mathf.Deg2Rad) * 333 + Mathf.Cos((theta1 + theta2) * Mathf.Deg2Rad) * 381;

//        //// Map the calculated position to the game's vertical boundaries
//        //float yMax = PlayerPrefs.GetFloat("y max");
//        //float yMin = PlayerPrefs.GetFloat("y min");
//        //float yPos = Mathf.Clamp(-(y1 + 350) / (400 * 2) * 4.8f, bottomBound, topBound);

//        //// Set the paddle's position
//        //transform.position = new Vector2(transform.position.x, yPos);



//        float y_max = PlayerPrefs.GetFloat("y max");
//        float y_min = PlayerPrefs.GetFloat("y min");

//        Debug.Log(y_max);
//        Debug.Log(y_min);


//        float l1 = 333;
//        float l2 = 381;
//        theta1 = Dof.theta1;
//        theta2 = Dof.theta2;



//        float thetaa = Dof.theta1 * Mathf.Deg2Rad;
//        float thetab = Dof.theta2 * Mathf.Deg2Rad;

//        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
//        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);

//        //x = ((x1) / (333 + 381) * 7.5f);
//        //y = ((y1 + 350) / (400 * 2)) * 4.8f;


//        float x_val = ((x1) / (333 + 381) * 12f);
//        float y_val = ((y1 + 350) / (400 * 2)) * 4.8f;


//        Debug.Log(y_val);
//        this.transform.position = new Vector2(this.transform.position.x, (Angle2ScreenZ((y_val), y_min, y_max)));



//        // Debug.Log(Angle2ScreenZ((Mathf.Sin(3.14f / 180 * theta1) * (475 * Mathf.Cos(3.14f / 180 * theta2) + 291 * Mathf.Cos(3.14f / 180 * theta2 + 3.14f / 180 * theta3))), y_min, y_max));
//        if (transform.position.y > topBound)
//        {
//            transform.position = new Vector3(transform.position.x, topBound, 0);
//        }
//        else if (transform.position.y < bottomBound)
//        {
//            transform.position = new Vector3(transform.position.x, bottomBound, 0);
//        }
//         player_x = this.transform.position.x;
//         player_y = y_val;


//       // LogData();
//    }





//    void LogData()
//    {
//        GameObject ball = GameObject.FindGameObjectWithTag("Target");
//        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

//        float ball_x = ball.transform.position.x;

//        float ball_y = ball.transform.position.y;
//        // Debug.Log(ball_y);
//        float enemy_x = enemy.transform.position.x;
//        //Debug.Log(enemy_x);
//        float enemy_y = enemy.transform.position.y;
//        //Debug.Log(enemy_y);

//        //Debug.Log("ball" + ball_x + "     " + ball_y +"enemy"+"   "+ enemy_x+"        "+enemy_y);

//        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//        //string data = $"{currentTime},{player_x},{player_y},{enemy_x},{enemy_y},{ball_x},{ball_y},{GetComponent<BoundController>().playerScore},{GetComponent<BoundController>().enemyScore}\n";



//        string data = $"{currentTime},{player_x},{player_y},{enemy_x},{enemy_y},{ball_x},{ball_y},{scoreclass.playerpoint},{scoreclass.enemypoint}\n";

//        Debug.Log(" score" + "      " + scoreclass.playerpoint + "     " + scoreclass.enemypoint);
//        File.AppendAllText(pongclass.filepath, data);
//    }
//    public static float Angle2ScreenZ(float angleZ, float y_min, float y_max)
//    {
//        float y_Max = PlayerPrefs.GetFloat("y max");
//        float y_Min = PlayerPrefs.GetFloat("y min");
//        return Mathf.Clamp(bottomBound + (angleZ - y_Min) * ((playSize) / (y_Max - y_Min)), -3.6f * playSize, 3.6f * playSize);
//    }
//}












using UnityEngine;
using System;
using System.IO;

public static class pongclass
{
    public static string filepath;
    public static string gamepath;
    public static string relativepath;
}

public class PongPlayerController : MonoBehaviour
{
    public static PongPlayerController Instance;
    public static float topBound = 3.6f;
    public static float bottomBound = -3.6f;
    public static float theta1;
    public static float theta2;
    public static float playSize;
    public static float player_x, player_y;

    private string welcompath = Staticvlass.FolderPath; // Replace with your folder path
    private string filePath;
   // private string gamedata = gameclass.gamePath;
    private bool isPaused = false;
    private bool gameWon = false; // Variable to track if the game is won

    void Start()
    {
        string pongfile = welcompath + "\\" + "Pong_Data";
        if (Directory.Exists(pongfile))
        {
            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }
        else
        {
            Directory.CreateDirectory(pongfile);
            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }
        pongclass.filepath = filePath;

        string fullFilePath = pongclass.filepath;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
        pongclass.relativepath = relativePath;

        //pongclass.gamepath= 
        WriteHeader();
    }

    void Update()
    {



        playSize = topBound - bottomBound;
        if (!isPaused && !gameWon)
        {
            LogData();
            Game();
        }
    }

    void Game()
    {
        float y_max = PlayerPrefs.GetFloat("y max");
        float y_min = PlayerPrefs.GetFloat("y min");

        float l1 = 333;
        float l2 = 381;
        theta1 = Dof.theta1;
        theta2 = Dof.theta2;

        float thetaa = Dof.theta1 * Mathf.Deg2Rad;
        float thetab = Dof.theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos(thetaa) * l1 + Mathf.Cos(thetaa + thetab) * l2);
        float x1 = -(Mathf.Sin(thetaa) * l1 + Mathf.Sin(thetaa + thetab) * l2);

        float x_val = (x1 / (333 + 381) * 12f);
        float y_val = ((y1 + 350) / (400 * 2)) * 4.8f;
        Debug.Log(y_val);

        transform.position = new Vector2(transform.position.x, Angle2ScreenZ(y_val, y_min, y_max));
        Debug.Log(transform.position);
        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, 0);
        }
        else if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }

        player_x = transform.position.x;

            player_y = transform.position.y;
    }

    public static float Angle2ScreenZ(float angleZ, float y_min, float y_max)
    {
        return Mathf.Clamp(bottomBound + (angleZ - y_min) * (playSize / (y_max - y_min)), -3.6f * playSize, 3.6f * playSize);
    }

    void WriteHeader()
    {
        if (!File.Exists(filePath))
        {
            string header = "Time,PlayerX,PlayerY,EnemyX,EnemyY,BallX,BallY,PlayerScore,EnemyScore\n";
            File.WriteAllText(pongclass.filepath, header);
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
        if (gameWon) return; // Stop logging data if the game is won

        GameObject ball = GameObject.FindGameObjectWithTag("Target");
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        float ball_x = ball.transform.position.x;
        float ball_y = ball.transform.position.y;
        float enemy_x = enemy.transform.position.x;
        float enemy_y = enemy.transform.position.y;

        string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
        string data = $"{currentTime},{player_x},{player_y},{enemy_x},{enemy_y},{ball_x},{ball_y},{scoreclass.playerpoint},{scoreclass.enemypoint}\n";

        File.AppendAllText(pongclass.filepath, data);

        // Check for winning conditions
        // if (scoreclass.playerpoint >= 10 || scoreclass.enemypoint >= 10) // Example winning condition

        //if (scoreclass.playerpoint <= 5|| scoreclass.enemypoint <= 5)
        if (scoreclass.playerpoint == 5 || scoreclass.enemypoint == 5) // Example winning condition
        {
            gameWon = true; // Set the gameWon flag to true
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
