using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleTestScript : MonoBehaviour
{
    public int[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shuffle()
    {
        int tempGO;

        for (int i = 0; i < numbers.Length - 1; i++)
        {
            int rnd = Random.Range(i, numbers.Length);
            tempGO = numbers[rnd];
            numbers[rnd] = numbers[i];
            numbers[i] = tempGO;
        }
    }
}
