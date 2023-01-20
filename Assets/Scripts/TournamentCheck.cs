using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentCheck : MonoBehaviour
{
    public GameObject tournamentUi;

    private bool currentStatus = false;

    private bool newStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameplayView.instance)
        {
            newStatus = gameplayView.instance.GetTournamentStatus();

            if (currentStatus != newStatus)
            {
                tournamentUi.SetActive(newStatus);

                currentStatus = newStatus;
            }
        }
    }
}
