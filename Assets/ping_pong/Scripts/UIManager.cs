









using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;

public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects, finishObjects;
    public BoundController rightBound;
    public BoundController leftBound;
    public bool isFinished;
    public bool playerWon, enemyWon;
    public AudioClip[] audioClips; // winlevel loose
    public int winScore = 7;
    public int win;
    private PongPlayerController playerController;
    public static string start_time;
    public static string end_time;
    public static string gamedata;
    public static string startdatetime;
    public static string endtime;
    public static string gameend_time;
    public static string datetime;

    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");
        hideFinished();
        Time.timeScale = 1; // Ensure the game starts unpaused
        playerController = FindObjectOfType<PongPlayerController>();
         startdatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
        start_time = DateTime.Now.ToString("HH:mm:ss.fff");
        datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");




    }

    void Update()
    {
        if (rightBound.enemyScore >= winScore && !isFinished)
        {
            HandleGameEnd(false);
        }
        else if (leftBound.playerScore >= winScore && !isFinished)
        {
            HandleGameEnd(true);
        }

        if (isFinished)
        {
            showFinished();
            if (Input.anyKeyDown)
            {

                gameend_time = DateTime.Now.ToString("HH:mm:ss tt");
                LoadScene("pong_menu");

            }
        }

        if (Input.GetKeyDown(KeyCode.P) && !isFinished)
        {
            pauseControl();
        }

        if (Time.timeScale == 0 && !isFinished)
        {
            foreach (GameObject g in pauseObjects)
            {
                if (g.name == "PauseText")
                {
                    g.SetActive(true);
                }
            }
        }
        else
        {
            foreach (GameObject g in pauseObjects)
            {
                if (g.name == "PauseText")
                {
                    g.SetActive(false);
                }
            }
        }






         gameend_time = DateTime.Now.ToString("HH:mm:ss.fff");

    }

    public static class gameclass
    {
        public static string gamePath;

        // public static string CrossSceneInformation;
    }
  

     public void AutoData()
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



        if (scoreclass.playerpoint == 5 || scoreclass.enemypoint == 5) // Example winning condition
        {
            // Set the gameWon flag to true
            endtime = end_time;
        }
        else
        {
            endtime = gameend_time;
        }

        //string endtime = end_time;
        string gamename = "PING-PONG";
        string datalocation = pongclass.relativepath;
        string devicesetup = "null";
        string assistmode="null";
        string assistmodeparameter =" null";
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

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter+","+gameparameter+'\n';
                return true;
            }

            else
            {

                //If the file is not empty,return false
                DateTime currentDateTime = DateTime.Now;
                //string Position_SpaceR = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter +","+gameparameter+ '\n';

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
            PositionBird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter +","+gameparameter+ '\n';

            File.AppendAllText(filepath_Endata1_Bird, PositionBird);
            return true;
        }
    }
    
    void HandleGameEnd(bool playerWon)
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        this.playerWon = playerWon;
        enemyWon = !playerWon;
        win = playerWon ? 1 : -1;
        isFinished = true;
        if (isFinished == true)
        {
            Time.timeScale = 0;
           
        }
        playAudio(playerWon ? 0 : 1);
        end_time = DateTime.Now.ToString("HH:mm:ss.fff");
    }

    public void LoadScene(string sceneName)
    {

       
        SceneManager.LoadScene(sceneName);
        AutoData();

    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void playAudio(int clipNumber)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioClips[clipNumber];
        audio.Play();
    }

    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
            playerController.PauseGame(); // Notify the player controller
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
            playerController.PauseGame(); // Notify the player controller
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void showFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    public void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }
}
