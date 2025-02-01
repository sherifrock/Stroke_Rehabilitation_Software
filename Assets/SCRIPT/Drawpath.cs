//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using UnityEngine.SceneManagement;
//using System.Linq;
//using System;
//using static PongPlayerController;
////using Unity.Android.Types;

//public class Drawpath : MonoBehaviour
//{ 
//    public static Drawpath instance;
//    public float[] Encoder = new float[6];
//    public float[] ELValue = new float[6];
//    public float max_x;
//    public float min_x;
//    public float max_y;
//    public float min_y;
//    int tocarry;
//    int Asses;
//    private  string CalliPath;

//    string welcompath = Staticvlass.FolderPath;
//    List<Vector3> paths;

//    void Awake()
//    {
//      instance = this;
//    }


//    public static class calliclass
//    {

//        public static string callipath;
//    }

//    void Start()
//    {
//        paths = new List<Vector3>();
//        //string DataPath = Staticvlass.FolderPath;
//        string callifile = welcompath + "\\" +"calibration";

//       // string callifile = 
//        if (Directory.Exists(callifile))
//        {
//             CalliPath = Path.Combine(callifile, "calibration_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
//        }
//        else
//        {
//            Directory.CreateDirectory(callifile);
//            CalliPath = Path.Combine(callifile, "callibration_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
//        }
//        calliclass.callipath =  CalliPath;
//    }


//    //public void onclick_game()
//    //{
//    //    SceneManager.LoadScene("SpaceShooterDemo");

//    //    // Debug.Log(xxx.Count);
//    //    // Debug.Log(xxx.Last()+" :Last");
//    //    // Debug.Log(xxx.First()+" :First");

//    //   // paths = Drawlines.paths_pass;

//    //    max_x = paths.Max(v => v.x);
//    //    min_x = paths.Min(v => v.x);
//    //    max_y = paths.Max(v => v.y);
//    //    min_y = paths.Min(v => v.y);
//    //    //Debug.Log(max_x + ......+ min_x + ...... +)
//    //}
//    //public void onclickStart()
//    //{
//    //    paths = Drawlines.paths_pass;

//    //    max_x = paths.Max(v => v.x);
//    //    min_x = paths.Min(v => v.x);
//    //    max_y = paths.Max(v => v.y);
//    //    min_y = paths.Min(v => v.y);
//    //}
//    public void Update()
//    {
//        SensorValue();
//        System.DateTime currentTime = System.DateTime.Now;
//    }
//    public void SensorValue()
//    {

//        ELValue[0] = Dof.theta1;
//        ELValue[1] = Dof.theta2;
//        //ELValue[2] = AppData.plutoData.enc3;
//        //ELValue[3] = AppData.plutoData.enc4;
//        //ELValue[4] = AppData.plutoData.torque1;
//        //ELValue[5] = AppData.plutoData.torque3;
//        //  Encoder = new float[] { ELValue[0], ELValue[1], ELValue[2], ELValue[3], ELValue[4], ELValue[5] };

//        Encoder = new float[] { ELValue[0], ELValue[1] };

//        //string DataPath = Application.dataPath;
//        //Directory.CreateDirectory(DataPath + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
//        // string filepath_Endata = DataPath + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "calibration_data.csv";
//        string filepath_Endata =calliclass.callipath;


//        if (IsCSVEmpty(filepath_Endata))
//        {

//        }
//        else
//        {

//        }
//    }

//    private bool IsCSVEmpty(string filepath_Endata)
//    {
//        if (File.Exists(filepath_Endata))
//        {
//            //check the file is empty,write header
//            if (new FileInfo(filepath_Endata).Length == 0)
//            {
//                string Endata = "Time,Encoder1,Encoder2\n";
//                File.WriteAllText(filepath_Endata, Endata);
//                DateTime currentDateTime = DateTime.Now;
//                string Position = currentDateTime + "," + Encoder[0] + "," + Encoder[1] + "," +  '\n';
//                return true;
//            }
//            else
//            {
//                //If the file is not empty,return false
//                DateTime currentDateTime = DateTime.Now;
//                string Position = currentDateTime + "," + Encoder[0] + "," + Encoder[1] + "," + '\n';
//                File.AppendAllText(filepath_Endata, Position);
//                return false;
//            }
//        }
//        else
//        {
//        //    //If the file doesnt exist
//        //    string DataPath = Application.dataPath;
//        //    Directory.CreateDirectory(DataPath + "\\" + "patient_data" + "\\" + Welcome.p_hospno);
//        //    string filepath_endata1 = DataPath + "\\" + "patient_data" + "\\" + Welcome.p_hospno + "\\" + "calibration_data.csv";
//        //    string endata1 = "time,encoder1,encoder2\n";
//        //    File.WriteAllText(filepath_endata1, endata1);
//        //    DateTime currentDateTime = DateTime.Now;
//        //    string Position = currentDateTime + "," + Encoder[0] + "," + Encoder[1] + "," + '\n';
//        //    File.AppendAllText(filepath_endata1, Position);
//           return true;
//        }

//    }

//    public void onclick_assessment()
//    {
//        SceneManager.LoadScene("Assessment");
//    }
//    public void onclick_SpaceShooter()
//    {
//        tocarry = 1;
//    }

//    public void onclick_Auto()
//    {
//        tocarry = 2;
//    }

//    public void onclick_Pingpong()
//    {
//        tocarry = 3;
//    }


//    //public void onclick_drawpath()
//    //{
//    //    tocarry = 4;
//    //}
//    public void onclick_game()
//    {



//        if (tocarry == 1)
//        {
//            SceneManager.LoadScene("SpaceShooterDemo");
//            paths = Drawlines.paths_pass;

//            max_x = paths.Max(v => v.x);
//            min_x = paths.Min(v => v.x);
//            max_y = paths.Max(v => v.y);
//            min_y = paths.Min(v => v.y);




//            // max_x = Drawpath.instance.max_x;
//            //min_x = Drawpath.instance.min_x;
//            //max_y = Drawpath.instance.max_y;
//            //min_y = Drawpath.instance.min_y;

//        }

//        else if (tocarry == 2)
//        {
//            SceneManager.LoadScene("FlappyGame");

//            paths = new List<Vector3>();
//            //paths = FlappyCalibrate.paths_pass;

//            //max_x = paths.Max(v => v.x);
//            //min_x = paths.Min(v => v.x);
//            //max_y = paths.Max(v => v.y);
//            //min_y = paths.Min(v => v.y);
//            paths = Drawlines.paths_pass;

//            max_y = paths.Max(v => v.y);
//            min_y = paths.Min(v => v.y);
//            PlayerPrefs.SetFloat("y max", max_y);
//            PlayerPrefs.SetFloat("y min", min_y);

//        }
//        else if (tocarry == 3)
//        {


//            SceneManager.LoadScene("pong_menu");

//            //    // Debug.Log(xxx.Count);
//            //    // Debug.Log(xxx.Last()+" :Last");
//            //    // Debug.Log(xxx.First()+" :First");

//            paths = Drawlines.paths_pass;

//            max_y = paths.Max(v => v.y);
//            min_y = paths.Min(v => v.y);
//            PlayerPrefs.SetFloat("y max", max_y);
//            PlayerPrefs.SetFloat("y min", min_y);


//        }

//        else if (tocarry == 4)
//        {
//            SceneManager.LoadScene("DrawPath");


//        }

//    }
//    //public void onclickGripStrength()
//    //{
//    //    Asses = 4;
//    //}
//    //public void onclickRoM()
//    //{
//    //    Asses = 5;
//    //}
//    //public void onclickassessment()
//    //{
//    //    if (Asses == 4)
//    //    {
//    //        SceneManager.LoadScene("Full Weight Grip Strength");
//    //    }

//    //    else if (Asses == 5)
//    //    {
//    //        SceneManager.LoadScene("DrawPath");
//    //    }

//    //}

//    public void onclick_drawpath()
//    {
//        SceneManager.LoadScene("DrawPath");
//    }
//    public void QuitGame()
//    {
//        Application.Quit();
//    }


//    public void onclick_gamescene()
//    {
//        SceneManager.LoadScene("New Scene");
//    }

//    public void onclick_recalibrate()
//    {
//        SceneManager.LoadScene("Drawpath");

//    }

//    public void onclick_login()
//    {
//        SceneManager.LoadScene("Welcome");

//    }
//}











using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;



public class Drawpath : MonoBehaviour
{
    public static Drawpath instance;
    public float[] Encoder = new float[6];
    public float[] ELValue = new float[6];
    public float max_x;
    public float min_x;
    public float max_y;
    public float min_y;
    public static string end_time;
   // public static Drawpath instance;
   // public float[] Encoder = new float[6];
   // public float[] ELValue = new float[6];
    //public float max_x;
    //public float min_x;
    //public float max_y;
    //public float min_y;
    int tocarry;
    int Asses;
    private string CalliPath;
    List<Vector3> paths;

    // private string CalliPath;


    public static class calliclass
        {

          public static string callipath;
        }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Initialize file paths
        //string welcompath = Staticvlass.FolderPath;
        //string callifile = Path.Combine(welcompath, "calibration");
        //if (!Directory.Exists(callifile))
        //{
        //    Directory.CreateDirectory(callifile);
        //}
        //CalliPath = Path.Combine(callifile, "calibration_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");

        //// Ensure the file is ready for writing
        //PrepareCSVFile(CalliPath, "Time,Encoder1,Encoder2\n");
       
    }

    void Update()
    {
        //SensorValue();
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

    private void PrepareCSVFile(string path, string header)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, header);
        }
    }

    //Rest of the class...
        public void onclick_assessment()
    {
        SceneManager.LoadScene("Assessment");
    }
    public void onclick_SpaceShooter()
    {
        tocarry = 1;
    }

    public void onclick_Auto()
    {
        tocarry = 2;
    }

    public void onclick_Pingpong()
    {
        tocarry = 3;
    }


    public void onclick_brikbreaker()
    {
        tocarry = 4;
    }
    public void onclick_game()
    {



        if (tocarry == 1)
        {
            SceneManager.LoadScene("SpaceShooterDemo");
            paths = Drawlines.paths_pass;

            max_x = paths.Max(v => v.x);
            min_x = paths.Min(v => v.x);
            max_y = paths.Max(v => v.y);
            min_y = paths.Min(v => v.y);




            // max_x = Drawpath.instance.max_x;
            //min_x = Drawpath.instance.min_x;
            //max_y = Drawpath.instance.max_y;
            //min_y = Drawpath.instance.min_y;

        }

        else if (tocarry == 2)
        {
            SceneManager.LoadScene("FlappyGame");

            paths = new List<Vector3>();
            //paths = FlappyCalibrate.paths_pass;

            //max_x = paths.Max(v => v.x);
            //min_x = paths.Min(v => v.x);
            //max_y = paths.Max(v => v.y);
            //min_y = paths.Min(v => v.y);
            paths = Drawlines.paths_pass;

            max_y = paths.Max(v => v.y);
            min_y = paths.Min(v => v.y);
            PlayerPrefs.SetFloat("y max", max_y);
            PlayerPrefs.SetFloat("y min", min_y);

        }
        else if (tocarry == 3)
        {


            SceneManager.LoadScene("pong_menu");

            //    // Debug.Log(xxx.Count);
            //    // Debug.Log(xxx.Last()+" :Last");
            //    // Debug.Log(xxx.First()+" :First");

            paths = Drawlines.paths_pass;

            max_y = paths.Max(v => v.y);
            min_y = paths.Min(v => v.y);
            PlayerPrefs.SetFloat("y max", max_y);
            PlayerPrefs.SetFloat("y min", min_y);


        }

        else if (tocarry == 4)
        {
            SceneManager.LoadScene("Level1");

            //    // Debug.Log(xxx.Count);
            //    // Debug.Log(xxx.Last()+" :Last");
            //    // Debug.Log(xxx.First()+" :First");

            paths = Drawlines.paths_pass;



            max_x = paths.Max(v => v.x);
            min_x = paths.Min(v => v.x);

            max_y = paths.Max(v => v.y);
            min_y = paths.Min(v => v.y);
            PlayerPrefs.SetFloat("x max", max_x);
            PlayerPrefs.SetFloat("x min", min_x);


        }

    }
    //public void onclickGripStrength()
    //{
    //    Asses = 4;
    //}
    //public void onclickRoM()
    //{
    //    Asses = 5;
    //}
    //public void onclickassessment()
    //{
    //    if (Asses == 4)
    //    {
    //        SceneManager.LoadScene("Full Weight Grip Strength");
    //    }

    //    else if (Asses == 5)
    //    {
    //        SceneManager.LoadScene("DrawPath");
    //    }

    //}

    public void onclick_drawpath()
    {
        SceneManager.LoadScene("DrawPath");
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public void onclick_gamescene()
    {

        end_time = DateTime.Now.ToString("HH:mm:ss.fff");

        SceneManager.LoadScene("New Scene");
    }

    public void onclick_recalibrate()
    {
        SceneManager.LoadScene("Drawpath");

    }

    public void onclick_login()
    {
        SceneManager.LoadScene("Welcome");

    }
}
