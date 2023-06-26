using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warrior
{
    public class ChickenSpawnerScript : MonoBehaviour
    {
        public GameObject chickenPrefab;

        [SerializeField]
        private float spawnDelay = 2.0f;

        [SerializeField]
        private int spawnNum = 1;

        private bool activateSpawning;

        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            activateSpawning = false;

            animator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (activateSpawning)
            {
                activateSpawning = false;

                animator.SetBool("inRange", true);

                StartCoroutine(ChickenSpawnDelay(spawnDelay));
            }
        }

        public void StartSpawning()
        {
            activateSpawning = true;
        }

        IEnumerator ChickenSpawnDelay(float secs)
        {
            yield return new WaitForSeconds(secs);

            for (int i = 0; i < spawnNum; i++)
            {
                Instantiate(chickenPrefab, gameObject.transform.position, chickenPrefab.transform.rotation);

                //Debug.Log("Deactivated");

                yield return new WaitForSeconds(secs);

                //Debug.Log("Activated");
            }

        }
    }
}
