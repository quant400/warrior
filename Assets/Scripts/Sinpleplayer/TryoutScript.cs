using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryoutScript : MonoBehaviour
{
    [SerializeField]
    GameObject startscreen;
    [SerializeField]
    NFTGetView nftGetter;

    /*
    private void OnEnable()
    {
        nftGetter.Skip();
        SceneManager.LoadScene(warriorGameModel.singlePlayerScene1.sceneName);

        transform.gameObject.SetActive(false);
    }
    */
    
    public void ButtonPressed(int i)
    {
        gameplayView.instance.isTryout = true;

        /*
        switch(i)
        {
            case 0:
                SegmentScript.Instance.segmentSelected = 0;
                break;
            case 1:
                SegmentScript.Instance.segmentSelected = 1;
                break;
            case 2:
                SegmentScript.Instance.segmentSelected = 2;
                break;
            case 3:
                SegmentScript.Instance.segmentSelected = 3;
                break;
            case 4:
                SegmentScript.Instance.segmentSelected = 4;
                break;
            case 5:
                SegmentScript.Instance.segmentSelected = 5;
                break;
            case 6:
                SegmentScript.Instance.segmentSelected = 6;
                break;
            case 7:
                SegmentScript.Instance.segmentSelected = 7;
                break;
            case 8:
                SegmentScript.Instance.segmentSelected = 8;
                break;
            case 9:
                SegmentScript.Instance.segmentSelected = 9;
                break;
            case 10:
                SegmentScript.Instance.segmentSelected = 10;
                break;
            case 11:
                SegmentScript.Instance.segmentSelected = 11;
                break;
            case 12:
                SegmentScript.Instance.segmentSelected = 12;
                break;
        }
        */

        if (i == -1)
        {
            //Debug.Log("Easy Only");

            SegmentScript.Instance.easyOnly = true;

            SegmentScript.Instance.segmentSelected = i;
        }
        else if (i == -2)
        {
            //Debug.Log("Medium Only");

            SegmentScript.Instance.mediumOnly = true;

            SegmentScript.Instance.segmentSelected = i;
        }
        else if (i == -3)
        {
            //Debug.Log("Hard Only");

            SegmentScript.Instance.hardOnly = true;

            SegmentScript.Instance.segmentSelected = i;
        }
        else
        {
            //Debug.Log("i = " + i);

            SegmentScript.Instance.segmentSelected = i;
        }
        

        nftGetter.Skip();
        SceneManager.LoadScene("TestScene");

        transform.gameObject.SetActive(false);

    }
    

    public void BackButton()
    {
        gameObject.SetActive(false);
        startscreen.SetActive(true);
        gameplayView.instance.isTryout = false;
    }

   public void TryOutHomeBtn()
    {
        if(gameplayView.instance.isTryout)
        {
            Debug.Log("Called");
            transform.parent.GetChild(2).gameObject.SetActive(true);
            transform.parent.GetChild(2).GetChild(0).GetChild(4).gameObject.SetActive(true);
            transform.parent.GetChild(6).gameObject.SetActive(false);
            warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnLogin;
            SceneManager.LoadScene(warriorGameModel.mainSceneLoadname.sceneName);

        }
    }
}
