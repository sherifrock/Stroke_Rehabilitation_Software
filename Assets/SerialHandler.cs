using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using PlutoDataStructures;
using System.IO.Ports;

public class SerialHandler : MonoBehaviour {

    //public Text statusText;
    //public Text sensDataText;
    //public Text errDataText;
    //public Text MechSelText;
    //public Dropdown mechList;
    //public Dropdown comPorts;
    //static public SerialPort serPort;

    private StringBuilder sb = new StringBuilder();
    //private int[] _count;
    //private int[] _Th = new int[] { 10, 50, };

    public float stopClock = 0;

    public int n = 0;

    float randomValue =-9;
    // Use this for initialization
    void Awake () {
        // First check subject info is empty.
        //if (AppData.subjHospNum == "")
        //{
        //   // Debug.Log("here!!!");
        //    if ((AppData.subjHospNum == ""))
        //    {
        //        AppData.subjHospNum = "admin";
        //        AppData._port = "COM3";
        //        ConnectToRobot.Connect(AppData.plutoData);
        //        AppData.plutoData.mechIndex = 0;




        //    }


        //}

        //AppData.WriteSessionInfo("Serial Handler running");

        // Update the list of mechanisms.
        //  UpdateMechList();

        // Update GUI
        // KepGuiUpToDate();
        //UpdateComPort();

        // Init and connect to robot.


        // Send all relevant parameter information.
        //  SendToRobot.ControlParam("", ControlType.NONE, false, false);
        //SendToRobot.TorqueSensorParam();
        //  AppData.WriteSessionInfo("Control set to NONE. | Sent torque sensor param.");

        // Mechanism selected
        //  On_Mechanism_Change();
        //string _fname = AppData.GetNewFileName("raw");
        //AppData.StartDataLog(_fname);
        //Debug.Log(_fname);
    }
	
	// Update is called once per frame
	void Update () {
        stopClock += Time.deltaTime;

        //if (Input.GetKeyUp(KeyCode.LeftControl))
        //{
        //    n++;

        //}

        //if (n % 2 == 1)
        //{

        //if (AppData.isLogging)
        //{
        //    AppData.LogData();
        //    Debug.Log("data logging");


        }
        //}

        //if (n % 2 == 0 && n>0)
        //{

        //if (stopClock > 120)
        //{
        //    AppData.StopLogging();
        //    Debug.Log("Stop data log");
        //    //n = 0;
        //}
    //}
    // Keep the GUI up to date.
    // KepGuiUpToDate();

    // Update sensor data display
    //UpdateSensorDataDisplay();
    // Update status text.
    //UpdateStatusText();

    // set des angles last number is dummy for now

    // first variable is dummy; data will be sent as resistance parameters with control info
    //string mech = AppData.plutoData.mechs[0];
       

        //if (Input.GetKeyUp(KeyCode.LeftControl))
        //{
            //randomValue = UnityEngine.Random.Range(0, 200);
            ////AppData.plutoData.PCParam = new float[] { randomValue, 0, 0, 0 };
            //SendToRobot.ControlParam(mech, ControlType.POSITION, false, true);
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    Debug.Log("RE-calibrated");
            //    SendToRobot.Calibrate();
            //}
        //}
        //Debug.Log("angle1" + AppData.plutoData.enc1*57.3f);
        //Debug.Log("angle2" + AppData.plutoData.enc2*57.3f);
        //Debug.Log(Time.deltaTime + "," + randomValue + "," + AppData.plutoData.des1);
    }

    //void OnApplicationQuit()
    //{
    //    MySerialThread.Disconnect();
    //}

//    private void UpdateSensorDataDisplay()
//    {
//        // Update sensor data display and erro messages.
//        if (AppData.count[0]++ > AppData.Th[0])
//        {
//            // Error message update
//            if ((AppData.plutoData.error1 == 0) && (AppData.plutoData.error2 == 0))
//            {
               
//                //errDataText.text = "";
//            }
//            else
//            {

//                Debug.Log("error");
//                //errDataText.text = "ERROR: ";
//                //if (AppData.plutoData.error1 != 0)
//                //{
//                //    for (int i = 0; i < 8; i++)
//                //    {
//                //        if (((AppData.plutoData.error1 >> i) & 0x01) == 0x01)
//                //        {
//                //            errDataText.text += DataTypeDefinitions.errorTypes[0][i] + " | ";
//                //        }
//                //    }
//                //}
//                //if (AppData.plutoData.error2 != 0)
//                //{
//                //    for (int i = 0; i < 8; i++)
//                //    {
//                //        if (((AppData.plutoData.error2 >> i) & (byte)0x01) == (byte)0x01)
//                //        {
//                //            errDataText.text += DataTypeDefinitions.errorTypes[1][i] + " | ";
//                //        }
//                //    }
//                //}
//            }

//        //    // Sensor data update
//        //    sensDataText.text = "Sensor Data\n";
//        //    sensDataText.text += "Ang: \t\t" + AppData.plutoData.enc1.ToString() + "\n";
//        //    sensDataText.text += "Ang vel: \t" + AppData.plutoData.enc2.ToString() + "\n";
//        //    sensDataText.text += "Torque: \t" + AppData.plutoData.enc3.ToString() + "\n";
//        //    sensDataText.text += "Control: \t" + AppData.plutoData.control.ToString();
//        //    AppData.count[0] = 0;
//        }
//    }

//    private void UpdateStatusText()
//    {
//        if (AppData.count[1]++ > AppData.Th[1])
//        {
//            //statusText.text = "FR: " + ((int)MySerialThread.framerate).ToString();
//            AppData.count[1] = 0;
//        }
//    }

//    private void KepGuiUpToDate()
//    {
        
//        // Display game selection if no mechanism is selected.
//        //Debug.Log(mechList.value);
//    }

//    public void On_CalibRobotSensors_Click()
//    {
//        SendToRobot.Calibrate();
//        //AppData.WriteSessionInfo("Robot sensors calibrated.");
//    }

//    public void On_SendTorqSensorParam_Click()
//    {
//        SendToRobot.TorqueSensorParam();
//        //AppData.WriteSessionInfo("Sent torque sensor param.");
//    }

//    public void On_StartSensorStream_Click()
//    {
//        SendToRobot.StartStream();
//    }

//    public void On_Mechanism_Change()
//    {
//        //AppData.plutoData.mechIndex = Array.IndexOf(DataTypeDefinitions.PlutoMechanisms[1], mechList.options[mechList.value].text);
//        // Update selected mechanism list.
//        //MechSelText.text = mechList.options[mechList.value].text;
//        //AppData.WriteSessionInfo(MechSelText.text + " selected.");
//    }

//    public void On_AROM_Click()
//    {
//        //AppData.WriteSessionInfo("AROM assessent selected.");
//        switch (AppData.plutoData.mechIndex)
//        {
//            case 0:
//            case 1:
//            case 2:
//                SceneManager.LoadScene(3);
//                break;
//            default:
//                break;
//        }
//    }


//    public void UpdateMechList()
//    {
//       // mechList.options.Clear();
//        foreach (string subj in DataTypeDefinitions.PlutoMechanisms[1])
//        {
//           // mechList.options.Add(new Dropdown.OptionData() { text = subj });
//        }
       
//    }

//    public void UpdateComPort()
//    {
//        string[] ports = SerialPort.GetPortNames();

//       // comPorts.options.Clear();

//        foreach (string port in ports)
//        {
//           //comPorts.options.Add(new Dropdown.OptionData() { text = port });
//        }
//    }

//    public void On_PROM_Click()
//    {
//        //AppData.WriteSessionInfo("AROM assessent selected.");
//        switch (AppData.plutoData.mechIndex)
//        {
//            case 0:
//            case 1:
//            case 2:
//                SceneManager.LoadScene("PROM-WF");
//                break;
//            default:
//                break;
//        }
//    }
//}
