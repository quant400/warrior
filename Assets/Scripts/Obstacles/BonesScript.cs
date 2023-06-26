using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warrior
{
    public class BonesScript : MonoBehaviour
    {
        private Collider2D gameObjectCollider;

        private Rigidbody2D playerRBody;

        private bool playerInContact;

        private Rigidbody2D objectRBody;

        // Start is called before the first frame update
        void Start()
        {
            gameObjectCollider = this.gameObject.GetComponent<Collider2D>();

            playerRBody = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Rigidbody2D>();

            objectRBody = this.gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if (playerInContact)
            {
                ForceApplied();
            }
        }

        private void ForceApplied()
        {
            Vector3 newVelocity = Vector3.zero;

            if (playerRBody.velocity.y < -1)
            {
                newVelocity.y = playerRBody.velocity.y * 15;

                //newVelocity.x = objectRBody.velocity.x;

                objectRBody.AddForce(newVelocity);

                //Debug.Log("Velocity Applied: " + objectRBody.velocity.y);
            }
        }

        /*
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                gameObjectCollider.isTrigger = true;
            }
        }
        */

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                playerInContact = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                //gameObjectCollider.isTrigger = false;

                playerInContact = false;
            }

        }

    }
}
