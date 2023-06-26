using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warrior
{
    public class BoulderPlayerDetection : MonoBehaviour
    {
        public GameObject boulder;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayerBody"))
            {
                if (!boulder.activeSelf)
                {
                    boulder.SetActive(true);
                }

            }
        }
    }
}
