using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerModelSelect : MonoBehaviour
{
    public GameObject playerModelFBX;

    //Dictionary<string, GameObject> playerModelFBX = new Dictionary<string, GameObject>();

    /*
    [SerializeField]
    private GameObject[] playerModelFBX;
    */


    private Animator playerAnimator;

    public string chosenNFTName;

    private string defaultNFTName;

    private PolygonCollider2D parentCollider;

    private GameObject geometry;

    private SkinnedMeshRenderer defaultModelSMR;

    private GameObject groundCheck;

    private GameObject armature;

    //private int selectedModel;

    // Start is called before the first frame update
    void Start()
    {
        parentCollider = gameObject.transform.parent.gameObject.GetComponent<PolygonCollider2D>();

        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
        
        /*
        defaultNFTName = "a-rod";

        GameObject defaultGameObject = (Resources.Load(Path.Combine(("SinglePlayerPrefabs/FIGHTERS2.0Redone/" + chosenNFTName), chosenNFTName)) as GameObject);

        defaultGameObject.transform.localScale = new Vector3(gameObject.transform.parent.transform.localScale.y / 10, gameObject.transform.parent.transform.localScale.y / 10, gameObject.transform.parent.transform.localScale.z / 10);

        armature = Instantiate(defaultGameObject.transform.GetChild(0).gameObject);
        armature.name = "Armature";
        */

        //defaultModelSMR = defaultGameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();


        try
        {
            chosenNFTName = NameToSlugConvert(gameplayView.instance.chosenNFT.name);
        }
        catch (Exception e)
        {
            //chosenNFTName = "a-rod";


            //Smaller

            //chosenNFTName = "big-bite";

            //chosenNFTName = "crisp-right";

            //chosenNFTName = "arachnid";

            //chosenNFTName = "bad-man";


            //Bigger

            //chosenNFTName = "android";

            //chosenNFTName = "alpha-cat";

            //chosenNFTName = "aegis";

            //chosenNFTName = "awoken-one";

            //chosenNFTName = "bad-news";

            //chosenNFTName = "bandit";

            //chosenNFTName = "barista";


        }



        //playerModelFBX = Resources.Load(Path.Combine("SinglePlayerPrefabs/DisplayModels", chosenNFTName)) as GameObject;

        playerModelFBX = Resources.Load(Path.Combine(("SinglePlayerPrefabs/FIGHTERS2.0Redone/" + chosenNFTName), chosenNFTName)) as GameObject;

        /*
        int randomNum;

        randomNum = UnityEngine.Random.Range(0, playerModelFBX.Length - 1);
        */

        SpawnModel();

        //HeightAdjuster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnModel()
    {
        playerModelFBX.transform.localScale = new Vector3(gameObject.transform.parent.transform.localScale.y / 10, gameObject.transform.parent.transform.localScale.y / 10, gameObject.transform.parent.transform.localScale.z / 10);

        playerModelFBX.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.x, gameObject.transform.position.x);
        
        
        GameObject playerModel = Instantiate(playerModelFBX);


        playerModel.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        playerAnimator = gameObject.GetComponent<Animator>();


        //playerModel.gameObject.transform.GetChild(0).transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        //Debug.Log(playerModel.gameObject.transform.GetChild(0).name + "position: " + playerModel.gameObject.transform.GetChild(0).transform.position);



        playerModel.gameObject.transform.GetChild(0).SetParent(gameObject.transform);


        //armature.transform.SetParent(gameObject.transform);



        //geometry = playerModel.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;


        geometry = playerModel.gameObject.transform.GetChild(0).gameObject;


        //geometry = playerModel.gameObject.transform.GetChild(1).gameObject;

        //geometry.GetComponent<SkinnedMeshRenderer>().rootBone = armature.transform.GetChild(0);

        //geometry.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);


        geometry.transform.SetParent(gameObject.transform);

        playerAnimator.avatar = playerModelFBX.GetComponent<Animator>().avatar;

        //Debug.Log(gameObject.transform.GetChild(0).name + "position: " + gameObject.transform.GetChild(0).transform.position);

        Destroy(playerModel);

        if(gameObject.transform.GetChild(0).transform.localPosition.z != 0)
        {
            Debug.Log(playerModelFBX.name + " pivot NOT centered");

            gameObject.transform.parent.transform.GetChild(5).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log(playerModelFBX.name + " pivot centered");
        }
        
    }

    string NameToSlugConvert(string name)
    {
        string slug;
        slug = name.ToLower().Replace(".", "").Replace("'", "").Replace(" ", "-");
        return slug;

    }

    private void HeightAdjuster()
    {

        SkinnedMeshRenderer modelSMR = geometry.GetComponent<SkinnedMeshRenderer>();

        float defaultHeight = defaultModelSMR.bounds.size.y;

        Vector3 modelSize = modelSMR.bounds.size;


        float heightRatio = modelSize.y / defaultHeight;

        //Debug.Log("heightRatio = " + heightRatio);

        float centerOffsetCalculation;

        float groundCheckCenterOffsetCalculation;

        float pointOffsetCalculation;

        float heightCenterOffsetRatio;

        centerOffsetCalculation = (modelSMR.bounds.center.y - defaultModelSMR.bounds.center.y);

        centerOffsetCalculation = Mathf.Round(centerOffsetCalculation * 10000f) / 10000f;

        groundCheckCenterOffsetCalculation = centerOffsetCalculation;

        //Debug.Log("centerOffsetCalculation = " + centerOffsetCalculation);

        if (centerOffsetCalculation == 0)
        {
            //Debug.Log("Same");
            /*
            Vector2[] modelPaths = new Vector2[parentCollider.GetPath(0).Length];

            for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
            {
                modelPaths[i] = parentCollider.GetPath(0)[i];

                modelPaths[i].y += 0.005f;
            }

            parentCollider.SetPath(0, modelPaths);
            */
        }
        else
        {
            heightCenterOffsetRatio = heightRatio / centerOffsetCalculation;

            //Debug.Log("hCOffsetRatio = " + heightCenterOffsetRatio);


            if ((heightRatio < 0.98f || heightRatio > 1.04f) || (centerOffsetCalculation > 0.04f))
            {
                Vector2[] modelPaths = new Vector2[parentCollider.GetPath(0).Length];

                if (heightRatio < 1)
                {
                    //Debug.Log("Smaller");

                    for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
                    {
                        modelPaths[i] = parentCollider.GetPath(0)[i];

                        if(centerOffsetCalculation < 0.1f)
                        {
                            /*
                            if (heightCenterOffsetRatio < 25)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / ((heightCenterOffsetRatio * 2) / 15);

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            else if (heightCenterOffsetRatio > 50)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 1.5f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            else
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 1.85f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            */

                            if (heightCenterOffsetRatio < 13 && heightCenterOffsetRatio > 12)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 2f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            else if (heightCenterOffsetRatio < 14 && heightCenterOffsetRatio > 13)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 1.5f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            else if (heightCenterOffsetRatio < 15)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 6f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                                
                            }
                            else if (heightCenterOffsetRatio < 20)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 2.3f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            else if (heightCenterOffsetRatio < 25)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 1.5f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            else if (heightCenterOffsetRatio > 50)
                            {
                                if (heightRatio > 0.97 && heightCenterOffsetRatio > 195)
                                {
                                    groundCheckCenterOffsetCalculation = 0.01f;

                                    modelPaths[i].y += groundCheckCenterOffsetCalculation;
                                }
                                else
                                {
                                    groundCheckCenterOffsetCalculation = centerOffsetCalculation / 1.5f;

                                    modelPaths[i].y += groundCheckCenterOffsetCalculation;
                                }

                            }
                            else
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 1.85f;

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;

                            }
                            
                        }
                        else
                        {
                            centerOffsetCalculation = 0;

                            //modelPaths[i].y -= centerOffsetCalculation;
                        }



                    }

                    //Debug.Log((modelPaths[3].y - (modelPaths[3].y * heightRatio)));

                    //Debug.Log("Before = " + modelPaths[3].y);

                    pointOffsetCalculation = (modelPaths[3].y - (modelPaths[3].y * heightRatio));

                    pointOffsetCalculation = Mathf.Round(pointOffsetCalculation * 10000f) / 10000f;

                    if((centerOffsetCalculation > 0.065 && centerOffsetCalculation < 0.07) && (heightCenterOffsetRatio < 15))
                    {
                        if (pointOffsetCalculation < -0.005 && pointOffsetCalculation > -0.006)
                        {
                            for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
                            {
                                modelPaths[i].y += 0.03f;
                            }
                        }
                        else if (pointOffsetCalculation < -0.01 && pointOffsetCalculation > -0.011)
                        {
                            for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
                            {
                                modelPaths[i].y += 0.02f;
                            }
                        }
                    }

                    //Debug.Log("pointOffsetCalculation = " + pointOffsetCalculation);

                    modelPaths[2].y -= pointOffsetCalculation;

                    modelPaths[3].y -= pointOffsetCalculation;

                    modelPaths[4].y -= pointOffsetCalculation;
                }
                else
                {
                    //Debug.Log("Bigger");

                    for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
                    {
                        modelPaths[i] = parentCollider.GetPath(0)[i];

                        /*
                        if (centerOffsetCalculation > 0.08)
                        {
                            modelPaths[i].y += centerOffsetCalculation / 3f;
                        }
                        */
                        if (centerOffsetCalculation > 0.04)
                        {
                            /*
                            if (heightCenterOffsetRatio > 20)
                            {
                                modelPaths[i].y += centerOffsetCalculation / 3f;
                            }
                            */

                            if (heightCenterOffsetRatio > 18 && heightCenterOffsetRatio < 19)
                            {
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 5.5f;

                                modelPaths[i].y -= groundCheckCenterOffsetCalculation;

                                groundCheckCenterOffsetCalculation *= -1;
                            }
                            else if (heightCenterOffsetRatio > 14 && heightCenterOffsetRatio < 15)
                            {
                                /*
                                groundCheckCenterOffsetCalculation = centerOffsetCalculation / 3.4f;

                                modelPaths[i].y -= groundCheckCenterOffsetCalculation;

                                groundCheckCenterOffsetCalculation *= -1;
                                */
                                if(heightRatio > 1.03)
                                {
                                    groundCheckCenterOffsetCalculation = centerOffsetCalculation / 3f;

                                    modelPaths[i].y += groundCheckCenterOffsetCalculation;
                                }
                                else
                                {
                                    //Debug.Log("Changed");

                                    groundCheckCenterOffsetCalculation = 0.005f;

                                    modelPaths[i].y += groundCheckCenterOffsetCalculation;

                                    //centerOffsetCalculation = 0;
                                }
                                
                            }
                            else if ((heightCenterOffsetRatio > 12 && heightCenterOffsetRatio < 13) && ((centerOffsetCalculation > 0.085) && (centerOffsetCalculation < 0.09)))
                            {
                                centerOffsetCalculation = 0;
                            }
                            else
                            {
                                if(heightRatio > 1.1f && centerOffsetCalculation > 0.1)
                                {
                                    //groundCheckCenterOffsetCalculation = (centerOffsetCalculation / 3f) * -1;

                                    groundCheckCenterOffsetCalculation = (centerOffsetCalculation / 8f);
                                }
                                else
                                {
   
                                    if((heightRatio - 1) < 0.0001)
                                    {
                                        //Debug.Log("Changed");

                                        groundCheckCenterOffsetCalculation = 0.02f;
                                    }
                                    else
                                    {
                                        groundCheckCenterOffsetCalculation = centerOffsetCalculation / 3f;
                                    }
                                    
                                }
                                

                                modelPaths[i].y += groundCheckCenterOffsetCalculation;
                            }
                            
                        }
                    }

                    //centerOffsetCalculation = 0;

                    pointOffsetCalculation = 0;

                }

                groundCheck.transform.position = new Vector3(groundCheck.transform.position.x, groundCheck.transform.position.y + ((groundCheckCenterOffsetCalculation + (-1 * pointOffsetCalculation)) * 2), groundCheck.transform.position.z);


                parentCollider.SetPath(0, modelPaths);
            }
            else
            {
                //Debug.Log("Similar");

                Vector2[] modelPaths = new Vector2[parentCollider.GetPath(0).Length];

                if (heightCenterOffsetRatio > 50)
                {
                    for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
                    {
                        modelPaths[i] = parentCollider.GetPath(0)[i];
                        
                        if(heightCenterOffsetRatio > 55)
                        {
                            modelPaths[i].y += 0.01f;
                        }
                        else
                        {
                            modelPaths[i].y += 0.02f;
                        }

                    }

                }
                else if (heightCenterOffsetRatio < 30)
                {
                    
                    for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
                    {
                        modelPaths[i] = parentCollider.GetPath(0)[i];

                        if(heightRatio < 1)
                        {
                            modelPaths[i].y += 0.02f;
                        }
                        else if (heightCenterOffsetRatio > 25.68 && heightCenterOffsetRatio < 25.7)
                        {
                            modelPaths[i].y += 0.02f;
                        }
                        else if(heightCenterOffsetRatio > 26)
                        {
                            modelPaths[i].y += 0.02f;
                        }
                        //modelPaths[i].y -= 0.01f;

                    }
                    
                }
                else
                {
                    for (int i = 0; i < parentCollider.GetPath(0).Length; i++)
                    {
                        modelPaths[i] = parentCollider.GetPath(0)[i];

                        if(heightCenterOffsetRatio < 35)
                        {
                            modelPaths[i].y += 0.02f;
                        }
                        else if (heightCenterOffsetRatio > 39)
                        {
                            modelPaths[i].y += 0.02f;
                        }
                        else if (heightCenterOffsetRatio > 38)
                        {
                            modelPaths[i].y += 0.02f;
                        }
                        else
                        {
                            modelPaths[i].y -= 0.01f;
                        }
 
                    }
                }

                parentCollider.SetPath(0, modelPaths);
            }
        }

    }
}
