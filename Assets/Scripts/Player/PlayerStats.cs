using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Tooltip("Enter starting time")]
    public float timeLeft;

    public float checkpointDistance = 0.0f;

    public float playerScore = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0.0f;

        checkpointDistance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return (int)playerScore;
    }
}
