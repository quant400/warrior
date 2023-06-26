using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warrior
{
    public class BoulderPartsScript : MonoBehaviour
    {
        private Vector3 position;

        private Quaternion rotation;

        private Rigidbody2D rb2D;

        private Collider2D objectCollider;

        // Start is called before the first frame update
        void Start()
        {
            rb2D = gameObject.GetComponent<Rigidbody2D>();

            objectCollider = gameObject.GetComponent<Collider2D>();

            Vector3 direction = objectCollider.bounds.center - gameObject.transform.parent.position;

            rb2D.AddForce(direction * -20, ForceMode2D.Impulse);
        }

        private void OnEnable()
        {
            position = gameObject.transform.localPosition;

            rotation = gameObject.transform.localRotation;
        }

        private void OnDisable()
        {
            gameObject.transform.localPosition = position;

            gameObject.transform.localRotation = rotation;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody") || collision.transform.CompareTag("GroundCheck") || collision.transform.CompareTag("BoulderDitch"))
            {
                Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
            }
        }
    }
}
