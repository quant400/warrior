using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenObstacleScript : MonoBehaviour
{
    private bool playerInRange;

    private bool hasSpawned;

    private GameObject spawner;



    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;

        hasSpawned = false;

        spawner = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnDisable()
    {
        hasSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !hasSpawned)
        {
            hasSpawned = true;

            spawner.GetComponent<ChickenSpawnerScript>().StartSpawning();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBody") && !hasSpawned)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBody") && hasSpawned)
        {
            playerInRange = false;
        }
    }
}
