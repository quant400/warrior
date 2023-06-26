using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warrior
{
    public class SpriteFlip : MonoBehaviour
    {
        SpriteRenderer objectSptiteRenderer;

        float previousX;

        // Start is called before the first frame update
        void Start()
        {
            objectSptiteRenderer = gameObject.GetComponent<SpriteRenderer>();

            previousX = gameObject.transform.position.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (previousX > gameObject.transform.position.x)
            {
                //gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, 0.0f, gameObject.transform.eulerAngles.z);

                objectSptiteRenderer.flipX = true;

                previousX = gameObject.transform.position.x;
            }
            else if (previousX < gameObject.transform.position.x)
            {

                //gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, 180.0f, gameObject.transform.eulerAngles.z);

                objectSptiteRenderer.flipX = false;

                previousX = gameObject.transform.position.x;
            }
        }
    }
}
