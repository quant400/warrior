using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCollisionsScript : MonoBehaviour
{
    private bool stopCollisonsCoroutine;

    private Collider2D[] childColliders;

    // Start is called before the first frame update
    void Start()
    {
        stopCollisonsCoroutine = false;

        childColliders = new Collider2D[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childColliders[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<Collider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopColliders()
    {
        if(!stopCollisonsCoroutine)
        {
            StartCoroutine(stopCollisons(0.1f));
        }
    }

    IEnumerator stopCollisons(float secs)
    {
        stopCollisonsCoroutine = true;

        for (int i = 0; i < childColliders.Length; i++)
        {
            childColliders[i].enabled = false;
        }

        yield return new WaitForSeconds(secs);

        for (int i = 0; i < childColliders.Length; i++)
        {
            childColliders[i].enabled = true;
        }

        stopCollisonsCoroutine = false;
    }
}
