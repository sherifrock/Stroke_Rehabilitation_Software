using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewScript : MonoBehaviour
{
    public float max_x;
    public float min_x;
    public float max_y;
    public float min_y;


    List<Vector3> paths;
    int tocarry;
    int Asses;
    // Start is called before the first frame update
    void Start()
    {

        paths = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {

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

    public void onclick_game()
    {
        if (tocarry == 1)
        {
            SceneManager.LoadScene("SpaceShooterDemo");


            

            // max_x = Drawpath.instance.max_x;
            //min_x = Drawpath.instance.min_x;
            //max_y = Drawpath.instance.max_y;
            //min_y = Drawpath.instance.min_y;

        }
        else if (tocarry == 2)
        {
            SceneManager.LoadScene("FlappyCalibrate");
        }
        else if (tocarry == 3)
        {
            SceneManager.LoadScene("pongDrawPath");
        }

    }
    public void onclickGripStrength()
    {
        Asses = 4;
    }
    public void onclickRoM()
    {
        Asses = 5;
    }
    public void onclickassessment()
    {
        if (Asses == 4)
        {
            SceneManager.LoadScene("Full Weight Grip Strength");
        }

        else if (Asses == 5)
        {
            SceneManager.LoadScene("DrawPath");
        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
