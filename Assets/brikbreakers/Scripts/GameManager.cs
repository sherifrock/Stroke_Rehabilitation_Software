//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviour
//{
//    private static GameManager m_Instance;
//    public static GameManager Instance
//    {
//        get
//        {
//            if (m_Instance == null)
//            {
//                m_Instance = FindObjectOfType<GameManager>();
//                if (m_Instance == null)
//                {
//                    GameObject go = new GameObject("Game Manager");
//                    m_Instance = go.AddComponent<GameManager>();
//                }
//            }
//            return m_Instance;
//        }
//    }

//    private const int NUM_LEVELS = 2;

//    private Ball ball;
//    private Paddle paddle;
//    private Brick[] bricks;

//    private int level = 1;
//    private int score = 0;
//    private int lives = 3;

//    public int Level => level;
//    public int Score => score;
//    public int Lives => lives;

//    private void Awake()
//    {
//        if (m_Instance != null)
//        {
//            DestroyImmediate(gameObject);
//            return;
//        }

//        m_Instance = this;
//        DontDestroyOnLoad(gameObject);
//        FindSceneReferences();
//        SceneManager.sceneLoaded += OnLevelLoaded;
//    }

//    private void FindSceneReferences()
//    {
//        ball = FindObjectOfType<Ball>();
//        paddle = FindObjectOfType<Paddle>();
//        bricks = FindObjectsOfType<Brick>();
//    }

//    private void LoadLevel(int level)
//    {
//        this.level = level;

//        if (level > NUM_LEVELS)
//        {
//            // Start over again at level 1 once you have beaten all the levels
//            // You can also load a "Win" scene instead
//            LoadLevel(1);
//            return;
//        }

//        SceneManager.LoadScene("Level" + level);
//    }

//    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
//    {
//        FindSceneReferences();
//    }

//    public void OnBallMiss()
//    {
//        lives--;

//        if (lives > 0)
//        {
//            ResetLevel();
//        }
//        else
//        {
//            GameOver();
//        }
//    }

//    private void ResetLevel()
//    {
//        paddle.ResetPaddle();
//        ball.ResetBall();
//    }

//    private void GameOver()
//    {
//        // Start a new game immediately
//        // You can also load a "GameOver" scene instead
//        NewGame();
//    }

//    private void NewGame()
//    {
//        score = 0;
//        lives = 3;

//        LoadLevel(1);
//    }

//    public void Hit(Brick brick)
//    {
//        score += brick.points;

//        if (Cleared())
//        {
//            LoadLevel(level + 1);
//        }
//    }

//    private bool Cleared()
//    {
//        for (int i = 0; i < bricks.Length; i++)
//        {
//            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
//            {
//                return false;
//            }
//        }

//        return true;
//    }

//}



using System.IO;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Paddle;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance;

    private const int NUM_LEVELS = 2;

    private Ball ball;
    private Paddle paddle;
    private Brick[] bricks;

    private int level = 1;
    private int score = 0;
    //private int lives = 3;



    public Text LivesText;
    public Text ScoreText;

    public static int gamescore;
    public static string start_time;
    public static string end_time;
    public static string gamedata;
    public static string startdatetime;
    public static string endtime;
    public static string gameend_time;
    public static string datetime;
    public static int lives = 3;
    private string welcompath = Staticvlass.FolderPath;
    private string filePath;
    public static float player_x;
    public static float player_y;


    public int Level => level;
    public int Score => score;
    //public int Lives => lives;

    public static class brik
    {
        public static int score;
        public static int live;
    }

    private void Awake()
    {
        //    if (m_Instance != null)
        //    {
        //        DestroyImmediate(gameObject);
        //        return;
        //    }

       // m_Instance = this;
        DontDestroyOnLoad(gameObject);
        FindSceneReferences();
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public void Start()
    {

        //startdatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
        //start_time = DateTime.Now.ToString("HH:mm:ss.fff");
        //datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");



        //string brikfile = welcompath + "\\" + "brik_Data";
        //if (Directory.Exists(brikfile))
        //{
        //    filePath = Path.Combine(brikfile, "brik_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        //}
        //else
        //{
        //    Directory.CreateDirectory(brikfile);
        //    filePath = Path.Combine(brikfile, "brik_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        //}
        //brikclass.brikpath = filePath;

        //string fullFilePath = brikclass.brikpath;

        //// Define the part of the path you want to store
        //string partOfPath = @"Application.dataPath";

        //// Use Path class to get the relative path
        //string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
        //brikclass.relativepath = relativePath;

        ////pongclass.gamepath= 
        //WriteHeader();
    }

    //void WriteHeader()
    //{
    //    if (!File.Exists(filePath))
    //    {
    //        string header = "Time,PlayerX,PlayerY,BallX,BallY,PlayerScore,lives\n";
    //        File.WriteAllText(brikclass.brikpath, header);
    //    }

    //    //if (!File.Exists(gamedata))
    //    //{

    //    //    //string header = "CurrentDateTime,PlayerX,PlayerY,ColumnX,ColumnY,HitData\n";
    //    //    string header = "session,datetime,device,assessment,starttime,stoptime,gamename,datafilelocation\n";
    //    //    File.WriteAllText(gameclass.gamePath, header);

    //    //}
    //}
    //void LogData()
    //{
    //    // if (gameWon) return; // Stop logging data if the game is won

    //    GameObject ball = GameObject.FindGameObjectWithTag("Ball");
    //    // GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

    //    float ball_x = ball.transform.position.x;
    //    float ball_y = ball.transform.position.y;

    //    float gamescore = GameManager.gamescore;
    //    float live = lives;
    //    //float enemy_x = enemy.transform.position.x;
    //    //float enemy_y = enemy.transform.position.y;

    //    string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
    //    string data = $"{currentTime},{player_x},{player_y},{ball_x},{ball_y},{gamescore},{live}\n";

    //    File.AppendAllText(brikclass.brikpath, data);

    //    // Check for winning conditions
    //    // if (scoreclass.playerpoint >= 10 || scoreclass.enemypoint >= 10) // Example winning condition

    //    //if (scoreclass.playerpoint <= 5|| scoreclass.enemypoint <= 5)
    //    //if (scoreclass.playerpoint == 5 || scoreclass.enemypoint == 5) // Example winning condition
    //    //{
    //    //    gameWon = true; // Set the gameWon flag to true
    //    //}
    //}






    public void brikData()
    {

        string GameData_Bird = Application.dataPath;
        // Directory.CreateDirectory(GameData_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
        string filepath_Bird = GameData_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "gamedata.csv";



        // string filepath_Bird =  gameclass.gamePath;
        if (IsCSVEmpty(filepath_Bird))
        {

        }
        else
        {

        }

    }


    private bool IsCSVEmpty(string filepath_Bird)
    {


        int session = GameDataManager.instance.GetSessionNumber();

        string currentTime = datetime;
        string device = "R2";
        string assessment = "0";
        string starttime = start_time;
        endtime = end_time;



        //string endtime = end_time;
        string gamename = "BRIK-BREAKERS";
        string datalocation = brikclass.relativepath;
        string devicesetup = "null";
        string assistmode = "null";
        string assistmodeparameter = " null";
        string gameparameter = "null";





        if (File.Exists(filepath_Bird))
        {
            string Position_Bird = "";
            //check the file is empty,write header
            if (new FileInfo(filepath_Bird).Length == 0)
            {
                string Endata_Bird = "sessionnumber,datetime,device,assessment,starttime,Stoptime,gamename,traildatafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
                File.WriteAllText(filepath_Bird, Endata_Bird);
                DateTime currentDateTime = DateTime.Now;
                //string Position_Space = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';
                return true;
            }

            else
            {

                //If the file is not empty,return false
                DateTime currentDateTime = DateTime.Now;
                //string Position_SpaceR = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

                File.AppendAllText(filepath_Bird, Position_Bird);
                return true;
            }
        }
        else
        {
            string PositionBird = "";
            //If the file doesnt exist
            string DataPath_Bird = Application.dataPath;
            Directory.CreateDirectory(DataPath_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
            string filepath_Endata1_Bird = DataPath_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "gamedata.csv";
            string Endata1_Bird = "sessionnumber,datetime,device,assessment,starttime,Stoptime,gamename,traildatafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
            File.WriteAllText(filepath_Endata1_Bird, Endata1_Bird);
            DateTime currentDateTime = DateTime.Now;
            //string Position = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';
            PositionBird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

            File.AppendAllText(filepath_Endata1_Bird, PositionBird);
            return true;
        }
    }






  






    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
       

    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > NUM_LEVELS)
        {
            // Start over again at level 1 once you have beaten all the levels
            // You can also load a "Win" scene instead
            LoadLevel(1);
            return;
        }

        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSceneReferences();


    }

    public void OnBallMiss()
    {
        lives--;



        if (lives > 0)
        {
            ResetLevel();

        }
        else
        {
            end_time = DateTime.Now.ToString("HH:mm:ss.fff");
            GameOver();
        }

        LivesText.text = "lives:" + " " + lives.ToString();

        //LivesText.text = "lives:" + " " + lives;

        brik.live = lives;
    }
   

   
    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    private void GameOver()
    {
        // Start a new game immediately
        // You can also load a "GameOver" scene instead
        //NewGame();
        brikData();
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        LoadLevel(1);
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points;
        ScoreText.text = "score:" + " " + score.ToString();

        gamescore = score;
        if (Cleared())
        {
            LoadLevel(level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }


}








