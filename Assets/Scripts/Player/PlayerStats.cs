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

    public int longestRun = 0;

    private bool highscoreChanged;

    public GameObject fireworks;

    private ParticleSystem fireworksParticleSystem;

    private bool fireworksTimerBool;

    // Start is called before the first frame update
    void Start()
    {
        //playerScore = 0.0f;

        checkpointDistance = 0.0f;

        try
        {
            longestRun = gameplayView.instance.GetLongestDistanceScore();
        }
        catch
        {
            longestRun = 0;
        }

        highscoreChanged = false;

        fireworksTimerBool = false;

        fireworksParticleSystem = fireworks.GetComponent<ParticleSystem>();

        fireworksParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft == 0)
        {
            warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnGameEnded;
        }

        if(!highscoreChanged)
        {
            if (playerScore > longestRun)
            {
                Debug.Log("New Highscore");

                if(!fireworksTimerBool)
                {
                    StartCoroutine(fireworksTimer(5f));
                }

                highscoreChanged = true;
            }
        }

    }

    public int GetScore()
    {
        return (int)playerScore;
    }

    IEnumerator fireworksTimer(float secs)
    {
        fireworksTimerBool = true;

        fireworksParticleSystem.Play();

        yield return new WaitForSeconds(secs);

        fireworksParticleSystem.Stop();

        fireworksTimerBool = false;
    }
}
