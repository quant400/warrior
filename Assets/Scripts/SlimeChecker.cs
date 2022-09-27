using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class SlimeChecker : MonoBehaviour
    {
        private Rigidbody2D rBody;

        private new Collider2D collider;

        public PhysicsMaterial2D frictionlessPhysicsMaterial;

        public PhysicsMaterial2D playerPhysicsMaterial;

        private bool isInSlime;

        private bool isInSlimeDrop;

        public float slowDownAmount = 15.0f;

        private float originalDragValue;

        private float originalGravityValue;

        private StarterAssetsInputs _input;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            rBody = gameObject.GetComponent<Rigidbody2D>();

            collider = gameObject.GetComponent<Collider2D>();

            isInSlime = false;

            originalDragValue = rBody.drag;

            originalGravityValue = rBody.gravityScale;

        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("isInSlime = " + isInSlime);

            //Debug.Log("gravityScale = " + rBody.gravityScale);
        }

        private void FixedUpdate()
        {
            if (isInSlime)
            {
                rBody.drag = slowDownAmount;

                //rBody.angularDrag = 5.0f;

                rBody.gravityScale = slowDownAmount + 2;

                collider.sharedMaterial = frictionlessPhysicsMaterial;

                AudioManager.Instance.InSlimeSoundEffect();

            }
            else if (isInSlimeDrop)
            {
                rBody.drag = slowDownAmount + 10;

                rBody.gravityScale = slowDownAmount + 10;

                AudioManager.Instance.InSlimeSoundEffect();
            }
            else
            {
                rBody.drag = originalDragValue;

                rBody.gravityScale = originalGravityValue;

                collider.sharedMaterial = playerPhysicsMaterial;

                AudioManager.Instance.OutSlimeSoundEffect();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Slime") && !isInSlime)
            {
                /*
                if(!isInSlime && !isInSlimeDrop)
                {
                    //Debug.Log("In slime");

                    AudioManager.Instance.InSlimeSoundEffect();
                }
                */

                isInSlime = true;
            }
            else if (collision.CompareTag("SlimeDrop"))
            {
                /*
                if (!isInSlime && !isInSlimeDrop)
                {
                    //Debug.Log("In slime");

                    AudioManager.Instance.InSlimeSoundEffect();
                }
                */

                isInSlimeDrop = true;
            }

        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Slime"))
            {
                /*
                if (!isInSlime && !isInSlimeDrop)
                {
                    //Debug.Log("In slime");

                    AudioManager.Instance.InSlimeSoundEffect();
                }
                */
                isInSlime = true;
            }
            else if (collision.CompareTag("SlimeDrop"))
            {
                /*
                if (!isInSlime && !isInSlimeDrop)
                {
                    //Debug.Log("In slime");

                    AudioManager.Instance.InSlimeSoundEffect();
                }
                */


                isInSlimeDrop = true;
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Slime"))
            {
                //Debug.Log("Out slime");
                
                if(!isInSlimeDrop)
                {
                    AudioManager.Instance.OutSlimeSoundEffect();
                }
                

            }


            if (collision.CompareTag("Slime") && isInSlime)
            {
                isInSlime = false;
            }
            else if (collision.CompareTag("SlimeDrop"))
            {
                if(!isInSlime)
                {
                    //Debug.Log("Out slime");

                    AudioManager.Instance.OutSlimeSoundEffect();
                }

                isInSlimeDrop = false;
            }
        }
    }
}
