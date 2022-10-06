using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class ObstacleAttachScript : MonoBehaviour
    {
        
        private StarterAssetsInputs _input;

        private bool playerCollision;

        private bool playerAttached;

        private bool ropeDetachCoroutine;

        private Rigidbody2D playerRBody2D;

        private Collider2D playerCollider2D;

        private HingeJoint2D ropeHingeJoint;

        private GameObject handleObject;

        private GameObject previousHandleObject;

        private bool addJumpForce;

        private bool addClimbJumpForce;

        [SerializeField]
        private float jumpForce = 3.0f;

        //private bool ropeDetachCoroutine;

        private bool ropeObstacle;

        private bool rockClimbObstacle;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            playerRBody2D = gameObject.GetComponent<Rigidbody2D>();

            playerCollider2D = gameObject.GetComponent<Collider2D>();

            playerCollision = false;

            playerAttached = false;

            //ropeDetachCoroutine = false;

            ropeHingeJoint = null;

            previousHandleObject = null;

            addJumpForce = false;

            ropeObstacle = false;

            rockClimbObstacle = false;

            ropeDetachCoroutine = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(!playerCollision)
            {
                if(_input.ropeAttach)
                {
                    _input.ropeAttach = false;
                }

                if(ropeObstacle)
                {
                    ropeObstacle = false;
                }

                if (rockClimbObstacle)
                {
                    rockClimbObstacle = false;
                }

                handleObject = null;

                ropeHingeJoint = null;
            }
            else
            {
                if(ropeObstacle)
                {
                    /*
                    if (_input.ropeAttach && !playerAttached)
                    {
                        if (handleObject != null)
                        {
                            RopeAttach();
                        }
                    }
                    */

                    if (!playerAttached)
                    {
                        _input.ropeAttach = true;

                        if (handleObject != null)
                        {
                            RopeAttach();
                        }
                    }

                    if (playerAttached && !_input.ropeAttach)
                    {
                        RopeDeattach();
                    }
                    else if (playerAttached && _input.jump)
                    {
                        _input.ropeAttach = false;

                        RopeDeattach();
                    }
                    
                }
                else if(rockClimbObstacle)
                {
                    if (_input.ropeAttach && !playerAttached)
                    {
                        RockClimbAttach();
                    }
                    else if(playerAttached && _input.jump)
                    {
                        RockClimbDeattach();
                    }
                    else if (playerAttached && !_input.ropeAttach)
                    {
                        RockClimbDeattach();
                    }
                }
            }

            /*
            if(!_input.ropeAttach)
            {
                playerCollision = false;
            }
            */


            /*
            Debug.Log("playerCollision = " + playerCollision);
            Debug.Log("_input.ropeAttach = " + _input.ropeAttach);
            Debug.Log("playerAttached = " + playerAttached);
            Debug.Log("ropeObstacle = " + ropeObstacle);
            Debug.Log("rockClimbObstacle = " + rockClimbObstacle);
            */

            
            //Debug.Log("_input.jump = " + _input.jump);
        }

        private void FixedUpdate()
        {
            if(addJumpForce)
            {
                if(!addClimbJumpForce)
                {
                    playerRBody2D.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * jumpForce, ForceMode2D.Impulse);
                }
                else
                {
                    playerRBody2D.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * jumpForce * 2, ForceMode2D.Impulse);
                }


                addJumpForce = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.transform.CompareTag("Links"))
            {
                //Debug.Log(collision.gameObject.name);

                if (handleObject == null && !playerCollision)
                {
                    if (!collision.transform.parent.GetChild(0).GetComponent<HingeJoint2D>())
                    {
                        handleObject = collision.transform.parent.GetChild(0).gameObject;
                    }

                    if (handleObject.transform.parent.gameObject != previousHandleObject)
                    {
                        //Debug.Log("previous objet alert!");
                        playerCollision = true;

                        ropeObstacle = true;
                    }
                }

            }
            else if(collision.transform.CompareTag("Climable"))
            {
                if(!playerCollision)
                {
                    playerCollision = true;

                    rockClimbObstacle = true;
                }
            }
            else
            {
                playerCollision = false;

                handleObject = null;

                ropeHingeJoint = null;

                previousHandleObject = null;
            }
            
        }

        
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Links"))
            {
                //Debug.Log(collision.gameObject.name);

                if (handleObject == null && !playerCollision)
                {
                        if (!collision.transform.parent.GetChild(0).GetComponent<HingeJoint2D>())
                        {
                            handleObject = collision.transform.parent.GetChild(0).gameObject;
                        }

                        if (handleObject.transform.parent.gameObject != previousHandleObject)
                        {
                            playerCollision = true;

                            ropeObstacle = true;
                        }
                }


            }
            else if (collision.transform.CompareTag("Climable"))
            {
                if (!playerCollision)
                {
                    playerCollision = true;

                    rockClimbObstacle = true;
                }
            }
            else
            {
                playerCollision = false;

                handleObject = null;

                ropeHingeJoint = null;

                previousHandleObject = null;
            }
        }
        
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Links"))
            {
                //Debug.Log("Links Exit");

                if (!_input.ropeAttach && playerAttached)
                {
                    //Debug.Log("playerCollision = false");

                    playerCollision = false;

                    playerAttached = false;

                    ropeObstacle = false;
                }
                
            }
            else if (collision.transform.CompareTag("Climable"))
            {
                if (!_input.ropeAttach)
                {
                    playerCollision = false;

                    playerAttached = false;

                    rockClimbObstacle = false;
                }
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Climable"))
            {
                if (!playerCollision)
                {
                    playerCollision = true;

                    rockClimbObstacle = true;
                }
            }
            else
            {
                playerCollision = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Climable"))
            {
                if (!playerCollision)
                {
                    playerCollision = true;

                    rockClimbObstacle = true;
                }
            }
            else
            {
                playerCollision = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Climable"))
            {
                if (!_input.ropeAttach)
                {
                    playerCollision = false;

                    playerAttached = false;

                    rockClimbObstacle = false;
                }
            }
        }

        private void RopeAttach()
        {
            ThirdPersonController.canApplyGravity = false;

            gameObject.transform.position = new Vector3(handleObject.transform.position.x, handleObject.transform.position.y, gameObject.transform.position.z);

            ropeHingeJoint = handleObject.AddComponent<HingeJoint2D>();

            ropeHingeJoint.connectedBody = playerRBody2D;

            playerAttached = true;

            playerRBody2D.mass = 0.0f;

            playerCollider2D.enabled = false;
        }

        private void RopeDeattach()
        {
            Destroy(ropeHingeJoint);

            //playerAttached = false;

            playerCollision = false;

            playerRBody2D.mass = 1.0f;

            ropeHingeJoint = null;

            ThirdPersonController.canApplyGravity = true;

            addJumpForce = true;

            playerCollider2D.enabled = true;


            if (handleObject != null)
            {
                previousHandleObject = handleObject.transform.parent.gameObject;

                handleObject.transform.parent.GetComponent<StopCollisionsScript>().stopColliders();
            }


            handleObject = null;


            /*
            if (!ropeDetachCoroutine)
            {
                StartCoroutine(ropeDetachWait(0.1f));
            }
            */

            playerAttached = false;
        }

        private void RockClimbAttach()
        {
            ThirdPersonController.canApplyGravity = false;

            ThirdPersonController.movementAllowed = false;

            playerRBody2D.bodyType = RigidbodyType2D.Static;

            playerAttached = true;

            //playerCollider2D.enabled = false;
        }

        private void RockClimbDeattach()
        {
            _input.ropeAttach = false;

            addJumpForce = true;

            addClimbJumpForce = true;

            playerAttached = false;

            //playerAttached = false;

            playerRBody2D.bodyType = RigidbodyType2D.Dynamic;

            ThirdPersonController.canApplyGravity = true;

            ThirdPersonController.movementAllowed = true;

            /*
            if (!ropeDetachCoroutine)
            {
                StartCoroutine(ropeDetachWait(0.05f));
            }
            */
        }

        /*
        IEnumerator ropeDetachWait(float secs)
        {
            ropeDetachCoroutine = true;

            //Debug.Log("Start");

            yield return new WaitForSeconds(secs);

            playerCollider2D.enabled = true;

            //Debug.Log("End");

            handleObject = null;

            ropeHingeJoint = null;

            ropeDetachCoroutine = false;
        }
        */
        
    }
}
