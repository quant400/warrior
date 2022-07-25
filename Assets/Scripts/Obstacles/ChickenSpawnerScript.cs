using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawnerScript : MonoBehaviour
{
    public GameObject chickenPrefab;

    [SerializeField]
    private float spawnDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(chickenSpawnDelay(spawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator chickenSpawnDelay(float secs)
    {
        while(true)
        {
            Instantiate(chickenPrefab, gameObject.transform.position, chickenPrefab.transform.rotation);

            //Debug.Log("Deactivated");

            yield return new WaitForSeconds(secs);

            //Debug.Log("Activated");
        }

    }
}
