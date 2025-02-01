




using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System;
using static BirdControl;
using System.Collections;

public class Welcome : MonoBehaviour
{
    public InputField hospno;
    public static string p_hospno;
    public static string newDirPath;
    public static string finalpath;
    public static string patientDir;
    public static string gamedata;
    public static string sessionfile;
    public Text messageText;


    //private const string SessionFilePath = "session_count.txt";

    void Start()
    {

        
    
       

       
            
    }

    void Update()
    {
    }

    public void signup()
    {
        SceneManager.LoadScene("Register");
    }

    public void onCLickQuit()
    {
        Application.Quit();
    }


    private IEnumerator ShowMessageFor3Seconds(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        messageText.gameObject.SetActive(false);
    }

    public void login()
    {
        p_hospno = hospno.text;
        bool hospno_check = string.IsNullOrEmpty(p_hospno);
        if (hospno_check == true)
        {
            Debug.Log("Empty hospno");
            StartCoroutine(ShowMessageFor3Seconds("EMPTY HOSPITAL NUMBER"));
        }
        else
        {
            string path_to_data = Application.dataPath;

            if (!Directory.Exists(path_to_data + "\\" + p_hospno))
            {
                string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno;
                circleclass.circlePath = patientDir;
                if (Directory.Exists(patientDir))
                {
                    string patientJson = File.ReadAllText(patientDir + "\\patient.json");

                   

                    var patient = JsonConvert.DeserializeObject<patient>(patientJson);
                    Staticvlass.CrossSceneInformation = patient.name + "," + patient.hospno;






                    string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy");
                    string newDirPath = Path.Combine(patientDir, dateTimeNow);

                    if (Directory.Exists(newDirPath))
                    {
                        Staticvlass.FolderPath = newDirPath;
                    }
                    else
                    {
                        Directory.CreateDirectory(newDirPath);
                        Staticvlass.FolderPath = newDirPath;
                    }


                    
                   

                    SceneManager.LoadScene("New Scene");

                    
                    
                      
                    





                }
                else
                {
                    Debug.Log("Hospital Number Does not exist");
                    StartCoroutine(ShowMessageFor3Seconds("HOSPITAL NUMBER DOES NOT EXIST "));
                    //StartCoroutine(ShowMessageFor3Seconds("PLEASE ENTER SIGN UP AND REGISTER"));
                }
            }




        }
       

    }
}









public static class circleclass
{
    public static string circlePath;
    public static string sessionpath;

    // public static string CrossSceneInformation;
}
