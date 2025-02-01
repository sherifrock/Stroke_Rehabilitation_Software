






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float movespeed = 5f;
    public float theta1;
    public float theta2;
    public float l1 = 333;
    public float l2 = 381;
    public static string Date;
    public static string filePath;
    public InputField Hos_;

    public static string csvFilePath;

    public float x2;
    public float y2;
    public static JediSerialCom serReader;

    float enc_1, enc_2;
    float Rob_X, Rob_Y;
    string TargetPosx, TargetPosy, CurrentStat, PlayerPosx, PlayerPosy, CirclePositionX, CirclePositionY, targetRadii, circleRadiusStr;

    public static float timer_;

    private string welcompath = Staticvlass.FolderPath;

    public Rigidbody2D _rb;

    void Start()
    {
        JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        serReader = new JediSerialCom("COM12");
        serReader.ConnectToArduino();

        string assessfile = Path.Combine(welcompath, "assessment_Data");
        if (!Directory.Exists(assessfile))
        {
            Directory.CreateDirectory(assessfile);
        }
        csvFilePath = Path.Combine(assessfile, "assessment_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");

        //filePath = Path.Combine(assessfile);
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        enc_1 = PlayerPrefs.GetFloat("Enc1");
        enc_2 = PlayerPrefs.GetFloat("Enc2");
        Rob_X = PlayerPrefs.GetFloat("Robx");
        Rob_Y = PlayerPrefs.GetFloat("Roby");

        TargetPosx = PlayerPrefs.GetString("targetPos1");
        TargetPosy = PlayerPrefs.GetString("targetPos2");
        CurrentStat = PlayerPrefs.GetString("Currentstat");

        PlayerPosx = PlayerPrefs.GetString("PlayerX");
        PlayerPosy = PlayerPrefs.GetString("PlayerY");

        CirclePositionX = PlayerPrefs.GetString("CirclePosX");
        CirclePositionY = PlayerPrefs.GetString("CirclePosY");
        targetRadii = PlayerPrefs.GetString("targetRadii");
        circleRadiusStr = PlayerPrefs.GetString("CircleRadius");

        timer_ += Time.deltaTime;

        if (JediSerialPayload.data.Count == 2)
        {
            try
            {
                theta1 = float.Parse(JediSerialPayload.data[0].ToString());
                theta2 = float.Parse(JediSerialPayload.data[1].ToString());
            }
            catch (Exception)
            {
                // Handle parsing exception
            }
        }

        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos(thetaa) * l1 + Mathf.Cos(thetaa + thetab) * l2);
        float x1 = -(Mathf.Sin(thetaa) * l1 + Mathf.Sin(thetaa + thetab) * l2);

        x2 = (x1 / (l1 + l2)) * 7.5f;
        y2 = ((y1 + 350) / (400 * 2)) * 4.8f;
        transform.position = new Vector3(x2, y2, 0);
        transform.Translate(transform.position);
        PlayerPrefs.SetFloat("Enc1", theta1);
        PlayerPrefs.SetFloat("Enc2", theta2);
        PlayerPrefs.SetFloat("Robx", transform.position.x);
        PlayerPrefs.SetFloat("Roby", transform.position.y);

        //string playerName = PlayerPrefs.GetString("PlayerName");
        //if (!string.IsNullOrEmpty(playerName))
        //{
        //    string csvFilePath = Path.Combine(Application.dataPath, playerName, "robo_data.csv");
        //    SaveCSVData(csvFilePath);
        //}


        // string csvFilePath = Path.Combine(filePath, "robo_data.csv");
        SaveCSVData(csvFilePath);
    }

    //void OnPlayerNameEntered(string Name)
    //{
    //    PlayerPrefs.SetString("PlayerName", Name);
    //    string DataPath = Path.Combine(Application.dataPath, Name);
    //    Directory.CreateDirectory(DataPath);

    //    string csvFilePath = Path.Combine(DataPath, "robo_data.csv");
    //    SaveCSVData(csvFilePath);
    //}

    private bool SaveCSVData(string csvFilePath)
    {
        string header = "Time,enc_1,enc_2,Robx,Roby,PlayerX,PlayerY,CirclePosX,CirclePosY,circleRadius,TargetPos1,TargetPos2,targetRadii,CurrentStat\n";
        DateTime currentDateTime = DateTime.Now;
        string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{circleRadiusStr},{TargetPosx},{TargetPosy},{targetRadii},{CurrentStat}\n";

        if (!File.Exists(csvFilePath))
        {
            File.WriteAllText(csvFilePath, header);
        }

        File.AppendAllText(csvFilePath, data);
        return true;
    }
}
