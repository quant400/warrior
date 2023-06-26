using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Warrior
{
    public class RotatingObstacleScript : MonoBehaviour
    {

        private Collider2D obstacleCollider;

        public float speed;

        private Vector3 originalPosition;

        bool firstPositionRecord = true;

        // Start is called before the first frame update
        void Start()
        {
            firstPositionRecord = true;

            obstacleCollider = gameObject.GetComponent<Collider2D>();

            //originalPosition = gameObject.transform.localPosition;
        }

        private void OnEnable()
        {
            if (firstPositionRecord)
            {
                //Debug.Log("Current Position: " + gameObject.transform.localPosition);

                originalPosition = gameObject.transform.localPosition;

                firstPositionRecord = false;
            }
            else
            {
                //Debug.Log("Original Position: " + originalPosition);

                //Debug.Log("Current Position: " + gameObject.transform.localPosition);

                gameObject.transform.localPosition = originalPosition;

                StartCoroutine(reposition());
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.RotateAround(obstacleCollider.bounds.center, new Vector3(0, 0, -1), speed * Time.deltaTime);
        }

        IEnumerator reposition()
        {
            //Debug.Log("reposition");

            yield return new WaitForSeconds(0.2f);

            gameObject.transform.localPosition = originalPosition;
        }
    }
}
