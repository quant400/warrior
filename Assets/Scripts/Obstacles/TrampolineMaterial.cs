using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class TrampolineMaterial : MonoBehaviour
    {
        private GameObject playerBody;

        private Rigidbody2D playerRBody;

        private Vector2 playerVelocity;

        private bool playerInContact;

        private Collider2D gameObjectCollider;

        [Range(1.0f, 120.0f)]
        public float maxJumpHeightModifier = 12.0f;

        [Range(1.0f, 20.0f)]
        public float maxTrampolineForceModifier = 2.0f;

        private float oldVelocity;

        private bool applyVelocityCoroutine;


        // Start is called before the first frame update
        void Start()
        {
            playerBody = GameObject.FindGameObjectWithTag("PlayerBody");

            playerRBody = playerBody.GetComponent<Rigidbody2D>();

            playerVelocity = Vector2.zero;

            playerInContact = false;

            oldVelocity = 0.0f;

            gameObjectCollider = gameObject.GetComponent<Collider2D>();

            applyVelocityCoroutine = false;

        }


        // Update is called once per frame
        void Update()
        {
            /*
            if (playerInContact)
            {
                TrampolineForces();
            }
            */

            /*
            if(playerRBody.velocity.y < 0)
            {
                Debug.Log("Update playerVelocity = " + playerRBody.velocity.y);
            */

        }



        private void FixedUpdate()
        {
            

            if (playerInContact)
            {
                TrampolineForces();

                /*
                if(applyVelocityCoroutine == false)
                {
                    StartCoroutine(ApplyVelocity(0.1f));
                }
                */
            }

            //Debug.Log("Fixed playerVelocity = " + playerRBody.velocity.y);


        }

        private void TrampolineForces()
        {
            //ThirdPersonController.canApplyGravity = true;

            /*
            if(!applyVelocityCoroutine)
            {
                StartCoroutine(ApplyVelocity(0.5f));
            }
            */

            playerVelocity = playerRBody.velocity;

            float playerYVelocity = playerVelocity.y;

            float newYVelocity;

            //Debug.Log("Old Velocity = " + playerVelocity.y);

            if (playerYVelocity < 0)
            {
                ThirdPersonController.canApplyGravity = false;

                //playerVelocity.y = 0;

                //Debug.Log("Enter playerVelocity = " + playerRBody.velocity.y);

                newYVelocity = playerYVelocity * (-1 * maxTrampolineForceModifier);

                /*
                Debug.Log("Force Applied");

                playerRBody.AddForce((new Vector3(0, 1, 0)) * maxTrampolineForceModifier * 10, ForceMode2D.Impulse);


                newYVelocity = playerRBody.velocity.y;
                */

                if (newYVelocity > oldVelocity)
                {
                    if (newYVelocity > maxJumpHeightModifier)
                    {
                        newYVelocity = maxJumpHeightModifier;
                    }

                    oldVelocity = newYVelocity;

                }

                playerVelocity.y = oldVelocity;

                //Debug.Log("New Velocity = " + playerVelocity.y);

                playerRBody.velocity = playerVelocity;

                if(!applyVelocityCoroutine)
                {
                    StartCoroutine(ApplyVelocity(0.5f));
                }
            }


            //Debug.Log("Exit playerVelocity = " + playerRBody.velocity.y);

        }


        IEnumerator ApplyVelocity(float secs)
        {
            applyVelocityCoroutine = true;

            //TrampolineForces();

            //Debug.Log("Applied Force");

            yield return new WaitForSeconds(secs);

            ThirdPersonController.canApplyGravity = true;

            applyVelocityCoroutine = false;
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                playerInContact = true;

                if (playerRBody.velocity.y >= -2 && playerRBody.velocity.y <= 1)
                {
                    gameObjectCollider.isTrigger = false;

                    //Debug.Log("isTrigger = false");
                }

                //Debug.Log("playerVelocity = " + playerRBody.velocity);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                playerInContact = false;

                gameObjectCollider.isTrigger = true;

                //Debug.Log("isTrigger = true");

                oldVelocity = 0.0f;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.transform.CompareTag("PlayerBody"))
            {
                playerInContact = true;

                if (!(playerRBody.velocity.y >= -2 && playerRBody.velocity.y <= 1))
                {
                    gameObjectCollider.isTrigger = true;

                    //Debug.Log("isTrigger = true");
                }

                //Debug.Log("playerVelocity = " + playerRBody.velocity);
            }


            /*
            if (collision.transform.CompareTag("PlayerBody"))
            {
                playerInContact = true;


                if (!(playerRBody.velocity.y >= -2 && playerRBody.velocity.y <= 1))
                {
                    gameObjectCollider.isTrigger = true;
                }


                //Debug.Log("playerVelocity = " + playerRBody.velocity);
            }
            */
        }


        private void OnCollisionExit2D(Collision2D collision)
        {

            if (collision.transform.CompareTag("PlayerBody"))
            {
                playerInContact = false;

                gameObjectCollider.isTrigger = true;

                //Debug.Log("isTrigger = true");

                oldVelocity = 0.0f;
            }


            /*
            if (collision.transform.CompareTag("PlayerBody"))
            {
                playerInContact = false;

                //gameObjectCollider.isTrigger = true;
            }
            */
        }

    }
}
