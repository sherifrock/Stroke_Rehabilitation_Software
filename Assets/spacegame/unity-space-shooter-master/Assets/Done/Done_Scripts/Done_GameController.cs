using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class Done_GameController : MonoBehaviour
{
	public AudioClip[] sounds;
    private AudioSource source;
	
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text durationText;
	public Text LevelText;

	public GameObject imagelife1;
	public GameObject imagelife2;
	public GameObject imagelife3;
	
	private bool gameOver;
	private bool restart;
	private bool nextlevel;
	private int score;
	private int duration = 0;

	public float levelspeed;
	public float start_levelspeed;

	public GameObject GameOverCanvas;
	public GameObject CongratsCanvas;

	string p_hospno;
	int gameover_count = 0;
	int overall_life_count = 0;
	int hit_count = 0;
	int life_count_completed = 0;

	string start_time;
	string end_time;

	float timeToAppear = 1f;
	float timeWhenDisappear;

	IEnumerator timecoRoutine;
	IEnumerator wavecoRoutine;

	float player_level;
	int level_playing = 0;

    string path_to_data;
	public static string datetime;

    void Start ()
	{

        
        
            path_to_data = Application.dataPath;
           
        start_time = DateTime.Now.ToString("HH:mm:ss.fff");
        datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");



        p_hospno = Welcome.p_hospno;

            source = GetComponent<AudioSource>();
            GameOverCanvas.SetActive(false);
            CongratsCanvas.SetActive(false);

            levelspeed = 0.5f;
        start_levelspeed = levelspeed;

        player_level = (levelspeed - 0.5f) / 0.25f;
        level_playing = (int)(player_level + 1);

        gameOver = false;
            restart = false;
            nextlevel = false;
            restartText.text = "";
            gameOverText.text = "";
            duration = 60;
            score = 0;
            gameover_count = 0;
            overall_life_count = 0;
            hit_count = 0;
            life_count_completed = 0;

            imagelife1.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
            imagelife2.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
            imagelife3.GetComponent<Image>().color = new Color32(76, 192, 28, 200);

            timecoRoutine = SpawnTimer();
            StartCoroutine(timecoRoutine);
            UpdateLevel();
        score = 0;
        UpdateScore();
            wavecoRoutine = SpawnWaves();
            StartCoroutine(wavecoRoutine);
        


    }



    void Update()
    {
        Time.timeScale = levelspeed;

        if (gameOver)
        {
            restartText.text = "Press 'R' to Restart";
            GameOverCanvas.SetActive(true);
            Debug.Log("Game Over Triggered");

            // Ensure coroutines are stopped
            StopCoroutine(timecoRoutine);
            StopCoroutine(wavecoRoutine);
           
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (nextlevel)
        {
            durationText.text = "";
            LevelText.text = "";

            StopCoroutine(timecoRoutine);
            StopCoroutine(wavecoRoutine);
            CongratsCanvas.SetActive(true);

            if (life_count_completed > 2)
            {
                levelspeed += 0.25f;
                player_level = (levelspeed - 0.5f) / 0.25f;
                level_playing = (int)(player_level + 1);
            }

            imagelife1.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
            imagelife2.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
            imagelife3.GetComponent<Image>().color = new Color32(76, 192, 28, 200);

            gameover_count = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            levelspeed += 0.25f;
            player_level = (levelspeed - 0.5f) / 0.25f;
            level_playing = (int)(player_level + 1);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            levelspeed -= 0.25f;
            player_level = (levelspeed - 0.5f) / 0.25f;
            level_playing = (int)(player_level + 1);
        }


        if (gameOver)
        {

            end_time = DateTime.Now.ToString("HH:mm:ss.fff");
        }
        else
        {
            end_time = DateTime.Now.ToString("HH:mm:ss.fff");

        }
        
       

    }




    public void StartGame()
    {
        // Reset necessary game state variables
        gameOver = false;
        restart = false;
        nextlevel = false;
        gameover_count = 0;
        overall_life_count = 0;
        hit_count = 0;
        life_count_completed = 0;
        levelspeed = 0.5f;

        duration = 60;
        score = 0;

        imagelife1.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
        imagelife2.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
        imagelife3.GetComponent<Image>().color = new Color32(76, 192, 28, 200);

        StartCoroutine(timecoRoutine);
        StartCoroutine(wavecoRoutine);

        UpdateLevel();
        UpdateScore();

      


    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SpaceShooterDemo")
        {

            StartGame();
            AutoData();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
       
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }

    void OnDestroy()
    {

        // StartNewGameSession();
        AutoData();
    }



    public void OnApplicationQuit()
    {
		end_time = DateTime.Now.ToString("HH:mm:ss tt");

        //AutoData();
        //string duration_played = (DateTime.Parse(end_time)-DateTime.Parse(start_time)).ToString();
        //string newFileName = @"C:\Users\ezhil\Myprojectspace\Assets\Patient_Data" + p_hospno+"\\"+"hits.csv";
        //System.DateTime today = System.DateTime.Today;
        //string data_csv = today.ToString()+ "," + start_levelspeed + "," + levelspeed + "," + start_time + "," + end_time + "," + duration_played + "," + hit_count + "\n";
        //File.AppendAllText(newFileName,data_csv);
       // Application.Quit();
		
    }


	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (!gameOver)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [UnityEngine.Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (UnityEngine.Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			// if (gameOver)
			// {
			// 	restartText.text = "Press 'R' for Restart";
			// 	// restart = true;
			// 	break;
			// }
		}
	}

	private IEnumerator SpawnTimer() {
		while (!gameOver){

			duration = duration-1;
			UpdateDuration ();
			UpdateLevel ();

			if(duration == 0){
				life_count_completed = life_count_completed + 1;
				
				nextlevel = true;
				source.clip = sounds[0];
				source.PlayOneShot(source.clip);
				
				
				
			}

			if(duration == 40){
				source.clip = sounds[1];
            	source.PlayOneShot(source.clip);
			}

			if(duration == 20){
				source.clip = sounds[2];
            	source.PlayOneShot(source.clip);
			}

			yield return new WaitForSeconds(levelspeed);
			// yield return new WaitForSeconds(1.0f/levelspeed);
			
		}	

	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	void UpdateDuration ()
	{
		if (!nextlevel)
		{
			durationText.text = "Duration: " + duration;
		}
		
		// Debug.Log(duration+"..."+Time.time);
		
	}

	void UpdateLevel ()
	{
		LevelText.text = "Level: " + level_playing;
		// Debug.Log(duration+"..."+Time.time);
		
	}
	
	public void GameOver ()
	{
		source.clip = sounds[3];
        source.PlayOneShot(source.clip);
		//duration = 60;

		hit_count = hit_count+1;
		
		gameover_count = gameover_count+1;
		if(gameover_count==1)
		{
			// imagelife1.SetActive(false);
			imagelife1.GetComponent<Image>().color = new Color32(255,0,0,200);
		}
		if(gameover_count==2)
		{
			// imagelife2.SetActive(false);
			imagelife2.GetComponent<Image>().color = new Color32(255,0,0,200);
		}
		if(gameover_count==3)
		{
			// imagelife3.SetActive(false);
			imagelife3.GetComponent<Image>().color = new Color32(255,0,0,200);
			// gameOverText.text = "Game Over";
			// timeWhenDisappear = Time.time + timeToAppear;
			StopCoroutine(timecoRoutine);
			StopCoroutine (wavecoRoutine);
			GameOverCanvas.SetActive(true);
            end_time = DateTime.Now.ToString("HH:mm:ss.fff");
           
        }
        if (gameover_count>2)
		{
			overall_life_count = overall_life_count+1;
			gameover_count = 0;
			
		}
		if(overall_life_count>2)
		{
			levelspeed = levelspeed-0.25f;
			overall_life_count = 0;
			// Debug.Log(levelspeed+" : levelspeed");
			player_level = (levelspeed-0.5f)/0.25f;
			level_playing = (int)(player_level+1);
		}
		
	}

	public void onclick_exit()
	{
		end_time = DateTime.Now.ToString("HH:mm:ss tt");
       
        //string duration_played = (DateTime.Parse(end_time) - DateTime.Parse(start_time)).ToString();
        //string newFileName = @"C:\Users\ezhil\Myprojectspace\Assets\Patient_Data" + p_hospno + "\\" + "hits.csv";
        //System.DateTime today = System.DateTime.Today;
        //string data_csv = today.ToString() + "," + start_levelspeed + "," + levelspeed + "," + start_time + "," + end_time + "," + duration_played + "," + hit_count + "\n";
        //File.AppendAllText(newFileName, data_csv);
        //OnApplicationQuit();
        SceneManager.LoadScene("New Scene");
		
	}

	public void onclick_replaygame()
	{
		//end_time = DateTime.Now.ToString("HH:mm:ss tt");
		string duration_played = (DateTime.Parse(end_time)-DateTime.Parse(start_time)).ToString();
		string newFileName = @"D:\AREBO\Unity\MARS demo3\Data\" + p_hospno+"\\"+"hits.csv";
		System.DateTime today = System.DateTime.Today;
		string data_csv = today.ToString()+ "," + start_levelspeed + "," + levelspeed + "," + start_time + "," + end_time + "," + duration_played + "," + hit_count + "\n";
		File.AppendAllText(newFileName,data_csv);

		GameOverCanvas.SetActive(false);

		start_time = DateTime.Now.ToString("HH:mm:ss tt");

		imagelife1.GetComponent<Image>().color = new Color32(76,192,28,200);
		imagelife2.GetComponent<Image>().color = new Color32(76,192,28,200);
		imagelife3.GetComponent<Image>().color = new Color32(76,192,28,200);

		StartCoroutine(timecoRoutine);
		StartCoroutine (wavecoRoutine);
	}

	public void onclick_nextlevel()
	{
		nextlevel = false;
		CongratsCanvas.SetActive(false);
        StartCoroutine(timecoRoutine);
        StartCoroutine(wavecoRoutine);
        duration = 60;
		levelspeed = levelspeed+0.25f;
		player_level = (levelspeed-0.5f)/0.25f;
		level_playing = (int)(player_level+1);
	}

	public void onclick_previouslevel()
	{
		nextlevel = false;
		CongratsCanvas.SetActive(false);
        StartCoroutine(timecoRoutine);
        StartCoroutine(wavecoRoutine);
        duration = 60;
		// levelspeed = levelspeed-0.3f;
		levelspeed = 0.25f;
		player_level = (levelspeed-0.5f)/0.25f;
		level_playing = (int)(player_level+1);
	}

	public void onclick_replay()
	{
		nextlevel = false;
		CongratsCanvas.SetActive(false);
		duration = 60;
	}

    public void onclick_game()
    {
        SceneManager.LoadScene("SpaceShooterDemo");
        //end_time = DateTime.Now.ToString("HH:mm:ss.fff");
        //AutoData();
    }

    public void doquit()
	{
		// Debug.Log("Quit");
		//SceneManager.LoadScene("Feedback");
       
        Debug.Log("Quit");
		//OnApplicationQuit();
        SceneManager.LoadScene("New Scene");
        // Application.Quit();
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

        string endtime= end_time;

      

        //string endtime = end_time;
        string gamename = "SPACESHOOTER";
        string datalocation =   spaceclass.relativepath;
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
}