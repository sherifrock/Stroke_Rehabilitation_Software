using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;



public class Flappydrawpath : MonoBehaviour
{
    public static Flappydrawpath instance;
    public float[] Encoder = new float[6];
    public float[] ELValue = new float[6];
    public float max_x;
    public float min_x;
    public float max_y;
    public float min_y;

    List<Vector3> paths;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        paths = new List<Vector3>();
        

    }


    //public void onclick_game()
    //{
    //    SceneManager.LoadScene("SpaceShooterDemo");

    //    // Debug.Log(xxx.Count);
    //    // Debug.Log(xxx.Last()+" :Last");
    //    // Debug.Log(xxx.First()+" :First");

    //   // paths = Drawlines.paths_pass;

    //    max_x = paths.Max(v => v.x);
    //    min_x = paths.Min(v => v.x);
    //    max_y = paths.Max(v => v.y);
    //    min_y = paths.Min(v => v.y);
    //    //Debug.Log(max_x + ......+ min_x + ...... +)
    //}
    //public void onclickStart()
    //{
    //    paths = Drawlines.paths_pass;

    //    max_x = paths.Max(v => v.x);
    //    min_x = paths.Min(v => v.x);
    //    max_y = paths.Max(v => v.y);
    //    min_y = paths.Min(v => v.y);
    //}
    public void Update()
    {
        // SensorValue();
        // System.DateTime currentTime = System.DateTime.Now;
    }
    //public void SensorValue()
    //{

    //    ELValue[0] = AppData.plutoData.enc1;
    //    ELValue[1] = AppData.plutoData.enc2;
    //    ELValue[2] = AppData.plutoData.enc3;
    //    ELValue[3] = AppData.plutoData.enc4;
    //    ELValue[4] = AppData.plutoData.torque1;
    //    ELValue[5] = AppData.plutoData.torque3;
    //    Encoder = new float[] { ELValue[0], ELValue[1], ELValue[2], ELValue[3], ELValue[4], ELValue[5] };


    //    string DataPath = Application.dataPath;
    //    Directory.CreateDirectory(DataPath + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
    //    string filepath_Endata = DataPath + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "EL Value-Full Weight Support.csv";
    //    if (IsCSVEmpty(filepath_Endata))
    //    {

    //    }
    //    else
    //    {

    //    }
    //}

    //private bool IsCSVEmpty(string filepath_Endata)
    //{
    //    if (File.Exists(filepath_Endata))
    //    {
    //        //check the file is empty,write header
    //        if (new FileInfo(filepath_Endata).Length == 0)
    //        {
    //            string Endata = "Time,Encoder1,Encoder2,Encoder3,Encoder4,Loadcell 1,Loadcell 2\n";
    //            File.WriteAllText(filepath_Endata, Endata);
    //            DateTime currentDateTime = DateTime.Now;
    //            string Position = currentDateTime + "," + Encoder[0] + "," + Encoder[1] + "," + Encoder[2] + "," + Encoder[3] + "," + Encoder[4] + "," + Encoder[5] + '\n';
    //            return true;
    //        }
    //        else
    //        {
    //            //If the file is not empty,return false
    //            DateTime currentDateTime = DateTime.Now;
    //            string Position = currentDateTime + "," + Encoder[0] + "," + Encoder[1] + "," + Encoder[2] + "," + Encoder[3] + "," + Encoder[4] + "," + Encoder[5] + '\n';
    //            File.AppendAllText(filepath_Endata, Position);
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        //If the file doesnt exist
    //        string DataPath = Application.dataPath;
    //        directory.createdirectory(datapath + "\\" + "patient_data" + "\\" + welcome.p_hospno);
    //        string filepath_endata1 = datapath + "\\" + "patient_data" + "\\" + welcome.p_hospno + "\\" + "el value-full weight support.csv";
    //        string endata1 = "time,encoder1,encoder2,encoder3,encoder4,loadcell 1,loadcell 2\n";
    //        File.WriteAllText(filepath_Endata1, Endata1);
    //        DateTime currentDateTime = DateTime.Now;
    //        string Position = currentDateTime + "," + Encoder[0] + "," + Encoder[1] + "," + Encoder[2] + "," + Encoder[3] + "," + Encoder[4] + "," + Encoder[5] + '\n';
    //        File.AppendAllText(filepath_Endata1, Position);
    //        return true;
    //    }

    //}


    //public void onclick_recalibrate()
    //{
    //    SceneManager.LoadScene("Drawpath");

    //}
    public void onclickHalfgame()
    {
        //SceneManager.LoadScene("SpaceShooterDemo");
        //paths = FlappyRecalibrate.paths_pass;

        SceneManager.LoadScene("FlappyGame");

        paths = new List<Vector3>();
        paths = FlappyCalibrate.paths_pass;

        max_x = paths.Max(v => v.x);
        min_x = paths.Min(v => v.x);
        max_y = paths.Max(v => v.y);
        min_y = paths.Min(v => v.y);

        Debug.Log(max_x);
        Debug.Log(min_x);
        Debug.Log(max_y);
        Debug.Log(min_y);
        Debug.Log("autoRotationControl");
    }

}


