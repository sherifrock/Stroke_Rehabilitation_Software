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
using TMPro;
using Newtonsoft.Json;

public class patient
{
    public string name { get; set; }
    public string lastname { get; set; }
    public string age { get; set; }
    public string gender { get; set; }
    public string hospno { get; set; }
    //public string use_hand { get; set; }
}

public static class Staticvlass
{
    public static string CrossSceneInformation { get; set; }
    public static string FolderPath;
}
public class PatientRegister : MonoBehaviour
{

    public AudioClip[] sounds;
    private AudioSource source;

    public new InputField name;
    public InputField lastname;
    public InputField age;
    //public InputField sex;
    public Dropdown genderDropdown;
    public InputField hospno;
    public Text messageText;
    // public InputField path;
    //public int hand = 1;


    //public object JsonConvert { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
        // Debug.Log(hand+" :hand");
        onclick_register();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private IEnumerator ShowMessageFor3Seconds(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        messageText.gameObject.SetActive(false);
    }


    public void onclick_register()
    {
        string p_name = name.text;
        string p_lastname = lastname.text;
        string p_age = age.text;
        //int p_age = age.text;
        string p_gender = genderDropdown.options[genderDropdown.value].text;  // Get selected gender from Dropdown
       // string p_sex = sex.text;
        string p_hospno = hospno.text;
        Debug.Log("Name: " + name);
        Debug.Log("Lastname: " + lastname);
        Debug.Log("Age: " + age);
        //Debug.Log("Sex: " + sex);
        Debug.Log("Hospno: " + hospno);

        //p_path = path.text;


        string path_to_data = Application.dataPath;

        bool name_check = string.IsNullOrEmpty(p_name);
        bool hospno_check = string.IsNullOrEmpty(p_hospno);
        bool age_check=string.IsNullOrEmpty(p_age);



        
       
        if (age_check == true )
        {
            StartCoroutine(ShowMessageFor3Seconds("ENTER YOUR AGE"));
        }

        if (hospno_check == true)
        {
            StartCoroutine(ShowMessageFor3Seconds("ENTER YOUR CURRECT HOSPITAL NUMBER"));
        }
        if (name_check == true)
        {
            StartCoroutine(ShowMessageFor3Seconds("ENTER YOUR  NAME"));
        }




        if (name_check == true | hospno_check == true | age_check==true)
        {
            Debug.Log("Empty name or hospno or age");
        }
        else
        {


            if (!Directory.Exists(path_to_data + "\\" + p_hospno))

            {
                
                string path = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno;

                if (Directory.Exists(path))
                {
                   
                    Debug.Log("THE HOSPITAL ID IS ALREADY EXIST");
                    StartCoroutine(ShowMessageFor3Seconds("THE HOSPITAL ID IS ALREADY EXIST"));

                }
               
                else
                {






                    var patient_details = new patient { name = p_name, lastname = p_lastname, age = p_age, gender = p_gender, hospno = p_hospno };
                    string patient_json = JsonConvert.SerializeObject(patient_details);

                    Directory.CreateDirectory(path);
                    File.WriteAllText(path + "\\patient.json", patient_json);

                    Debug.Log("Registration successful");
                    SceneManager.LoadScene("Welcome");
                }
            }

        }

       

    }

    public void onclick_existing()
    {
        // SceneManager.LoadScene("History");
        SceneManager.LoadScene("Welcome");
    }
}