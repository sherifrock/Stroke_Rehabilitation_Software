//using PlutoDataStructures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using static UIManager;
using UnityEngine.Rendering.Universal;
public class FlappyGameControl : MonoBehaviour
{
    // public hyper1 h;
    public AudioClip[] winClip;
    public AudioClip[] hitClip;
    public Text ScoreText;
    public ProgressBar timerObject;
    public static FlappyGameControl instance;
    //public RockVR.Video.VideoCapture vdc;
    public GameObject GameOverText;
    public GameObject CongratulationsText;
    public bool gameOver = false;
    //public float scrollSpeed = -1.5f;
    public float scrollSpeed;
    private int score;
    public GameObject[] pauseObjects;
    public float gameduration = 90 * 5;
    //public float gameduration = PlayerPrefs.GetFloat("");
    public GameObject start;
    int win = 0;
    bool endValSet = false;

    public int startGameLevelSpeed = 1;
    public int startGameLevelRom = 1;
    public float ypos;

    public GameObject menuCanvas;
    public GameObject Canvas;

    public BirdControl bc;

    public Text durationText;
    private int duration = 0;
    IEnumerator timecoRoutine;
    bool column_position_flag;
    public static bool column_position_flag_topass;

    public Text LevelText;

    public static string start_time;
    public static string current;
    string end_time;
    string p_hospno;
    int hit_count;

    float start_speed;
    public static float auto_speed = -2.0f;

    int total_life = 3;

    DateTime last_three_duration;

    string path_to_data;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        //AppData.game = "FLAPPY BIRD";

    }


    // Start is called before the first frame update
    void Start()
    {
        path_to_data = Application.dataPath;

        LevelText.enabled = false;
        scrollSpeed = -(PlayerPrefs.GetFloat("ScrollSpeed"));
        Time.timeScale = 1;
        //timerObject.isOn = true;
        //timerObject.enableSpecified = true;
        // AppData.timeOnTrail = 0;
        //AppData.reps = 0;
        //vdc.customPath = false;
        //vdc.customPathFolder = "";
        //vdc.filePath = AppData.GameVideoLog(AppData.subjHospNum, AppData.plutoData.mechs[AppData.plutoData.mechIndex], AppData.game, AppData.regime);
        //// Debug.Log(vdc.filePath);
        //vdc.StartCapture();
        // Time.timeScale = 0;
        ShowGameMenu();
        column_position_flag = false;
       


    }

     void OnDisable()
    {
        FlappyData(); 
    }

    public void FlappyData()
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
            Debug.Log("file  path not consider");
        }

    }


    private bool IsCSVEmpty(string filepath_Bird)
    {


        int session = GameDataManager.instance.GetSessionNumber();
        string current = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
        string currentTime = current ;
        string device = "R2";
        string assessment = "0";
        string starttime = start_time;


        string endtime = end_time;
        //string endtime = DateTime.Now.ToString("HH:mm:ss.fff");
        




        //string endtime = end_time;
        string gamename = "TUK-TUK";
        string datalocation =BirdControl.flappyclass.relativepath;

        string devicesetup = "null";
        string assistmode = "null";
        string assistmodeparameter = " null";
        string gameparameter = "null";
        // gameclass.gamePath = circleclass.circlePath;
        //string newFileName = gameclass.gamePath;






        if (File.Exists(filepath_Bird))
        {
            string Position_Bird = "";
            //check the file is empty,write header
            if (new FileInfo(filepath_Bird).Length == 0)
            {
                string Endata_Bird = "session,datetime,device,assessment,starttime,stoptime,gamename,datafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
                File.WriteAllText(filepath_Bird, Endata_Bird);
                DateTime currentDateTime = DateTime.Now;
                //string Position_Space = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation +"," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';
                return true;
            }

            else
            {

                //If the file is not empty,return false
                DateTime currentDateTime = DateTime.Now;
                //string Position_SpaceR = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation  +"," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

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
            string Endata1_Bird = "session,datetime,device,assessment,starttime,stoptime,gamename,datafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
            File.WriteAllText(filepath_Endata1_Bird, Endata1_Bird);
            DateTime currentDateTime = DateTime.Now;
            //string Position = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';
            PositionBird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation  +"," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

            File.AppendAllText(filepath_Endata1_Bird, PositionBird);
            return true;
        }
    }







    // Update is called once per frame
    void Update()
    {
       
        LevelText.text = "Level: " + auto_speed * (-0.5);

        UpdateGameDurationUI();

        column_position_flag_topass = column_position_flag;
         

        //uses the p button to pause and unpause the game
        if ((Input.GetKeyDown(KeyCode.P)))
        {
            if (!gameOver)
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                    showPaused();
                }
                else if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    hidePaused();
                }
            }
            else if (gameOver)
            {
                hidePaused();
                playAgain();
                // h.Stop_data_log();
            }
        }


        if (!gameOver && Time.timeScale == 1)
        {
            //AppData.sessionDuration += Time.deltaTime;
            //AppData.timeOnTrail += Time.deltaTime;
            gameduration -= Time.deltaTime;
        }


        int control = BirdControl.hit_count;
        //birdTime = DateTime.Now.ToString(" HH:mm:ss.fff");

        if (control == 3)
        {
            end_time = DateTime.Now.ToString("HH:mm:ss.fff");
        }
        else
        {
            end_time = DateTime.Now.ToString("HH:mm:ss.fff");
        }
        

    }


    void UpdateGameDurationUI()
    {
        //// timerObject.specifiedValue = Mathf.Clamp(100 * (90 - gameduration) / 90f, 0, 100); 

    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        // AppData.plutoData.desTorq = 0;
        //SendToRobot.ControlParam(AppData.plutoData.mechs[AppData.plutoData.mechIndex], ControlType.TORQUE, true, false);
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {

        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
    public void BirdDied()
    {

       // string end_time = DateTime.Now.ToString("HH:mm:ss.fff");
        GameOverText.SetActive(true);
        gameOver = true;
        
    }

    public void Birdalive()
    {
        CongratulationsText.SetActive(true);
        gameOver = true;
       
    }

    public void BirdScored()
    {
        if (!bc.startBlinking)
        {
            score += 1;

        }


        ScoreText.text = "Score: " + score.ToString();
        // Debug.Log(ScoreText.text);
        FlappyColumnPool.instance.spawnColumn();


    }


    public void RandomAngle()
    {
        ypos = UnityEngine.Random.Range(-3f, 5.5f);
    }

    public void playAgain()
    {
        if (gameOver == true)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (!gameOver)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }

        }

    }
    public void PlayStart()
    {
        endValSet = false;
        start.SetActive(false);
        Time.timeScale = 1;
    }

    public void continueButton()
    {

        StartGame();
        GameOverText.SetActive(false);
        CongratulationsText.SetActive(false);
        gameOver = false;


    }

    public void ShowGameMenu()
    {

        menuCanvas.SetActive(true);
        Canvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        gameOver = false;
        start_time = DateTime.Now.ToString("HH:mm:ss.fff");
       
        Canvas.SetActive(true);
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
        timecoRoutine = SpawnTimer();
        duration = 60;
        StartCoroutine(timecoRoutine);
        //FlappyData();



        // var lines = File.ReadAllLines(@"C:\Users\BioRehab\Desktop\Sathya\unity-pose\Arebo demo\Data\"+p_hospno+"\\"+"flappy_hits.csv");
        var lines = File.ReadAllLines(path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno + "\\" + "flappy_hits.csv");

        var count = lines.Length;
        List<string> second_row = new List<string>();
        List<string> fifth_row = new List<string>();
        List<string> sixth_row = new List<string>();
        if (count > 1)
        {
            foreach (var item in lines)
            {
                var rowItems = item.Split(',');
                second_row.Add(rowItems[2]);
                fifth_row.Add(rowItems[5]);
                sixth_row.Add(rowItems[6]);
            }

            auto_speed = float.Parse(second_row[count - 1]);
            if (auto_speed < 0)
            {
                start_speed = auto_speed;
            }
            else
            {
                auto_speed = -2.0f;
                start_speed = auto_speed;
            }



        }
        else
        {
            auto_speed = -2.0f;
            start_speed = auto_speed;
        }


    }

    private IEnumerator SpawnTimer()
    {
        while (!gameOver)
        {

            duration = duration - 1;
            UpdateDuration();

            if (duration == 0)
            {
                gameOver = true;
                duration = 60;
                Birdalive();
                total_life = total_life - 1;
                if (total_life < 0)
                {
                    auto_speed = auto_speed - 2.0f;
                }



            }

            yield return new WaitForSeconds(1);

        }

    }

    void UpdateDuration()
    {
        durationText.text = "Duration: " + duration;

    }

   

    public void continue_pressed()
    {
        //StartGame();
        //GameOverText.SetActive(false);
        //CongratulationsText.SetActive(false);
        //gameOver = false;
    }

    public void quit_pressed()
    {

        SceneManager.LoadScene("New Scene");
    }

    public void OnApplicationQuit()
    {

    }

}





















