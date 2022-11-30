using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardMovement : MonoBehaviour
{
    private GameObject playerBody;

    private Vector3 playerPos;

    Vector3 gameObjectPos;

    public int updateLocationTimes = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("PlayerBody");

        playerPos = playerBody.transform.position;

        gameObjectPos = gameObject.transform.position;

        /*

        gameObjectPos.x = UnityEngine.Random.Range(playerPos.x + 100, playerPos.x + 200);

        gameObject.transform.position = gameObjectPos;
        */
    }

    // Update is called once per frame
    void Update()
    {
        gameObjectPos = gameObject.transform.position;

        playerPos = playerBody.transform.position;

        if(updateLocationTimes > 0)
        {
            if (playerPos.x > gameObject.transform.position.x + 100)
            {
                gameObjectPos.x = UnityEngine.Random.Range(playerPos.x + 300, playerPos.x + 500);

                gameObject.transform.position = gameObjectPos;

                updateLocationTimes--;
            }
        }

    }
}
