//using System;
//using System.IO;
//using UnityEngine;

//public static class CsvHelper
//{
//    private static string folderPath = "; // Update with your actual folder path
//    private static string filepath_Pong = Path.Combine(folderPath, "pong_data.csv");

//    public static void CreateCsvFile()
//    {
//        if (!File.Exists(filepath_Pong))
//        {
//            File.WriteAllText(filepath_Pong, "Time,Encoder1,Encoder2,PaddlePosition,State\n");
//        }
//    }

//    public static void AppendToCsvFile(string encoder1, string encoder2, string paddlePosition, string state)
//    {
//        string currentDateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
//        string data = $"{currentDateTime},{encoder1},{encoder2},{paddlePosition},{state}\n";
//        File.AppendAllText(filepath_Pong, data);
//    }
//}

