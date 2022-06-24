using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width, height;
    float tilemapDistance = 0.28f;
    [SerializeField] int widthAddition;
    private int widthAdditionMultiplier;
    [SerializeField] float smoothness;
    [SerializeField] private float seed;
    [SerializeField] TileBase groundTile, caveTile;
    [SerializeField] GameObject firstTilemap;
    [SerializeField] GameObject secondTilemap;
    [SerializeField] GameObject thirdTilemap;

    private Tilemap firstGroundTilemap, firstCaveTilemap;
    private Tilemap secondGroundTilemap, secondCaveTilemap;
    private Tilemap thirdGroundTilemap, thirdCaveTilemap;

    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] Text checkpointText;
    [SerializeField] GameObject player;


    private GameObject firstCheckpoint;
    private GameObject secondCheckpoint;
    private GameObject thirdCheckpoint;

    private bool firstCheckpointCheck;
    private bool secondCheckpointCheck;
    private bool thirdCheckpointCheck;

    [Header("Caves")]
    [Range(0, 1)]
    [SerializeField] float modifier;

    private bool firstTilemapCheck;

    private bool startTransformingTilemaps;

    public static int currentActiveTilemap;

    private Vector3 startingPosition;

    private int startingWidth;


    int[,] map;

    private void Start()
    {
        tilemapDistance *= width;

        startingWidth = width;

        startingPosition = player.transform.position;

        startTransformingTilemaps = false;

        firstCheckpointCheck = true;
        secondCheckpointCheck = false;
        thirdCheckpointCheck = false;
        
        widthAdditionMultiplier = 0;

        firstGroundTilemap = firstTilemap.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        firstCaveTilemap = firstTilemap.transform.GetChild(1).gameObject.GetComponent<Tilemap>();

        secondGroundTilemap = secondTilemap.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        secondCaveTilemap = secondTilemap.transform.GetChild(1).gameObject.GetComponent<Tilemap>();

        thirdGroundTilemap = thirdTilemap.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        thirdCaveTilemap = thirdTilemap.transform.GetChild(1).gameObject.GetComponent<Tilemap>();


        clearAllMap();

        firstTilemapCheck = true;

        currentActiveTilemap = 1;

        Generation(firstGroundTilemap, firstCaveTilemap, 0);

        firstCheckpoint = Instantiate(checkpointPrefab, new Vector3(firstGroundTilemap.transform.position.x + (width - widthAddition) + tilemapDistance, height, firstGroundTilemap.transform.position.z), checkpointPrefab.transform.rotation);

        Generation(secondGroundTilemap, secondCaveTilemap, (width - widthAddition) + tilemapDistance + (2.8f* (widthAdditionMultiplier - 1)));

        secondCheckpoint = Instantiate(checkpointPrefab, new Vector3(secondGroundTilemap.transform.position.x + (width - widthAddition) + tilemapDistance + (2.8f * (widthAdditionMultiplier - 1)), height, secondGroundTilemap.transform.position.z), checkpointPrefab.transform.rotation);

        Generation(thirdGroundTilemap, thirdCaveTilemap, secondGroundTilemap.transform.position.x + (width - widthAddition) + tilemapDistance + (2.8f * (widthAdditionMultiplier - 1)));

        thirdCheckpoint = Instantiate(checkpointPrefab, new Vector3(thirdGroundTilemap.transform.position.x + (width - widthAddition) + tilemapDistance + (2.8f * (widthAdditionMultiplier - 1)), height, thirdGroundTilemap.transform.position.z), checkpointPrefab.transform.rotation);

    }

    private void Update()
    {
        IsTilemapCollidingWithPlayer();

        //Debug.Log("Active Tilemap: " + currentActiveTilemap);

        if(!startTransformingTilemaps)
        {
            if(secondCheckpoint.gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer())
            {
                startTransformingTilemaps = true;

                secondCheckpointCheck = true;
            }
            else if (firstCheckpoint.gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer() && firstCheckpointCheck)
            {
                firstCheckpointCheck = false;

                startingWidth += widthAddition;

                startingPosition = firstCheckpoint.transform.position;
            }
        }

        //checkpointText.text = 


        if (currentActiveTilemap == 1)
        {
            if(!((firstCheckpoint.transform.position.x - startingPosition.x) <= 0))
            {
                checkpointText.text = (Mathf.RoundToInt(((firstCheckpoint.transform.position.x - player.transform.position.x) / (firstCheckpoint.transform.position.x - startingPosition.x)) * startingWidth)).ToString();
            }
            else
            {
                checkpointText.text = "0";
            }
            
        }
        else if(currentActiveTilemap == 2)
        {
            if (!((secondCheckpoint.transform.position.x - startingPosition.x) <= 0))
            {
                checkpointText.text = (Mathf.RoundToInt(((secondCheckpoint.transform.position.x - player.transform.position.x) / (secondCheckpoint.transform.position.x - startingPosition.x)) * startingWidth)).ToString();
            }
            else
            {
                checkpointText.text = "0";
            }
        }
        else
        {
            if (!((thirdCheckpoint.transform.position.x - startingPosition.x) <= 0))
            {
                checkpointText.text = (Mathf.RoundToInt(((thirdCheckpoint.transform.position.x - player.transform.position.x) / (thirdCheckpoint.transform.position.x - startingPosition.x)) * startingWidth)).ToString();
            }
            else
            {
                checkpointText.text = "0";
            }
        }
        

        if (startTransformingTilemaps)
        {
            if (firstCheckpoint.gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer() && firstCheckpointCheck)
            {
                firstCheckpointCheck = false;

                secondCheckpointCheck = true;

                TransformTilemap(thirdGroundTilemap, thirdCaveTilemap, secondGroundTilemap, secondCaveTilemap);
                thirdCheckpoint.transform.position = new Vector3(thirdGroundTilemap.transform.position.x + (width - widthAddition) + tilemapDistance + (2.8f * (widthAdditionMultiplier - 1)), height, thirdGroundTilemap.transform.position.z);

                startingWidth += widthAddition;

                startingPosition = firstCheckpoint.transform.position;
            }
            else if(secondCheckpoint.gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer() && secondCheckpointCheck)
            {
                secondCheckpointCheck = false;

                thirdCheckpointCheck = true;

                TransformTilemap(firstGroundTilemap, firstCaveTilemap, thirdGroundTilemap, thirdCaveTilemap);
                firstCheckpoint.transform.position = new Vector3(firstGroundTilemap.transform.position.x + (width - widthAddition) + tilemapDistance + (2.8f * (widthAdditionMultiplier - 1)), height, firstGroundTilemap.transform.position.z);

                startingWidth += widthAddition;

                startingPosition = secondCheckpoint.transform.position;
            }
            else if (thirdCheckpoint.gameObject.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer() && thirdCheckpointCheck)
            {
                thirdCheckpointCheck = false;

                firstCheckpointCheck = true;

                TransformTilemap(secondGroundTilemap, secondCaveTilemap, firstGroundTilemap, firstCaveTilemap);
                secondCheckpoint.transform.position = new Vector3(secondGroundTilemap.transform.position.x + (width - widthAddition) + tilemapDistance + (2.8f * (widthAdditionMultiplier - 1)), height, secondGroundTilemap.transform.position.z);

                startingWidth += widthAddition;

                startingPosition = thirdCheckpoint.transform.position;
            }
        }
    }

    private void Generation(Tilemap groundTilemap, Tilemap caveTilemap, float xAdd)
    {
        seed = UnityEngine.Random.Range(-10000, 10000);

        //Debug.Log(xAdd);

        groundTilemap.transform.position = new Vector3(groundTilemap.transform.position.x + xAdd, groundTilemap.transform.position.y, groundTilemap.transform.position.z);
        caveTilemap.transform.position = new Vector3(caveTilemap.transform.position.x + xAdd, caveTilemap.transform.position.y, caveTilemap.transform.position.z);

        map = GenerateArray(width, height, true);
        map = TerrainGeneration(map);

        RenderMap(map, groundTilemap, caveTilemap, groundTile, caveTile);

        width += widthAddition;

        widthAdditionMultiplier++;
    }

    public int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];

        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (empty) ? 0 : 1;
            }
        }

        return map;
    }

    public int[,] TerrainGeneration(int[,] map)
    {
        int perlinHeight;

        for (int x = 0; x < width; x++)
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, seed) * height / 2);

            perlinHeight += height / 2;

            for (int y = 0; y < perlinHeight; y++)
            {
                //map[x, y] = 1;

                int caveValue = Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));

                map[x, y] = (caveValue == 1) ? 2 : 1;
            }
        }

        return map;
    }

    private void RenderMap(int[,] map, Tilemap groundTilemap, Tilemap caveTilemap, TileBase groundTilebase, TileBase caveTilebase)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(x > width * 0.05)
                {
                    if (map[x, y] == 1)
                    {
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                    }
                    else if (map[x, y] == 2)
                    {
                        if ((y > height * 0.5) && (y < (height / 1.4)))
                        {
                            caveTilemap.SetTile(new Vector3Int(x, y, 0), caveTilebase);
                        }
                        else
                        {
                            groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                        }

                    }
                }
                else
                {
                    if (firstTilemapCheck && (y < height * 0.9))
                    {
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                    }
                    else
                    {
                        if (map[x, y] == 1)
                        {
                            groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                        }
                        else if (map[x, y] == 2)
                        {
                            if ((y > height * 0.5) && (y < (height / 1.4)))
                            {
                                caveTilemap.SetTile(new Vector3Int(x, y, 0), caveTilebase);
                            }
                            else
                            {
                                groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                            }

                        }
                    }
                    
                }

                

            }
        }

        firstTilemapCheck = false;
    }

    private void clearAllMap()
    {
        firstGroundTilemap.ClearAllTiles();
        firstCaveTilemap.ClearAllTiles();

        firstGroundTilemap.ClearAllTiles();
        firstCaveTilemap.ClearAllTiles();

        secondGroundTilemap.ClearAllTiles();
        secondCaveTilemap.ClearAllTiles();

        thirdGroundTilemap.ClearAllTiles();
        thirdCaveTilemap.ClearAllTiles();
    }

    private void clearScpecificMap(Tilemap tilemapToClear)
    {
        tilemapToClear.ClearAllTiles();
    }

    private void NextActiveTilemap()
    {
        if(currentActiveTilemap != 3)
        {
            currentActiveTilemap++;
        }
        else
        {
            currentActiveTilemap = 1;
        }
        
    }

    private bool IsTilemapCollidingWithPlayer()
    {
        bool groundCollision;

        bool caveCollision;

        if (currentActiveTilemap == 1)
        {
            groundCollision = secondGroundTilemap.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
            caveCollision = secondCaveTilemap.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
        }
        else if (currentActiveTilemap == 2)
        {
            groundCollision = thirdGroundTilemap.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
            caveCollision = thirdCaveTilemap.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
        }
        else
        {
            groundCollision = firstGroundTilemap.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
            caveCollision = firstCaveTilemap.GetComponent<TilemapColliisionChecker>().IsCollidingWithPlayer();
        }

        if (groundCollision || caveCollision)
        {
            NextActiveTilemap();

            return true;
        }


        return false;

    }

    private void TransformTilemap(Tilemap lastGroundTilemap, Tilemap lastCaveTilemap, Tilemap firstGroundTilemap, Tilemap firstCaveTilemap)
    {
        clearScpecificMap(lastGroundTilemap);
        clearScpecificMap(lastCaveTilemap);

        Generation(lastGroundTilemap, lastCaveTilemap, (firstGroundTilemap.transform.position.x - lastGroundTilemap.transform.position.x) + (width - widthAddition) + tilemapDistance + (2.8f * (widthAdditionMultiplier - 1)));

    }
}
