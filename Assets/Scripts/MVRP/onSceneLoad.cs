using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onSceneLoad : MonoBehaviour
{
    
    void Start()
    {
        warriorGameModel.lastSavedStep = warriorGameModel.gameCurrentStep.Value;
        warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.onSceneLoaded;
        warriorGameModel.gameCurrentStep.Value = warriorGameModel.lastSavedStep;

    }
}
