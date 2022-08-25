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
        /*
        switch(i)
        {
            case 0:
                nftGetter.Skip();
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene1.sceneName);
                break;
            case 1:
                nftGetter.Skip();
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene2.sceneName);
                break;
            case 2:
                nftGetter.Skip();
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene3.sceneName);
                break;
            case 3:
                nftGetter.Skip();
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene4.sceneName);
                break;
            case 4:
                nftGetter.Skip();
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene5.sceneName);
                break;
        }
        */

        nftGetter.Skip();
        SceneManager.LoadScene(warriorGameModel.singlePlayerScene1.sceneName);

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
