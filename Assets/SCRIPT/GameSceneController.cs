using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;



using System.IO;
using UnityEngine.SceneManagement;

using Newtonsoft.Json;
using System;



public class GameSceneController : MonoBehaviour
{
    public TMP_Text patientNameText;
    public TMP_Text patientHospNoText;

    public Text messageText;

    void Start()
    {
        string[] patientInfo = Staticvlass.CrossSceneInformation.Split(',');
        if (patientInfo.Length == 2)
        {
            //patientNameText.text = "Name: " + patientInfo[0];
            //patientHospNoText.text = "Hospital No: " + patientInfo[1];
            patientNameText.text =  patientInfo[0];
            patientHospNoText.text =   patientInfo[1];
        }


       // StartCoroutine(ShowMessageFor3Seconds("FIRST CLICK THE CALIBRATION PROCESS AFTER THE GAME CLICK  ---->"));


    }


    private IEnumerator ShowMessageFor3Seconds(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        messageText.gameObject.SetActive(false);
    }


}


