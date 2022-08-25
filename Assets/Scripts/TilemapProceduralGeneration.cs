using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapProceduralGeneration : MonoBehaviour
{
    private int width = 100;
    float tilemapDistance = 0.28f;
    [Tooltip("Distance addition of one checkpoint to the next")]
    [SerializeField] int checkpointWidthAddition;
    private int widthAdditionMultiplier;

    [Tooltip("Add prefab maps here")]
    [SerializeField] GameObject[] obstacleMaps;
    private GameObject[] tilemaps;
    private int[] currentActiveTilemaps;
    private Collider2D[] mapColliders;

    [Tooltip("Checkpoint object prefab")]
    [SerializeField] GameObject checkpointPrefab;
    [Tooltip("Text in canvas for checkpoint distance")]
    [SerializeField] Text checkpointText;
    [Tooltip("Text in canvas for time left")]
    [SerializeField] Text timerText;
    [Tooltip("Text in canvas for player Score")]
    [SerializeField] Text playerScoreText;
    /*
    [Tooltip("Enter starting time")]
    [SerializeField] float timeLeft;
    */
    [Tooltip("Enter value of time that will be added every time a checkpoint is crossed")]
    [SerializeField] float timeCheckpointAddition;
    [Tooltip("Player Gameobject")]
    [SerializeField] GameObject player;
    [Tooltip("Grid Gameobject")]
    [SerializeField] GameObject grid;
    [Tooltip("Add Main Camera here")]
    [SerializeField] Camera mainCamera;
    [Tooltip("Path blocking start tiles")]
    [SerializeField] GameObject startTiles;
    /*
    [Tooltip("Add World Space Canvas here")]
    [SerializeField] GameObject canvasWorldSpace;
    [Tooltip("Add Distance Covered Prefab here")]
    [SerializeField] GameObject distanceCoveredPrefab;
    */


    private int newestMap;
    private int oldestMap;
    private float distanceOfPlayerToMiddleMap = 0.0f;


    private GameObject[] checkpoints;

    private bool[] checkpointCheck;

    private bool checkpointCrossed;

    private int checkpointDistanceUpdate;

    //private float checkpointDistance;

    private float checkpointLastDistance;

    //private GameObject[] distanceCovered;

    public static int currentPlayerTilemap;

    private Vector3 startingPosition;

    private int startingWidth;

    //private float playerScore;

    /*
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = -1;
    }
    */

    private void Start()
    {
        tilemapDistance *= width;

        startingWidth = width;

        startingPosition = player.transform.position;

        widthAdditionMultiplier = 0;

        checkpointDistanceUpdate = 1;

        currentPlayerTilemap = 1;

        newestMap = 3;

        oldestMap = currentPlayerTilemap;

        checkpointCrossed = false;

        //playerScore = 0.0f;

        //checkpointDistance = 0.0f;

        checkpointLastDistance = width;


        //Debug.Log("currentPlayerTilemap = " + currentPlayerTilemap);

        instantiateAllMaps();

        /*
        if (obstacleMaps.Length > 3)
        {
            tilemaps = new GameObject[3];

            currentActiveTilemaps = new int[3];

            checkpoints = new GameObject[3];

            checkpointCheck = new bool[3];

            //distanceCovered = new GameObject[3];

            for (int i = 0; i < checkpointCheck.Length; i++)
            {
                checkpointCheck[i] = true;
            }

            mapColliders = new Collider2D[3];

            for (int i = 0; i < currentActiveTilemaps.Length; i++)
            {
                currentActiveTilemaps[i] = -1;
            }
        }
        else
        {
            tilemaps = new GameObject[obstacleMaps.Length];

            currentActiveTilemaps = new int[obstacleMaps.Length];

            checkpoints = new GameObject[obstacleMaps.Length];

            checkpointCheck = new bool[obstacleMaps.Length];

            //distanceCovered = new GameObject[obstacleMaps.Length];

            for (int i = 0; i < checkpointCheck.Length; i++)
            {
                checkpointCheck[i] = true;
            }

            mapColliders = new Collider2D[obstacleMaps.Length];

            for (int i = 0; i < currentActiveTilemaps.Length; i++)
            {
                currentActiveTilemaps[i] = -1;
            }
        }
        

        for (int i = 0; i < tilemaps.Length; i++)
        {
            if(i == 0)
            {
                tilemaps[i] = Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier, i);

                checkpoints[i] = Instantiate(checkpointPrefab, new Vector3(checkpointPrefab.transform.position.x + (width - checkpointWidthAddition) + tilemapDistance - 9.05f, 5, checkpointPrefab.transform.position.z), checkpointPrefab.transform.rotation);
            }
            else
            {
                tilemaps[i] = Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), i);

                checkpoints[i] = Instantiate(checkpointPrefab, new Vector3(checkpoints[i - 1].transform.position.x + (width - checkpointWidthAddition) + tilemapDistance, 5, checkpointPrefab.transform.position.z), checkpointPrefab.transform.rotation);

            }

        }
        */

        if (obstacleMaps.Length > 3)
        {
            currentActiveTilemaps = new int[3];

            checkpoints = new GameObject[3];

            checkpointCheck = new bool[3];

            //distanceCovered = new GameObject[3];

            for (int i = 0; i < checkpointCheck.Length; i++)
            {
                checkpointCheck[i] = true;
            }

            mapColliders = new Collider2D[3];
        }
        else
        {
            currentActiveTilemaps = new int[obstacleMaps.Length];

            checkpoints = new GameObject[obstacleMaps.Length];

            checkpointCheck = new bool[obstacleMaps.Length];

            //distanceCovered = new GameObject[obstacleMaps.Length];

            for (int i = 0; i < checkpointCheck.Length; i++)
            {
                checkpointCheck[i] = true;
            }

            mapColliders = new Collider2D[obstacleMaps.Length];
        }

        for (int i = 0; i < currentActiveTilemaps.Length; i++)
        {
            currentActiveTilemaps[i] = -1;
        }


        for (int i = 0; i < currentActiveTilemaps.Length; i++)
        {
            if (i == 0)
            {
                Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier, i);

                checkpoints[i] = Instantiate(checkpointPrefab, new Vector3(checkpointPrefab.transform.position.x + (width - checkpointWidthAddition) + tilemapDistance - 9.05f, 5, checkpointPrefab.transform.position.z), checkpointPrefab.transform.rotation);
            }
            else
            {
                Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), i);

                checkpoints[i] = Instantiate(checkpointPrefab, new Vector3(checkpoints[i - 1].transform.position.x + (width - checkpointWidthAddition) + tilemapDistance, 5, checkpointPrefab.transform.position.z), checkpointPrefab.transform.rotation);

            }
        }

    }

    private void Update()
    {

        IsTilemapCollidingWithPlayer();

        TimeCountDown();

        TilemapSwapping();

        CheckpointMoving();

        ScoreCalculator();

    }

    private void instantiateAllMaps()
    {
        tilemaps = new GameObject[obstacleMaps.Length];

        for (int i = 0; i < obstacleMaps.Length; i++)
        {
            tilemaps[i] = Instantiate(obstacleMaps[i]);

            tilemaps[i].SetActive(false);

            tilemaps[i].transform.parent = grid.transform;
        }
    }

    private void Generation(float xAdd, int iterationNum)
    {
        //Debug.Log(xAdd);

        //GameObject tilemap;


        currentActiveTilemaps[iterationNum] = randomObstacleMapIndex();

        mapColliders[iterationNum] = tilemaps[currentActiveTilemaps[iterationNum]].transform.GetChild(0).GetComponent<Collider2D>();


        //GameObject obstacleMap = tilemaps[currentActiveTilemaps[iterationNum]];

        tilemaps[currentActiveTilemaps[iterationNum]].SetActive(true);

        tilemaps[currentActiveTilemaps[iterationNum]].transform.position = new Vector3(xAdd, tilemaps[currentActiveTilemaps[iterationNum]].transform.position.y, tilemaps[currentActiveTilemaps[iterationNum]].transform.position.z);

        for (int i = 0; i < tilemaps[currentActiveTilemaps[iterationNum]].transform.childCount; i++)
        {
            tilemaps[currentActiveTilemaps[iterationNum]].transform.GetChild(i).transform.localPosition = obstacleMaps[currentActiveTilemaps[iterationNum]].transform.GetChild(i).transform.localPosition;

            tilemaps[currentActiveTilemaps[iterationNum]].transform.GetChild(i).transform.localRotation = obstacleMaps[currentActiveTilemaps[iterationNum]].transform.GetChild(i).transform.localRotation;
        }



        /*
        
        GameObject obstacleMap = obstacleMaps[currentActiveTilemaps[iterationNum]];

        //Debug.Log("obstacleMap.transform.position.x = " + obstacleMap.transform.position.x);


        tilemap = Instantiate(obstacleMap, new Vector3(obstacleMap.transform.position.x + xAdd, obstacleMap.transform.position.y, obstacleMap.transform.position.z), obstacleMap.transform.rotation);
        

        //tilemap.transform.position = new Vector3(tilemap.transform.position.x + xAdd, tilemap.transform.position.y, tilemap.transform.position.z);

        tilemap.transform.parent = grid.transform;
        //caveTilemap.transform.position = new Vector3(caveTilemap.transform.position.x + xAdd, caveTilemap.transform.position.y, caveTilemap.transform.position.z);
        */

        width += checkpointWidthAddition;

        widthAdditionMultiplier++;

        //return tilemap;
    }


    private void clearAllMap()
    {
        /*
        firstGroundTilemap.ClearAllTiles();
        firstCaveTilemap.ClearAllTiles();
        */

    }

    private void clearScpecificMap(Tilemap tilemapToClear)
    {
        tilemapToClear.ClearAllTiles();
    }

    private void NextActiveTilemap()
    {
        if (currentPlayerTilemap < 3)
        {
            currentPlayerTilemap++;
        }
        else
        {
            currentPlayerTilemap = 1;
        }

        //Debug.Log("currentPlayerTilemap = " + currentPlayerTilemap);
    }

    private bool IsTilemapCollidingWithPlayer()
    {
        bool groundCollision;

        bool caveCollision;


        if (currentPlayerTilemap < 3)
        {
            groundCollision = tilemaps[currentActiveTilemaps[currentPlayerTilemap]].transform.GetChild(0).GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
            caveCollision = tilemaps[currentActiveTilemaps[currentPlayerTilemap]].transform.GetChild(1).GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
        }
        else
        {
            groundCollision = tilemaps[currentActiveTilemaps[0]].transform.GetChild(0).GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
            caveCollision = tilemaps[currentActiveTilemaps[0]].transform.GetChild(1).GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
        }



        if (groundCollision || caveCollision)
        {
            NextActiveTilemap();

            //Debug.Log("active map = " + currentPlayerTilemap);

            return true;
        }


        return false;

    }


    private int randomObstacleMapIndex()
    {
        int loopLimit = currentActiveTilemaps.Length * 2;

        int randomNum;

        randomNum = UnityEngine.Random.Range(0, tilemaps.Length);

        //Debug.Log("Random Num: " + randomNum);

        while (tilemaps[randomNum].activeSelf)
        {
            randomNum = UnityEngine.Random.Range(0, tilemaps.Length);

            //Debug.Log("Random Num: " + randomNum);
        }

        /*
        for (int j = 0; j < currentActiveTilemaps.Length; j++)
        {
            if (currentActiveTilemaps[j] == randomNum)
            {
                j = -1;

                randomNum = UnityEngine.Random.Range(0, obstacleMaps.Length);
            }

            loopLimit--;

            if(loopLimit == 0)
            {
                j = currentActiveTilemaps.Length;
            }
        }

        //Debug.Log("Random Number Generated: " + randomNum);
        */


        return randomNum;

    }

    private bool isMapInCameraView(Vector3 objectCollider)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        var point = objectCollider;

        for (int i = 0; i < 6; ++i)
        {
            if (i != 2 || i != 3)
            {
                if (planes[i].GetDistanceToPoint(point) < 0)
                {
                    return false;
                }
            }

        }

        return true;


    }

    private void TimeCountDown()
    {
        if (PlayerStats.Instance.timeLeft > 0)
        {
            PlayerStats.Instance.timeLeft -= Time.deltaTime;
        }
        else if (PlayerStats.Instance.timeLeft < 0)
        {
            PlayerStats.Instance.timeLeft = 0;
        }

        timerText.text = MathF.Round(PlayerStats.Instance.timeLeft).ToString();
    }

    private void TilemapSwapping()
    {
        GameObject mapToTrunOff;

        if (newestMap == 3 && oldestMap == 1)
        {
            distanceOfPlayerToMiddleMap = player.transform.position.x - tilemaps[currentActiveTilemaps[1]].transform.GetChild(0).transform.gameObject.GetComponent<Collider2D>().bounds.center.x;

            //Debug.Log("Map: " + 2);


            if (distanceOfPlayerToMiddleMap > 10)
            {
                //StartCoroutine(DeleteMap(tilemaps[oldestMap - 1]));

                //Destroy(tilemaps[oldestMap - 1]);

                //tilemaps[oldestMap - 1] = Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), oldestMap - 1);

                //tilemaps[currentActiveTilemaps[oldestMap - 1]].SetActive(false);

                mapToTrunOff = tilemaps[currentActiveTilemaps[oldestMap - 1]];

                Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), oldestMap - 1);

                mapToTrunOff.SetActive(false);

                startTiles.transform.position = tilemaps[currentActiveTilemaps[oldestMap]].transform.position;

                /*
                distanceCovered[oldestMap - 1].transform.position = new Vector3(tilemaps[oldestMap - 1].transform.position.x + (width - (checkpointWidthAddition * widthAdditionMultiplier)) + 19.05f, distanceCoveredPrefab.transform.position.y, distanceCoveredPrefab.transform.position.z);

                distanceCovered[oldestMap - 1].transform.GetChild(0).gameObject.GetComponent<Text>().text = ((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier).ToString() + "m";
                */

                newestMap = oldestMap;

                oldestMap = oldestMap + 1;
            }

        }
        else if (newestMap == 1 && oldestMap == 2)
        {
            distanceOfPlayerToMiddleMap = player.transform.position.x - tilemaps[currentActiveTilemaps[2]].transform.GetChild(0).transform.gameObject.GetComponent<Collider2D>().bounds.center.x;

            //Debug.Log("Map: " + 3);


            if (distanceOfPlayerToMiddleMap > 10)
            {
                //StartCoroutine(DeleteMap(tilemaps[oldestMap - 1]));

                //Destroy(tilemaps[oldestMap - 1]);

                //tilemaps[oldestMap - 1] = Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), oldestMap - 1);

                //tilemaps[currentActiveTilemaps[oldestMap - 1]].SetActive(false);

                mapToTrunOff = tilemaps[currentActiveTilemaps[oldestMap - 1]];

                Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), oldestMap - 1);

                mapToTrunOff.SetActive(false);

                startTiles.transform.position = tilemaps[currentActiveTilemaps[oldestMap]].transform.position;

                /*
                distanceCovered[oldestMap - 1].transform.position = new Vector3(tilemaps[oldestMap - 1].transform.position.x + (width - (checkpointWidthAddition * widthAdditionMultiplier)) + 19.05f, distanceCoveredPrefab.transform.position.y, distanceCoveredPrefab.transform.position.z);

                distanceCovered[oldestMap - 1].transform.GetChild(0).gameObject.GetComponent<Text>().text = ((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier).ToString() + "m";
                */

                newestMap = oldestMap;

                oldestMap = oldestMap + 1;
            }

        }
        else if (newestMap == 2 && oldestMap == 3)
        {
            distanceOfPlayerToMiddleMap = player.transform.position.x - tilemaps[currentActiveTilemaps[0]].transform.GetChild(0).transform.gameObject.GetComponent<Collider2D>().bounds.center.x;

            //Debug.Log("Map: " + 1);


            if (distanceOfPlayerToMiddleMap > 10)
            {
                //StartCoroutine(DeleteMap(tilemaps[oldestMap - 1]));

                //Destroy(tilemaps[oldestMap - 1]);

                //tilemaps[oldestMap - 1] = Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), oldestMap - 1);

                //tilemaps[currentActiveTilemaps[oldestMap - 1]].SetActive(false);

                mapToTrunOff = tilemaps[currentActiveTilemaps[oldestMap - 1]];

                Generation((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier + (tilemapDistance * widthAdditionMultiplier), oldestMap - 1);

                mapToTrunOff.SetActive(false);

                //startTiles.transform.position = tilemaps[currentActiveTilemaps[oldestMap]].transform.position;

                /*
                distanceCovered[oldestMap - 1].transform.position = new Vector3(tilemaps[oldestMap - 1].transform.position.x + (width - (checkpointWidthAddition * widthAdditionMultiplier)) + 19.05f, distanceCoveredPrefab.transform.position.y, distanceCoveredPrefab.transform.position.z);

                distanceCovered[oldestMap - 1].transform.GetChild(0).gameObject.GetComponent<Text>().text = ((width - (checkpointWidthAddition * widthAdditionMultiplier)) * widthAdditionMultiplier).ToString() + "m";
                */

                newestMap = oldestMap;

                oldestMap = 1;

                startTiles.transform.position = tilemaps[currentActiveTilemaps[oldestMap - 1]].transform.position;
            }

        }
    }

    private void CheckpointMoving()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (i == 0)
            {
                if (checkpoints[i].gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer() && checkpointCheck[i])
                {
                    //Debug.Log("Checkpoint 1");

                    checkpointCheck[i] = false;

                    checkpointCheck[i + 1] = true;

                    startingPosition = checkpoints[i].transform.position;

                    checkpoints[i].transform.position = new Vector3(checkpoints[i + 2].transform.position.x + width + tilemapDistance, 5, checkpoints[i + 2].transform.position.z);

                    startingWidth += checkpointWidthAddition;

                    checkpointDistanceUpdate = 2;

                    checkpointCrossed = true;

                    PlayerStats.Instance.playerScore++;

                    checkpointLastDistance = startingWidth;
                }
            }
            else if (i == 1)
            {
                if (checkpoints[i].gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer() && checkpointCheck[i])
                {
                    //Debug.Log("Checkpoint 2");

                    checkpointCheck[i] = false;

                    checkpointCheck[i + 1] = true;

                    startingPosition = checkpoints[i].transform.position;

                    checkpoints[i].transform.position = new Vector3(checkpoints[i - 1].transform.position.x + width + tilemapDistance, 5, checkpoints[i - 1].transform.position.z);

                    startingWidth += checkpointWidthAddition;

                    checkpointDistanceUpdate = 3;

                    checkpointCrossed = true;

                    PlayerStats.Instance.playerScore++;

                    checkpointLastDistance = startingWidth;
                }
            }
            else
            {
                if (checkpoints[i].gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer() && checkpointCheck[i])
                {
                    //Debug.Log("Checkpoint 3");

                    checkpointCheck[i] = false;

                    checkpointCheck[i - 2] = true;

                    startingPosition = checkpoints[i].transform.position;

                    checkpoints[i].transform.position = new Vector3(checkpoints[i - 1].transform.position.x + width + tilemapDistance, 5, checkpoints[i - 1].transform.position.z);

                    startingWidth += checkpointWidthAddition;

                    checkpointDistanceUpdate = 1;

                    checkpointCrossed = true;

                    PlayerStats.Instance.playerScore++;

                    checkpointLastDistance = startingWidth;
                }
            }
        }

        checkpointDistanceUpdater();
    }

    private void checkpointDistanceUpdater()
    {
        if (checkpointDistanceUpdate == 1)
        {

            if (!((checkpoints[0].transform.position.x - startingPosition.x) <= 0))
            {
                PlayerStats.Instance.checkpointDistance = Mathf.RoundToInt(((checkpoints[0].transform.position.x - player.transform.position.x) / (checkpoints[0].transform.position.x - startingPosition.x)) * startingWidth);

                checkpointText.text = PlayerStats.Instance.checkpointDistance.ToString() + "m";
            }
            else
            {
                checkpointText.text = "0";
            }

        }
        else if (checkpointDistanceUpdate == 2)
        {
            if (!((checkpoints[1].transform.position.x - startingPosition.x) <= 0))
            {
                PlayerStats.Instance.checkpointDistance = Mathf.RoundToInt(((checkpoints[1].transform.position.x - player.transform.position.x) / (checkpoints[1].transform.position.x - startingPosition.x)) * startingWidth);

                checkpointText.text = PlayerStats.Instance.checkpointDistance.ToString() + "m";
            }
            else
            {
                checkpointText.text = "0";
            }
        }
        else
        {
            if (!((checkpoints[2].transform.position.x - startingPosition.x) <= 0))
            {
                PlayerStats.Instance.checkpointDistance = Mathf.RoundToInt(((checkpoints[2].transform.position.x - player.transform.position.x) / (checkpoints[2].transform.position.x - startingPosition.x)) * startingWidth);

                checkpointText.text = PlayerStats.Instance.checkpointDistance.ToString() + "m";
            }
            else
            {
                checkpointText.text = "0";
            }
        }

        if (checkpointCrossed)
        {
            //Debug.Log("checkpointCrossed = " + checkpointCrossed);

            checkpointCrossed = false;

            PlayerStats.Instance.timeLeft += timeCheckpointAddition;
        }
    }

    private void ScoreCalculator()
    {
        if ((PlayerStats.Instance.checkpointDistance - checkpointLastDistance) < 0)
        {
            PlayerStats.Instance.playerScore += Mathf.Abs(PlayerStats.Instance.checkpointDistance - checkpointLastDistance);

            checkpointLastDistance = PlayerStats.Instance.checkpointDistance;

            playerScoreText.text = Mathf.RoundToInt(PlayerStats.Instance.playerScore).ToString();
        }


    }

    IEnumerator DeleteMap(GameObject tileMapToDestroy)
    {
        yield return new WaitForSeconds(5f);

        Destroy(tileMapToDestroy);
    }
}
