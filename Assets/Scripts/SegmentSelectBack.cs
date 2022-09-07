using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSelectBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndGame()
    {
        warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnGameEnded;

        SegmentScript.Instance.easyOnly = false;

        SegmentScript.Instance.mediumOnly = false;

        SegmentScript.Instance.hardOnly = false;

        //SegmentScript.Instance.segmentSelected = -5;
    }
}
