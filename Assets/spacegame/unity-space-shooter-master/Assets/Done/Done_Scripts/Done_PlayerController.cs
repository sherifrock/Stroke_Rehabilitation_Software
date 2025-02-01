using UnityEngine;
using System.Collections;
using UnityEditor.Rendering;
using System.IO;
using System;
//using static UnityEditor.Experimental.GraphView.GraphView;

//[System.Serializable]
//public class Done_Boundary
//{
//    public float xMin, xMax, zMin, zMax;
//   // public static Done_Boundary instance;


//    ////public float xMin = Drawpath.instance.min_x;
//    ////public float xMax = Drawpath.instance.max_x;
//    ////public float zMin = Drawpath.instance.min_y;
//    ////public float zMax = Drawpath.instance.max_y;

//    //public float xMin = -17;
//    //public float xMax = 17;
//    //public float zMin = -4;
//    //public float zMax = 12;

//}



[System.Serializable]
public class Done_Boundary
{

    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

    public Done_Boundary(float minX, float maxX, float minZ, float maxZ)
    {
        xMin = minX;
        xMax = maxX;
        zMin = minZ;
        zMax = maxZ;
    }
}


public static class spaceclass
{
    public static string spacepath;
    public static string relativepath;
}
public class Done_PlayerController : MonoBehaviour
{
    public static Done_PlayerController instance;

    float max_x;
    float min_x;
    float max_y;
    float min_y;

    public float speed;
    public float tilt;
    public Done_Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    // private float nextFire;
    public float nextFire;

    private float[] angXRange = { -40, 40 };
    private float[] angZRange = { -40, -120 };

    public float fireStopClock;

    public float singleFireTime;

    double x_c;
    double y_c;

    float x_val, z_val;

    // Vector3[] positions_moved_x;
    float[] positions_moved_x;
    float[] positions_moved_z;
    int count_position = 0;


    public float l1 = 333;
    public float l2 = 381;

    public AudioClip[] sounds;
    private AudioSource source;




    public static string Date;
    public static JediSerialCom serReader;
    private string welcompath = Staticvlass.FolderPath;
    private static string spacePath;
    private float PlayerX, PlayerY, hazardX, hazardY, savedState;
    public float theta1;
    public float theta2;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //max_x = Drawpath.instance.max_x;
        //min_x = Drawpath.instance.min_x;
        //max_y = Drawpath.instance.max_y;
        //min_y = Drawpath.instance.min_y;

        // Debug.Log("START");
        // Debug.Log("max_x_play: "+max_x);
        // Debug.Log("min_x_play: "+min_x);
        // Debug.Log("max_y_play: "+max_y);
        // Debug.Log("min_y_play: "+min_y);




        JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        serReader = new JediSerialCom("COM12");
        serReader.ConnectToArduino();

        source = GetComponent<AudioSource>();

        // positions_moved_x = new Vector3[200];
        positions_moved_x = new float[200];
        positions_moved_z = new float[200];

        angXRange[0] = Drawpath.instance.max_x;
        angXRange[1] = Drawpath.instance.min_x;
        angZRange[0] = Drawpath.instance.max_y;
        angZRange[1] = Drawpath.instance.min_y;


        //angXRange[0] = Drawpath.instance.max_y;
        //angXRange[1] = Drawpath.instance.min_y;
        //angZRange[0] = Drawpath.instance.max_x;
        //angZRange[1] = Drawpath.instance.min_y;

        boundary = new Done_Boundary(-12, 12, -4, 12);

        //angXRange[0] = -2.5f;
        //angXRange[1] = 2.5f;
        //angZRange[0] = -4;
        //angZRange[1] = 4;

        // Debug.Log("update");

        Debug.Log(angXRange[0] + ".." + angXRange[1] + ".." + angZRange[0] + ".." + angZRange[1]);

        x_c = (angXRange[0] + angXRange[1]) / 2;
        y_c = (angZRange[0] + angZRange[1]) / 2;



        Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");
        string DataPath = Path.Combine(welcompath, "spaceshooter");
        // string assessfile = Path.Combine(welcompath, "space_Data");
        if (!Directory.Exists(DataPath))
        {
            Directory.CreateDirectory(DataPath);
        }
        spacePath = Path.Combine(DataPath, "space_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        spaceclass.spacepath = spacePath;

        //string fullFilePath = pongclass.filepath;

        string fullFilePath = spaceclass.spacepath;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
        spaceclass.relativepath = relativePath;
        

    }

    void Update()
    {
        Time.timeScale = 1;

        PlayerX = PlayerPrefs.GetFloat("Playerx");
        PlayerY = PlayerPrefs.GetFloat("Playery");
        hazardX = PlayerPrefs.GetFloat("HazardX");
        hazardY = PlayerPrefs.GetFloat("HazardY");

        //timer_ += Time.deltaTime;

        if (JediSerialPayload.data.Count == 2)
        {
            try
            {
                theta1 = float.Parse(JediSerialPayload.data[0].ToString());
                theta2 = float.Parse(JediSerialPayload.data[1].ToString());
            }
            catch (System.Exception)
            {
                // Handle exception
            }
        }




    }

   

    void FixedUpdate()
    {

        


        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }






        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;




        //float thetaa = Dof.theta1 * Mathf.Deg2Rad;
        //float thetab = Dof.theta2 * Mathf.Deg2Rad;

        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);

        //x = ((x1) / (333 + 381) * 7.5f);
        //y = ((y1 + 350) / (400 * 2)) * 4.8f;


        float x = ((x1) / (333 + 381) * 12f);
        float y = ((y1 + 350) / (400 * 2)) * 4.5f;

        x_val = x;
        z_val = ( y);

        Debug.Log(x_val + " " + z_val); 
        //Debug.Log("dx"+"dy"+x_val + " " + z_val); 

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(-Angle2ScreenX(x_val), boundary.xMin, boundary.xMax),
           0.0f,
            Mathf.Clamp(- Angle2ScreenZ(z_val), boundary.zMin, boundary.zMax)
        );

        // Debug.Log(Angle2ScreenZ(Dof.x) + "   ...  ... " + Angle2ScreenZ(Dof.y));

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);


        Vector3 xxx = GetComponent<Rigidbody>().position;

        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        game_data();


    }






    public float Angle2ScreenX(float anglex)
    {
        float playSizeX = boundary.xMax - boundary.xMin;
        //left hand use
        //return Mathf.Clamp(-(boundary.xMin + (anglex - angXRange[0]) * (playSizeX) / (angXRange[1] - angXRange[0])), -1.2f * playSizeX, 1.2f * playSizeX);
        // right hand use

        // return Mathf.Clamp((boundary.xMin + (anglex - angXRange[0]) * (playSizeX) / (angXRange[1] - angXRange[0])), -1.2f * playSizeX, 1.2f * playSizeX);
        return Mathf.Clamp((boundary.xMin + (anglex - angXRange[0]) * (playSizeX) / (angXRange[1] - angXRange[0])), -1.2f * playSizeX, 1.2f * playSizeX);
    }
    public float Angle2ScreenZ(float angleZ)
    {
        float playSizeZ = boundary.zMax - boundary.zMin;
        return Mathf.Clamp(boundary.zMin + (angleZ - angZRange[0]) * (playSizeZ) / (angZRange[1] - angZRange[0]), -1.2f * playSizeZ,1.2f * playSizeZ);

        //return Mathf.Clamp(boundary.zMin + (angleZ - angZRange[0]) * (playSizeZ) / (angZRange[1] - angZRange[0]), -1f * -4, 0.5f * 12);

    }

    public void game_data()
    {
        string filepath_Endata = spaceclass.spacepath;
        IsCSVEmpty(filepath_Endata);
    }

    private bool IsCSVEmpty(string filepath_Endata)
    {
        if (File.Exists(filepath_Endata))
        {
            if (new FileInfo(filepath_Endata).Length == 0)
            {
                string Endata = "Time,Playerx,Playery,hazardX,hazardY,savedState\n";
                File.WriteAllText(filepath_Endata, Endata);
            }

            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{savedState}\n";
            File.AppendAllText(filepath_Endata, data);
            return false;
        }
        else
        {
            string Endata = "Time,Playerx,Playery,hazardX,hazardY,savedState\n";
            File.WriteAllText(filepath_Endata, Endata);

            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{savedState}\n";
            File.AppendAllText(filepath_Endata, data);
            return true;
        }
    }


    

}



