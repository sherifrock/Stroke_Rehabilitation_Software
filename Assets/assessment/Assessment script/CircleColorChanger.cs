using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using Random = UnityEngine.Random;
using static UnityEngine.GraphicsBuffer;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UI; // Add this line to access UI components


public enum State
{
    // None = -1, // Placeholder state
    TargetAppeared = 0,
    reachingtarget = 1,//deactivating circle(circle disappears)
    targetreached = 2,
    homepositionappears = 3,//circle appears
    reachinghome = 4,//deactivating target
    reachedhome = 5,

}


public class CircleColorChanger : MonoBehaviour
{
   
    public static CircleColorChanger instance;

    public GameObject circlePrefab;
    public GameObject targetPrefab;

    public Transform PlayerPosition;

    public KeyCode spawnKey = KeyCode.Space;

    public float delayInSeconds = 1f;
    public int numberOfTargets = 7;
    public float distancefromhome = 3.5f;
    public Color insideColor = Color.red;
    public Color leavingColor = Color.green;
    public Color stayingColor = Color.yellow;
    public float detectionradius = 0.55f;
    private GameObject spawnedCircle;
    private GameObject[] spawnedTargets;
    private int currentTargetIndex = 0;
    private bool isPlayerInsideCircle = false;
    private bool isPlayerInsideTarget = false;
    public float targetresttime = 1f;
    public float circleresttime = 3f;
    private float timeInside = 0f;

    private bool isSpawningTarget = false;
    private bool isSpawningCircle = false;
    private bool circleAppeared = false; // Track if the circle has appeared

    private State currentState;
    private bool shouldStopStateMachine = false; // Flag to indicate if the state machine should stop
    public Text moveBackText; // Reference to the UI text element
    float enc_1, enc_2;
    float Rob_X, Rob_Y;
    string TargetPosx, TargetPosy, CurrentStat, PlayerPosx, PlayerPosy, CirclePositionX, CirclePositionY, targetRadii, circleRadiusStr;
    // Add class variables to track time
    private float circleActivationTime;
    private float circleTotargetDuration;
    private float targetReachedTime;
    private float targetToCircleDuration;
    int assessmentRounds = 0;
    int totalRounds = 3; // Total number of assessment rounds including the initial one
    public float delayBeforeNextAssessment = 4.0f; // Adjust as needed
    public bool isAssessmentRunning = false;
    public Text roundCompletionText;
    public float textDisplayDelay = 5f; // Delay in seconds before hiding the text
    public Text assessmentoverText;

    // Define a List to store the positions of the targets spawned in the first round
    List<Vector2> targetPositionsFirstRound = new List<Vector2>();



    private string saveFilePath;


    [System.Serializable]
    public class PositionData
    {
        public float x;
        public float y;

        public PositionData(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }



    void Start()
    {
      
        StartCoroutine(WaitForSKeyPress());
        //currentState = State.WaitingForInput;

        enc_1 = PlayerPrefs.GetFloat("Enc1");
        enc_2 = PlayerPrefs.GetFloat("Enc2");
        Rob_X = PlayerPrefs.GetFloat("Robx");
        Rob_Y = PlayerPrefs.GetFloat("Roby");
        TargetPosx = PlayerPrefs.GetString("targetPos1");
        TargetPosy = PlayerPrefs.GetString("targetPos2");
        CurrentStat = PlayerPrefs.GetString("Currentstat");
        PlayerPosx = PlayerPrefs.GetString("PlayerX");
        PlayerPosy = PlayerPrefs.GetString("PlayerY");
        CirclePositionX = PlayerPrefs.GetString("CirclePosX");
        CirclePositionY = PlayerPrefs.GetString("CirclePosY");
        targetRadii = PlayerPrefs.GetString("targetRadii");
        circleRadiusStr = PlayerPrefs.GetString("circleRadius");
        string circlepath = circleclass.circlePath;
        saveFilePath = Path.Combine(circlepath, "circlePosition.json");

    }

    IEnumerator WaitForSKeyPress()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                //yield return new WaitForSeconds(delayInSeconds);
                StartAssessment(numberOfTargets, distancefromhome);

            }
            yield return null;
        }
    }
    IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {

                case State.TargetAppeared:
                    Debug.Log("Current State: " + (int)currentState);
                    yield return TargetAppearedState();
                    break;
                case State.reachingtarget:
                    Debug.Log("Current State: " + (int)currentState);
                    yield return reachingtargetState();
                    break;
                case State.targetreached:
                    Debug.Log("Current State: " + (int)currentState);
                    yield return targetreachedState();
                    break;


                case State.homepositionappears:
                    Debug.Log("Current State: " + (int)currentState);
                    yield return homepositionappearsState();
                    break;
                case State.reachinghome:
                    Debug.Log("Current State: " + (int)currentState);
                    yield return reachinghomeState();
                    break;
                case State.reachedhome:
                    Debug.Log("Current State: " + (int)currentState);
                    yield return reachedhomeState();
                    break;

            }
            // After the state transition, save the state to CSV
            Data(currentState);


        }
    }

    void Update()
    {


        //if (File.Exists(saveFilePath))
        //{
        //    Vector2 spawnPosition;


        //    if (Input.GetKeyDown(spawnKey))
        //    {
        //        string json = File.ReadAllText(saveFilePath);
        //        PositionData positionData = JsonUtility.FromJson<PositionData>(json);
        //        spawnPosition = new Vector2(positionData.x, positionData.y);
        //        spawnedCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        //        Collider2D circleCollider = spawnedCircle.AddComponent<CircleCollider2D>();
        //        circleCollider.isTrigger = true;
        //        ((CircleCollider2D)circleCollider).radius = detectionradius;

        //        spawnedCircle.SetActive(true);
        //        Debug.Log("Spawned circle position: " + spawnedCircle.transform.position);

        //        // Record the activation time of the circle
        //        circleActivationTime = Time.time;

        //        StartCoroutine(StateMachine());
        //    }



        //} else
        //{
        //    if (Input.GetKeyDown(spawnKey))
        //    {


        //        SpawnCircleAtPlayerPosition();


        //        StartCoroutine(StateMachine());
        //    }
        //}



        if (Input.GetKeyDown(spawnKey))
        {


            SpawnCircleAtPlayerPosition();


            StartCoroutine(StateMachine());
        }

        if (spawnedCircle != null)
        {
            // Get the player's position
            //Vector2 PlayerPosition = transform.position;

            GameObject Player = GameObject.FindGameObjectWithTag("Player");

            if (Player != null)
            {
                // Get the player's position
                Vector2 PlayerPosition = Player.transform.position;

                // Check if the player is inside the circle
                bool isPlayerInside = IsPlayerInsideCircle(PlayerPosition);

                if (isPlayerInside)
                {
                    SetCircleColor(Color.red);
                    Debug.Log("player inside circle");



                }
                else
                {
                    SetCircleColor(Color.green);
                    Debug.Log("player outside circle");




                }
            }
            else
            {
                Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
            }
        }

        if (spawnedTargets != null && spawnedTargets.Length >= 0 && currentTargetIndex < spawnedTargets.Length)
        {
            foreach (GameObject Target in spawnedTargets)
            {



                GameObject Player = GameObject.FindGameObjectWithTag("Player");

                if (Player != null)
                {
                    // Get the player's position
                    Vector2 PlayerPosition = Player.transform.position;

                    //Vector2 playerPosition = transform.position;
                    bool wasPlayerInside = isPlayerInsideTarget;
                    if (currentTargetIndex < spawnedTargets.Length)
                    {
                        isPlayerInsideTarget = IsPlayerInsideTarget(PlayerPosition, spawnedTargets[currentTargetIndex]);

                        if (isPlayerInsideTarget)
                        {
                            SetTargetColor(insideColor, currentTargetIndex);

                            Debug.Log("player inside target");


                            if (!wasPlayerInside)
                            {
                                timeInside = 0f;


                            }

                            if (timeInside >= targetresttime)
                            {
                                SetTargetColor(stayingColor, currentTargetIndex);

                                Debug.Log("player staying inside target");

                            }

                            else
                            {
                                timeInside += Time.deltaTime;
                            }
                        }

                        else
                        {
                            SetTargetColor(leavingColor, currentTargetIndex);
                            Debug.Log("player outside target");

                            timeInside = 0f;



                        }




                    }
                }
            }

        }


    }
    void SpawnCircleAtPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;






            spawnedCircle = Instantiate(circlePrefab, playerPosition, Quaternion.identity);





            Collider2D circleCollider = spawnedCircle.AddComponent<CircleCollider2D>();
            circleCollider.isTrigger = true;
            // circleCollider.Radius = 1f;
            // Set the radius of the collider
            //((CircleCollider2D)circleCollider).radius = 1f;
            ((CircleCollider2D)circleCollider).radius = detectionradius;

            spawnedCircle.SetActive(true);
            // Log the position of the spawned circle
            Debug.Log("Spawned circle position: " + spawnedCircle.transform.position);
        }

        // Record the activation time of the circle
        circleActivationTime = Time.time;








        // Vector2 spawnPosition;

        // // Check if a saved position exists
        // if (File.Exists(saveFilePath))
        // {
        //     // Load the saved position

        // }
        // else
        // {
        //     // Get the player's position
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     if (player != null)
        //     {
        //         spawnPosition = player.transform.position;

        //         // Save the player's position to a JSON file
        //         PositionData newPositionData = new PositionData(spawnPosition.x, spawnPosition.y);
        //         Debug.Log("newPositionData");
        //         string newJson = JsonUtility.ToJson(newPositionData);
        //         File.WriteAllText(saveFilePath, newJson);
        //         spawnedCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        //         Collider2D circleCollider = spawnedCircle.AddComponent<CircleCollider2D>();
        //         circleCollider.isTrigger = true;
        //         ((CircleCollider2D)circleCollider).radius = detectionradius;

        //         spawnedCircle.SetActive(true);
        //         Debug.Log("Spawned circle position: " + spawnedCircle.transform.position);

        //         // Record the activation time of the circle
        //         circleActivationTime = Time.time;
        //     }
        //     else
        //     {
        //         Debug.LogWarning("Player not found!");
        //         return;
        //     }
        // }

        //// Spawn the circle at the determined position


        ////// Add and configure the collider
        ////Collider2D circleCollider = spawnedCircle.AddComponent<CircleCollider2D>();
        //// circleCollider.isTrigger = true;
        //// ((CircleCollider2D)circleCollider).radius = detectionradius;

        //// spawnedCircle.SetActive(true);
        //// Debug.Log("Spawned circle position: " + spawnedCircle.transform.position);

        //// // Record the activation time of the circle
        //// circleActivationTime = Time.time;
    }



    void StartAssessment(int numberOfTargets, float distancefromhome)
    {
        if (!isAssessmentRunning)
        {
            assessmentRounds++; // Increment the number of assessment rounds completed
            Debug.Log("Starting assessment round " + assessmentRounds);
            currentTargetIndex = 0;
            SpawnTargets(numberOfTargets, distancefromhome);
        }

    }

    //void SpawnTargets(int numberOfTargets, float distancefromhome)
    //{
    //    if (circlePrefab != null && targetPrefab != null)
    //    {
    //        Vector2 homePosition = new Vector2(spawnedCircle.transform.position.x, spawnedCircle.transform.position.y);
    //        float angleStep = Mathf.PI / (numberOfTargets - 1); // Use Mathf.PI for a semicircle
    //        //float angleStep = 360f / numberOfTargets;
    //        spawnedTargets = new GameObject[numberOfTargets];
    //        if (assessmentRounds == 1)
    //        {

    //            for (int i = 0; i < numberOfTargets; i++)
    //            {
    //                float angle = i * angleStep;
    //                float x = homePosition.x + distancefromhome * Mathf.Cos(angle);
    //                float y = homePosition.y + distancefromhome * Mathf.Sin(angle);
    //                Vector2 spawnPosition = new Vector2(x, y);

    //                spawnedTargets[i] = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
    //                Collider2D targetCollider = spawnedTargets[i].AddComponent<CircleCollider2D>(); // Use CircleCollider2D for 2D projects
    //                targetCollider.isTrigger = true;
    //                ((CircleCollider2D)targetCollider).radius = detectionradius;
    //                //((CircleCollider2D)targetCollider).radius = 1f;



    //                spawnedTargets[i].SetActive(false);
    //            }
    //        }
    //        else
    //        {
    //            // Ensure that all targets spawn at least once in the second round
    //            int guaranteedSpawnCount = Mathf.Min(numberOfTargets, 1);
    //            for (int i = 0; i < guaranteedSpawnCount; i++)
    //            {
    //                float angle = i * angleStep; // Adjust angle for the guaranteed spawns
    //                float x = homePosition.x + distancefromhome * Mathf.Cos(angle);
    //                float y = homePosition.y + distancefromhome * Mathf.Sin(angle);
    //                Vector2 spawnPosition = new Vector2(x, y);

    //                spawnedTargets[i] = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
    //                Collider2D targetCollider = spawnedTargets[i].AddComponent<CircleCollider2D>(); // Use CircleCollider2D for 2D projects
    //                targetCollider.isTrigger = true;
    //                ((CircleCollider2D)targetCollider).radius = detectionradius;
    //                spawnedTargets[i].SetActive(false);
    //            }

    //            // Randomly spawn the remaining targets
    //            for (int i = guaranteedSpawnCount; i < numberOfTargets; i++)
    //            {
    //                float randomAngle = Random.Range(0f, Mathf.PI); // Random angle in radians
    //                float x = homePosition.x + distancefromhome * Mathf.Cos(randomAngle);
    //                float y = homePosition.y + distancefromhome * Mathf.Sin(randomAngle);
    //                Vector2 spawnPosition = new Vector2(x, y);

    //                spawnedTargets[i] = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
    //                Collider2D targetCollider = spawnedTargets[i].AddComponent<CircleCollider2D>(); // Use CircleCollider2D for 2D projects
    //                targetCollider.isTrigger = true;
    //                ((CircleCollider2D)targetCollider).radius = detectionradius;
    //                spawnedTargets[i].SetActive(false); // Deactivate the spawned target initially
    //            }
    //        }
    
    void SpawnTargets(int numberOfTargets, float distanceFromHome)
    {
        if (circlePrefab != null && targetPrefab != null)
        {
            Vector2 homePosition = new Vector2(spawnedCircle.transform.position.x, spawnedCircle.transform.position.y);
            float angleStep = Mathf.PI / (numberOfTargets - 1); // Use Mathf.PI for a semicircle
                                                                //float angleStep = 360f / numberOfTargets;
            spawnedTargets = new GameObject[numberOfTargets];
            if (assessmentRounds == 1)
            {
                // First round, spawn targets normally and store their positions
                for (int i = 0; i < numberOfTargets; i++)
                {
                    float angle = i * angleStep;
                    float x = homePosition.x + distanceFromHome * Mathf.Cos(angle);
                    float y = homePosition.y + distanceFromHome * Mathf.Sin(angle);
                    Vector2 spawnPosition = new Vector2(x, y);

                    spawnedTargets[i] = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
                    targetPositionsFirstRound.Add(spawnPosition); // Store the position
                    Collider2D targetCollider = spawnedTargets[i].AddComponent<CircleCollider2D>(); // Use CircleCollider2D for 2D projects
                    targetCollider.isTrigger = true;
                    ((CircleCollider2D)targetCollider).radius = detectionradius;
                    spawnedTargets[i].SetActive(false);
                }
            }
            else
            {
                // Subsequent rounds, shuffle and spawn targets at positions from the first round
                List<Vector2> shuffledPositions = new List<Vector2>(targetPositionsFirstRound);
                shuffledPositions.Shuffle(); // Shuffle the positions

                for (int i = 0; i < numberOfTargets; i++)
                {
                    Vector2 spawnPosition = shuffledPositions[i];

                    spawnedTargets[i] = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
                    Collider2D targetCollider = spawnedTargets[i].AddComponent<CircleCollider2D>(); // Use CircleCollider2D for 2D projects
                    targetCollider.isTrigger = true;
                    ((CircleCollider2D)targetCollider).radius = detectionradius;
                    spawnedTargets[i].SetActive(false);
                }
            }
        }
    }
       
    IEnumerator TargetAppearedState()
    {
        if (isSpawningCircle) yield break; // Avoid spawning a new circle if one is already being spawned

        isSpawningCircle = true;


        if (spawnedCircle != null)
        {

            bool playerInsideCircle = true; // Assuming player is initially inside the circle
            float timeInsideCircle = 0f; // Track the time the player spends inside the circle
            bool targetAppeared = false; // Track if the target has appeared


            while (playerInsideCircle && timeInsideCircle < circleresttime) // Loop until the player is inside for 2 seconds

            {
                yield return null; // Wait for the next frame

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    Vector2 playerPosition = player.transform.position;
                    playerInsideCircle = IsPlayerInsideCircle(playerPosition);
                    //Debug.Log("Player inside circle: " + isplayerInsideCircle);
                    if (playerInsideCircle)
                    {
                        timeInsideCircle += Time.deltaTime; // Increment time spent inside the circle

                    }
                    else
                    {
                        timeInsideCircle = 0f; // Reset time if player exits the circle

                    }

                }


            }


            if (timeInsideCircle >= circleresttime)
            {
                // spawnedCircle.SetActive(false);
                //Debug.Log("Deactivating spawned circle");

                StartCoroutine(SpawnNextTargetactivate());// Increment the target index to move to the next target
                targetAppeared = true;
                currentState = State.reachingtarget; // Update currentState to reachingtarget
                Debug.Log("Target appeared: " + targetAppeared);
                Debug.Log("Transitioning to State " + (int)currentState);
            }
            else
            {
                // Player moved out of the circle before the target appeared
                Debug.Log("Player moved out of the circle before the target appeared.");
                ShowMoveBackText(); // Show the UI text
            }


        }
        isSpawningCircle = false;
    }
    void ShowMoveBackText()
    {
        if (moveBackText != null)
        {
            moveBackText.gameObject.SetActive(true); // Enable the UI text
            StartCoroutine(HideMoveBackTextAfterDelay()); // Hide the text after some delay
        }
    }
    IEnumerator HideMoveBackTextAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay as needed
        if (moveBackText != null)
        {
            moveBackText.gameObject.SetActive(false); // Disable the UI text
        }
    }
    IEnumerator SpawnNextTargetactivate()
    {

        if (isSpawningTarget) yield break; // Avoid spawning a new target if one is already being spawned

        isSpawningTarget = true;
        //check if the next target index is within the bounds of spawnedTargets array
        if (spawnedTargets != null && spawnedTargets.Length >= 0 && currentTargetIndex < spawnedTargets.Length)
        {
            // Activate the target at the current index
            spawnedTargets[currentTargetIndex].SetActive(true);

            Debug.Log("Activating target: " + currentTargetIndex);


            bool playerInsideTarget = true;




            while (playerInsideTarget && currentTargetIndex < spawnedTargets.Length)
            {
                yield return null;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    Vector2 playerPosition = player.transform.position;
                    playerInsideTarget = IsPlayerInsideTarget(playerPosition, spawnedTargets[currentTargetIndex]);
                }




            }



        }


        isSpawningTarget = false;
    }
    IEnumerator reachingtargetState()
    {
        if (isSpawningCircle) yield break; // Avoid spawning a new circle if one is already being spawned

        isSpawningCircle = true;


        if (spawnedCircle != null)
        {
            bool playerInsideCircle = true;
            bool targetAppeared = true;

            if (targetAppeared)
            {



                while (playerInsideCircle)
                {
                    yield return null;

                    GameObject player = GameObject.FindGameObjectWithTag("Player");

                    if (player != null)
                    {
                        Vector2 playerPosition = player.transform.position;
                        playerInsideCircle = IsPlayerInsideCircle(playerPosition);
                        Debug.Log("Player inside circle: " + playerInsideCircle);
                    }
                }

                // Player left the circle, deactivate it
                spawnedCircle.SetActive(false);
                // Player reached the target within 2 seconds
                // Calculate reaching duration
                circleTotargetDuration = Time.time - circleActivationTime;
                // Record the time when the target is reached
                targetReachedTime = Time.time;
                currentState = State.targetreached; // Update currentState to targetreached
                Debug.Log("Target reached: " + targetAppeared);
                Debug.Log("Transitioning to State " + (int)currentState);


                //// Calculate reaching duration
                //circleTotargetDuration = Time.time - circleActivationTime;
                //// Record the time when the target is reached
                //targetReachedTime = Time.time;
                //currentState = State.targetreached; // Update currentState to targetreached
                //Debug.Log("Deactivating spawned circle");
                //Debug.Log("Transitioning to State " + (int)currentState);

            }


        }
        isSpawningCircle = false;
    }
    IEnumerator targetreachedState()
    {
        float elapsedTime = 0f; // Track elapsed time
        bool playerReachedTarget = false;

        while (elapsedTime < 3f)
        {
            // Check if the player is inside the target
            if (IsPlayerInsideTarget(PlayerPosition.position, spawnedTargets[currentTargetIndex]))
            {
                // Player has reached the target within 4 seconds
                playerReachedTarget = true;
                break;
            }

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame before checking again
        }

        // If player reached the target within 4 seconds
        if (playerReachedTarget)
        {
            currentState = State.homepositionappears;
            Debug.Log("Player has reached the target within 4 seconds. Transitioning to homepositionappears state.");
            Debug.Log("Transitioning to State " + (int)currentState);
        }
        else
        {
            // Player didn't reach the target within 4 seconds
            // Make the target disappear
            MakeTargetDisappear();
            ActivateHomeposition();
            // Display assessment text
           // GetComponent<CircleColorChanger>().ShowAssessmentText("Assessment over. Keep trying ");
            Debug.Log("Player took more than 4 seconds to reach the target. Target disappeared. Transitioning to homepositionappears state.");

            Debug.Log("Transitioning to State " + (int)currentState);
            yield return new WaitForSeconds(1f);
            currentState = State.reachinghome;
        }
    }

    void MakeTargetDisappear()
    {
        // Code to make the target disappear
        // For example:
        spawnedTargets[currentTargetIndex].SetActive(false);
    }

    void ActivateHomeposition()
    {
        // Implement logic to make the homeposition appear here
        // For example:
        spawnedCircle.SetActive(true);
    }
    public void ShowAssessmentText(string text)
    {
        assessmentoverText.text = text;
        assessmentoverText.gameObject.SetActive(true);
        //if (assessmentoverText != null)
        //{
        //    // Display assessment text on the screen using Unity UI
        //    assessmentoverText.text = "You took too long to reach the target.";
        //}
        //else
        //{
        //    Debug.LogWarning("Assessment Text component is not assigned!");
        //}

    }

    IEnumerator homepositionappearsState()
    {
        if (isSpawningTarget) yield break;

        isSpawningTarget = true;

        if (spawnedTargets != null && spawnedTargets.Length >= 0 && currentTargetIndex < spawnedTargets.Length)
        {
            if (spawnedTargets[currentTargetIndex].activeSelf)
            {
                bool wasPlayerInside = isPlayerInsideTarget;
                bool playerInsideTarget = true;
                float timeInsideTarget = 0f;

                while (playerInsideTarget && currentTargetIndex < spawnedTargets.Length && timeInsideTarget < targetresttime)
                {
                    yield return null;

                    GameObject player = GameObject.FindGameObjectWithTag("Player");

                    if (player != null)
                    {
                        Vector2 playerPosition = player.transform.position;
                        playerInsideTarget = IsPlayerInsideTarget(playerPosition, spawnedTargets[currentTargetIndex]);
                        if (playerInsideTarget)
                        {
                            timeInsideTarget += Time.deltaTime; // Increment time spent inside the circle

                        }
                        else
                        {
                            timeInsideTarget = 0f; // Reset time if player exits the circle

                        }
                    }



                }
                if (timeInsideTarget >= targetresttime)
                {
                    StartCoroutine(SpawnNextCircleactivate());

                    circleAppeared = true;
                    currentState = State.reachinghome; // Update currentState to reachinghome

                    Debug.Log("circle appeared");
                    Debug.Log("Transitioning to State " + (int)currentState);


                }


            }
        }
        isSpawningTarget = false;


    }
    IEnumerator SpawnNextCircleactivate()
    {
        if (isSpawningCircle) yield break; // Avoid spawning a new circle if one is already being spawned

        isSpawningCircle = true;

        if (spawnedCircle != null)
        {
            spawnedCircle.SetActive(true);
            Debug.Log("Activating spawned circle");


            bool playerInsideCircle = true; // Assuming player is initially inside the circle

            while (playerInsideCircle)
            {
                yield return null; // Wait for the next frame

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    Vector2 playerPosition = player.transform.position;
                    playerInsideCircle = IsPlayerInsideCircle(playerPosition);
                }
            }


        }
        isSpawningCircle = false;
    }
    IEnumerator reachinghomeState() //deactivating target
    {
        // Continuously check if the player has left the target
        while (IsPlayerInsideTarget(PlayerPosition.position, spawnedTargets[currentTargetIndex]))
        {
            yield return null; // Wait for the next frame before checking again
        }

        // Player has left the target, deactivate it
        spawnedTargets[currentTargetIndex].SetActive(false);
        Debug.Log("Deactivating target: " + currentTargetIndex);

        // Transition to the next state
        currentState = State.reachedhome;
        Debug.Log("Transitioning to State " + (int)currentState);
        IncrementNextTarget();

    }
    IEnumerator reachedhomeState()
    {
        // Continuously check if the player is inside the home
        while (true)
        {
            // Check if the player is inside the home
            if (IsPlayerInsideCircle(PlayerPosition.position))
            {
                // Calculate reaching duration from target back to circle
                targetToCircleDuration = Time.time - targetReachedTime;
                // Player is inside the home, transition to State 0 (Target Appeared)
                currentState = State.TargetAppeared;
                Debug.Log("Player has reached home. Transitioning to State 0 (Target Appeared).");
                Debug.Log("Transitioning to State " + (int)currentState);
                yield break; // Exit the coroutine
            }

            yield return null; // Wait for the next frame before checking again

        }

    }



    void IncrementNextTarget()
    {
        currentTargetIndex++;
        if (currentTargetIndex >= numberOfTargets)
        {
            //currentTargetIndex = 0;
            shouldStopStateMachine = true; // Set the flag to stop the state machine
            AllTargetsVisitedState();
        }
    }
    void AllTargetsVisitedState()
    {
        Debug.Log("All targets visited!");

        if (assessmentRounds < totalRounds)
        {
            ResetAssessment();
        }
        else
        {
            Debug.Log("Assessment completed.");
            // You might want to perform any final actions here after completing all rounds.
        }
        DisplayRoundCompletionText();
        //// Display round completion text
        //if (assessmentRounds <= totalRounds)
        //{
        //    roundCompletionText.text = "Round " + assessmentRounds + " completed!";
        //}
        //else
        //{
        //    roundCompletionText.text = "Assessment completed!";
        //}
        //// Activate the round completion text
        //roundCompletionText.gameObject.SetActive(true);

        //// Start coroutine to hide the text after a delay
        //StartCoroutine(HideTextAfterDelay());
    }
    void DisplayRoundCompletionText()
    {
        Debug.Log("DisplayRoundCompletionText called."); // Add this line
        // Determine the text to display based on the assessment round
        string completionText = (assessmentRounds <= totalRounds) ?
            "Round " + assessmentRounds + " completed!" : "Assessment completed!";

        // Set the text of the UI Text element
        roundCompletionText.text = completionText;

        // Activate the round completion text
        roundCompletionText.gameObject.SetActive(true);

        // Start coroutine to hide the text after a delay
        StartCoroutine(HideTextAfterDelay());
    }

    IEnumerator HideTextAfterDelay()
    {
        Debug.Log("HideTextAfterDelay coroutine started."); // Add this line
        // Wait for the specified delay
        yield return new WaitForSeconds(textDisplayDelay);

        // Deactivate the round completion text
        roundCompletionText.gameObject.SetActive(false);
    }
    void ResetAssessment()
    {
        // Reset any assessment-related variables or targets here.
        isAssessmentRunning = false;

        // Start a new assessment after a delay.
        StartCoroutine(WaitForNextAssessment());
    }

    IEnumerator WaitForNextAssessment()
    {
        // Wait for some time before starting the next assessment round.
        //yield return new WaitForSeconds(2.0f); // Adjust as needed

        yield return new WaitForSeconds(2.0f); // take break before starting next assessment round

        // Start the next round of assessment.
        StartAssessment(numberOfTargets, distancefromhome);
    }
    

    void SetCircleColor(Color color)
    {
        if (spawnedCircle != null)
        {
            Renderer circleRenderer = spawnedCircle.GetComponent<Renderer>();
            if (circleRenderer != null)
            {
                circleRenderer.material.color = color;
            }
        }
    }

    void SetTargetColor(Color color, int currentTargetIndex)
    {
        if (spawnedTargets != null && spawnedTargets.Length > 0 && currentTargetIndex < spawnedTargets.Length)
        {
            if (spawnedTargets[currentTargetIndex] != null)
            {
                Renderer targetRenderer = spawnedTargets[currentTargetIndex].GetComponent<Renderer>();
                if (targetRenderer != null)
                {
                    targetRenderer.material.color = color;
                }
            }
        }
    }

    bool IsPlayerInsideCircle(Vector2 PlayerPosition)
    {
        if (spawnedCircle != null)
        {
            //Vector2 circlePosition = new Vector2(spawnedCircle.transform.position.x, spawnedCircle.transform.position.y);
            Vector2 circlePosition = spawnedCircle.transform.position;
            float distance = (PlayerPosition - circlePosition).magnitude;
            return distance <= detectionradius;
        }
        return false;
    }

    bool IsPlayerInsideTarget(Vector2 PlayerPosition, GameObject target)
    {
        if (target != null)
        {
            Vector2 targetPosition = target.transform.position;
            float distance = (PlayerPosition - targetPosition).magnitude;
            return distance <= detectionradius;
        }
        return false;
        //float distance = Vector2.Distance(PlayerPosition, target.transform.position);
        //return distance <= detectionradius;
    }

    void Data(State state)
    {
        // Clear previous PlayerPrefs data
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("targetPos1");
        PlayerPrefs.DeleteKey("targetPos2");
        PlayerPrefs.DeleteKey("targetRadii");
        PlayerPrefs.DeleteKey("CirclePosX");
        PlayerPrefs.DeleteKey("CirclePosY");
        PlayerPrefs.DeleteKey("CircleRadius");
        PlayerPrefs.DeleteKey("Currentstat");

        // Now save the current data
        string currentStateStr = ((int)state).ToString();

        // Get the positions of all active targets
        string targetPositions1 = "";
        string targetPositions2 = "";
        string targetRadii = ""; // String to store target radii


        foreach (GameObject target in spawnedTargets)
        {
            if (target.activeSelf)
            {
                Vector2 position = target.transform.position;
                targetPositions1 += $"{position.x};";
                targetPositions2 += $"{position.y};";
                //Vector2 position = target.transform.position;
                //Vector2 scale = target.transform.localScale;
                //targetData += $"{position.x};{position.y};{scale.x};{scale.y};";
                // Get the target's radius
                CircleCollider2D collider = target.GetComponent<CircleCollider2D>();
                if (collider != null)
                {
                    float radius = collider.radius;
                    targetRadii += $"{radius};";
                }
            }
        }

        // Get the player's position
        //string Playerpositionx = "";
        //string Playerpositiony = "";
        Vector2 playerPosition = PlayerPosition.transform.position;
        String Playerpositionx = $"{playerPosition.x};";
        String Playerpositiony = $"{playerPosition.y};";

        // Get the position of the spawned circle
        Vector2 circlePosition = spawnedCircle.transform.position;
        string circlePositionx = $"{circlePosition.x};";
        string circlePositiony = $"{circlePosition.y};";
        Debug.Log("Spawned circle position: " + spawnedCircle.transform.position);
        // Get the radius of the spawned circle
        float circleRadius = spawnedCircle.GetComponent<CircleCollider2D>().radius;
        Debug.Log("Circle Radius: " + circleRadius); // Add this line
        string circleRadiusStr = $"{circleRadius};";

        // Save the data to PlayerPrefs
        PlayerPrefs.SetString("PlayerX", Playerpositionx);
        PlayerPrefs.SetString("PlayerY", Playerpositiony);
        PlayerPrefs.SetString("targetPos1", targetPositions1);
        PlayerPrefs.SetString("targetPos2", targetPositions2);
        PlayerPrefs.SetString("targetRadii", targetRadii); // Save target radii
        PlayerPrefs.SetString("CirclePosX", circlePositionx); // Save circle position X
        PlayerPrefs.SetString("CirclePosY", circlePositiony); // Save circle position Y
        PlayerPrefs.SetString("CircleRadius", circleRadiusStr); // Save circle radius
        PlayerPrefs.SetString("Currentstat", currentStateStr);

    }

}



