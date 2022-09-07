using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawScript : MonoBehaviour
{
    Quaternion originalRotation;

    bool playerInContact;

    bool readjustRoationAllowed;

    bool rotationCoroutineRunning;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = gameObject.transform.localRotation;

        rotationCoroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerInContact && (originalRotation.z != gameObject.transform.localRotation.z))
        {
            if(!rotationCoroutineRunning)
            {
                StartCoroutine(rotateToOriginalPosition());
            }
        }

        if(readjustRoationAllowed)
        {
            if(originalRotation.z != gameObject.transform.localRotation.z)
            {
                if(gameObject.transform.localRotation.z < originalRotation.z)
                {
                    gameObject.transform.localRotation = new Quaternion(gameObject.transform.localRotation.x, gameObject.transform.localRotation.y, gameObject.transform.localRotation.z + (Time.deltaTime * 0.1f), gameObject.transform.localRotation.w);
                }
                else if (gameObject.transform.localRotation.z > originalRotation.z)
                {
                    gameObject.transform.localRotation = new Quaternion(gameObject.transform.localRotation.x, gameObject.transform.localRotation.y, gameObject.transform.localRotation.z - (Time.deltaTime * 0.1f), gameObject.transform.localRotation.w);
                }

                if(Mathf.Abs(gameObject.transform.localRotation.z - originalRotation.z) < 0.005)
                {
                    gameObject.transform.localRotation = new Quaternion(gameObject.transform.localRotation.x, gameObject.transform.localRotation.y, originalRotation.z, gameObject.transform.localRotation.w);
                }
            }
        }

        //Debug.Log("z = " + gameObject.transform.localRotation.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBody"))
        {
            playerInContact = true;

            readjustRoationAllowed = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBody"))
        {
            playerInContact = false;
        }
    }

    IEnumerator rotateToOriginalPosition()
    {
        rotationCoroutineRunning = true;

        yield return new WaitForSeconds(2f);

        if (!playerInContact)
        {
            readjustRoationAllowed = true;
        }

        rotationCoroutineRunning = false;
    }
}
