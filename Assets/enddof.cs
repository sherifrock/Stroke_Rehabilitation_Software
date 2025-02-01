using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime;
using System.IO;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Dof : MonoBehaviour

{

    private Vector3 originalPosition;
    private bool isCentered = false;

    public static Dof instance;
    public static JediSerialCom serReader;
    public static float x;
    public static float y;
    public static float x1;
    public static float y1;
    public static float movement;

    public static float theta1;
    public static float theta2;



    public float l1 = 333;
    public float l2 = 381;

    [SerializeField]
    public Text textResults;




    public static string Date;
    public static string name_sub_;
    public static string date_;
    public static string Path;
    public static string filePath;
    public static string folderpath;
    public static float timer;
    public string[] portnames;
    public static string savepath;
    public InputField Hos_;
    // public int condition;


    // public int MovingAverageLength = 5;
    // private int count;
    // private float movingAverage;





    public static float timer_;

    void Start()
    {
        JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        serReader = new JediSerialCom("COM12");
        serReader.ConnectToArduino();

        Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");


    }

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        timer_ += Time.deltaTime;




        if ((JediSerialPayload.data.Count == 2))
        {


            try
            {

                // torque = (float.Parse(JediSerialPayload.data[0].ToString()));
                theta1 = (float.Parse(JediSerialPayload.data[0].ToString()));
                theta2 = (float.Parse(JediSerialPayload.data[1].ToString()));
                // error = (float.Parse(JediSerialPayload.data[3].ToString()));

            }



            catch (System.Exception)
            {

            }

            //Debug.Log(theta1);
            //Debug.Log(theta2);

            //Debug.Log(X);
            //Debug.Log(Y);

        }

        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);

        //x = ((x1) / (333 + 381) * 7.5f);
        //y = ((y1 + 350) / (400 * 2)) * 4.8f;


        y = ((x1) / (333 + 381)) * 4.8f;
        x = ((y1 + 350) / (400 * 2)) * 7.5f;











    }






    public void DconnectToArduino()
    {
        serReader.DisconnectArduino();
    }










}







