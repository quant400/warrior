using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawnerScript : MonoBehaviour
{
    public GameObject chickenPrefab;

    [SerializeField]
    private float spawnDelay = 2.0f;

    [SerializeField]
    private int spawnNum = 1;

    private bool activateSpawning;

    // Start is called before the first frame update
    void Start()
    {
        activateSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activateSpawning)
        {
            activateSpawning = false;

            StartCoroutine(chickenSpawnDelay(spawnDelay));
        }
    }

    public void startSpawning()
    {
        activateSpawning = true;
    }

    IEnumerator chickenSpawnDelay(float secs)
    {
        for(int i = 0; i < spawnNum; i++)
        {
            Instantiate(chickenPrefab, gameObject.transform.position, chickenPrefab.transform.rotation);

            //Debug.Log("Deactivated");

            yield return new WaitForSeconds(secs);

            //Debug.Log("Activated");
        }

    }
}
