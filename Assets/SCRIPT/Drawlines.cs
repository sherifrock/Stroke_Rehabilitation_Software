


using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.Rendering.Universal;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class Drawlines : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Done_Boundary boundary;
    public static Drawlines instance;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float nextFire;

    public float[] angXRange = { -40, 40 };
    public float[] angZRange = { -40, -120 };

    public float fireStopClock;
    public float singleFireTime;

    float max_x_init;
    float min_x_init;
    float max_y_init;
    float min_y_init;
    float startWidth = 0.1f;
    float endWidth = 0.1f;
    double x_c;
    double y_c;

    double x_value;
    double y_value;
    double x_u;
    double y_u;
    public static float theta1;
    public static float theta2; 
    float l1 = 333;
    float l2 = 381;

    public static LineRenderer lr;
    public static List<Vector3> paths_draw;
    public static List<Vector3> paths_pass;
    private string CalliPath;
    public float[] Encoder = new float[6];
    public float[] ELValue = new float[6];
    public static string start_time;
    public static string end_time;
    public static string datetime;
    public static string relativePath;
    public static string stopTime;

    public static class calliclass
    {
        public static string calli;

    }


    void Start()
    {

       


        max_x_init = 591;
        min_x_init = -91;
        max_y_init = 575;
        min_y_init = -75;



        
        paths_draw = new List<Vector3>();
        paths_pass = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;

        x_c = (max_x_init + min_x_init) / 2;
        y_c = (max_y_init + min_y_init) / 2;



        // Initialize file paths
        string welcompath = Staticvlass.FolderPath;
        string callifile = Path.Combine(welcompath, "calibration");
        if (!Directory.Exists(callifile))
        {
            Directory.CreateDirectory(callifile);
        }
        CalliPath = Path.Combine(callifile, "calibration_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        calliclass.calli = CalliPath;
        string fullFilePath = calliclass.calli;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        relativePath = Path.GetRelativePath(partOfPath, fullFilePath);



        start_time = DateTime.Now.ToString("HH:mm:ss.fff");
        datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");

        // Ensure the file is ready for writing
        PrepareCSVFile(CalliPath, "Time,Encoder1,Encoder2\n");

        //CalliData();
       
    }

    private void PrepareCSVFile(string path, string header)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, header);
        }
    }

    private void OnDisable()
    {
        CalliData();
    }


    public void CalliData()
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
        string assessment = "1";
        string starttime = start_time;

         string endtime = end_time;


        //string endtime = end_time;
        string gamename = "CALIBRATION";
        string datalocation = relativePath;
        string devicesetup = "null";
        string assistmode = "null";
        string assistmodeparameter = "null";
        string gameparameter= "null"; 






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

    void Update()
    {
        Time.timeScale = 1;


       
        SensorValue();
        end_time = DateTime.Now.ToString("HH:mm:ss.fff");


        // Debug.Log(AppData.plutoData.motorCurrent1 == 0 ? "pREESSED" : "nOtPressed");
    }

    private void SensorValue()
    {


        float l1 = 333;
        float l2 = 381;

        float theta1 = Drawlines.theta1;
        float theta2 = Drawlines.theta2;


        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);



        float x = ((x1) / (333 + 381) * 12f);
        float y = ((y1 + 350) / (400 * 2)) * 4.8f;
        //ELValue[0] = Dof.theta1;
        //ELValue[1] = Dof.theta2;


        ELValue[0] = x;
        ELValue[1] = y;


        Encoder = new float[] { theta1, theta2 };

        string data = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff},{Encoder[0]},{Encoder[1]}\n";
        File.AppendAllText(CalliPath, data);
        
    }

    void FixedUpdate()
    {

        float theta1 = Dof.theta1;
        float theta2 = Dof.theta2;  


        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);

       

        float x = ((x1) / (333 + 381) * 12f);
        float y = ((y1 + 350) / (400 * 2)) * 4.8f;


       




        //Debug.Log("xaxis" + "" + x + "y" + "   " + y);
        //Debug.Log("thet1" + "" + thetaa + "theta2" + "   " + thetab);







        // Update the position values
        x_value = x;
        y_value = y;

        

        //Adjust the coordinate transformation
         x_u = ((x_value / (max_x_init - min_x_init)) * 690f); // Adjust the x transformation
        y_u = ((y_value / (max_y_init - min_y_init)) * 650f);  // Adjust the y transformation


        Debug.Log("x:" + x_u);
        Debug.Log("y:" + y_u);

        // Create the draw and pass vectors
        Vector3 to_draw_values = new Vector3((float)x_u, (float)y_u, 0.0f);
        Vector3 to_pass_values = new Vector3((float)x_value, (float)y_value, 0.0f);

        // Add the vectors to the lists
        paths_draw.Add(to_draw_values);
        paths_pass.Add(to_pass_values);

        // Update the LineRenderer
        lr.positionCount = paths_draw.Count;
        lr.SetPositions(paths_draw.ToArray());
        lr.useWorldSpace = true;

        

    }

   
}





