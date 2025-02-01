﻿//using UnityEngine;
//using System.Collections;

//public class BoundController : MonoBehaviour
//{

//    //enemy transform
//    public Transform enemy;

//    public int enemyScore;
//    public int playerScore;
//    public AudioClip[] audioClips; // win ,loose

//    void Start()
//    {
//        enemyScore = 0;
//        playerScore = 0;
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.tag == "Target")
//        {
//            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
//            {
//                playAudio(1);
//                enemyScore++;
//            }
//            else
//            {
//                playerScore++;
//                playAudio(0);
//            }


//            //Destroys other object
//            Destroy(other.gameObject);

//            //sets enemy's position back to original
//            enemy.position = new Vector3(-6, 0, 0);
//            //pauses game
//            Time.timeScale = 0;
//        }
//    }
//    void playAudio(int clipNumber)
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        audio.clip = audioClips[clipNumber];
//        audio.Play();

//    }
//}




using UnityEngine;

public static class scoreclass
{
    public static int playerpoint;
    public static int enemypoint;
}

public class BoundController : MonoBehaviour
{
    public Transform enemy;
    public int enemyScore;
    public int playerScore;
    public AudioClip[] audioClips;

    void Start()
    {
        enemyScore = 0;
        playerScore = 0;
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            if (other.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                PlayAudio(1); // Enemy scores
                enemyScore++;
            }
            else
            {
                PlayAudio(0); // Player scores
                playerScore++;
            }


            scoreclass.enemypoint = enemyScore;
            scoreclass.playerpoint = playerScore;
            //if (enemyScore<5 || playerScore<5) {
            
            //  scoreclass.enemypoint =   enemyScore;
            //  scoreclass.playerpoint = playerScore;
            //}else
            //{
            //    scoreclass.enemypoint = 0;
            //    scoreclass.playerpoint =0;
            //}
        Destroy(other.gameObject); // Destroy the ball

            enemy.position = new Vector3(-6, 0, 0); // Reset enemy position
           // Time.timeScale = 0; // Pause the game
        }
    }

    void PlayAudio(int clipNumber)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioClips[clipNumber];
        audio.Play();
    }
}
