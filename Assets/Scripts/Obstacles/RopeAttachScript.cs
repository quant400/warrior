using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class RopeAttachScript : MonoBehaviour
    {
        
        private StarterAssetsInputs _input;

        private bool playerCollision;

        private bool playerAttached;

        private Rigidbody2D playerRBody2D;

        private Collider2D playerCollider2D;

        private HingeJoint2D ropeHingeJoint;

        private GameObject handleObject;

        private bool addJumpForce;

        [SerializeField]
        private float jumpForce = 3.0f;

        private bool ropeDetachCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            playerRBody2D = gameObject.GetComponent<Rigidbody2D>();

            playerCollider2D = gameObject.GetComponent<Collider2D>();

            playerCollision = false;

            playerAttached = false;

            ropeDetachCoroutine = false;

            ropeHingeJoint = null;

            addJumpForce = false;
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
            }
            else
            {
                if (_input.ropeAttach && !playerAttached)
                {
                    if(handleObject != null)
                    {
                        ThirdPersonController.canApplyGravity = false;

                        gameObject.transform.position = new Vector3(handleObject.transform.position.x, handleObject.transform.position.y, gameObject.transform.position.z);

                        ropeHingeJoint = handleObject.AddComponent<HingeJoint2D>();

                        ropeHingeJoint.connectedBody = playerRBody2D;

                        playerAttached = true;

                        playerRBody2D.mass = 0.0f;

                        gameObject.GetComponent<Collider2D>().enabled = false;
                    }
                }

                if (playerAttached && !_input.ropeAttach)
                {
                    Destroy(ropeHingeJoint);

                    //playerAttached = false;

                    handleObject = null;

                    playerCollision = false;

                    playerRBody2D.mass = 1.0f;

                    ropeHingeJoint = null;

                    ThirdPersonController.canApplyGravity = true;

                    addJumpForce = true;

                    if(!ropeDetachCoroutine)
                    {
                        StartCoroutine(ropeDetachWait(0.05f));
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
            */

        }

        private void FixedUpdate()
        {
            if(addJumpForce)
            {
                playerRBody2D.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * jumpForce, ForceMode2D.Impulse);

                addJumpForce = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.transform.CompareTag("Links"))
            {
                //Debug.Log(collision.gameObject.name);

                if(handleObject == null && !playerCollision)
                {
                    if (!collision.transform.parent.GetChild(0).GetComponent<HingeJoint2D>())
                    {
                        handleObject = collision.transform.parent.GetChild(0).gameObject;
                    }


                    //Debug.Log("playerCollision = true");
                    playerCollision = true;
                }

                
            }
            else
            {
                playerCollision = false;

                handleObject = null;

                ropeHingeJoint = null;
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


                    //Debug.Log("playerCollision = true");
                    playerCollision = true;
                }


            }
            else
            {
                playerCollision = false;

                handleObject = null;

                ropeHingeJoint = null;
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
                }
                
            }
        }
        
        
        
        
        IEnumerator ropeDetachWait(float secs)
        {
            ropeDetachCoroutine = true;

            yield return new WaitForSeconds(secs);

            gameObject.GetComponent<Collider2D>().enabled = true;

            ropeDetachCoroutine = false;
        }
        
        
    }
}
