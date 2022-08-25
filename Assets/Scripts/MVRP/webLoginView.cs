
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx.Operators;
using UniRx;
using UniRx.Triggers;
#if UNITY_WEBGL
public class webLoginView : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account;


    [SerializeField]
    NFTGetView nftGetter;
    [SerializeField]
    GameObject loginButton;

    // temp for skip
    [SerializeField]
    GameObject skipButton; 
    [SerializeField]
    GameObject tryoutButton;
    [SerializeField]
    GameObject tryoutCanvas;

    public void checkUSerLoggedAtStart()
    {
        if (warriorGameModel.userIsLogged.Value)
        {
            nftGetter.savedLoggedDisplay();
        }
        else
        {
            warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnLogin;

        }
    }
    public void OnLogin(Button loginBtn, Button skipBtn, Button tryoutBtn)
    {
        if (warriorGameModel.userIsLogged.Value)
        {
            loginBtn.GetComponent<Button>().interactable = false;
            skipBtn.GetComponent<Button>().interactable = false;
            tryoutBtn.GetComponent<Button>().interactable = false;
            nftGetter.savedLoggedDisplay();
        }
        else
        {
            Web3Connect();
            OnConnected();
        }
        gameplayView.instance.isTryout = false;
    }

    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "")
        {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        // reset login message
        SetConnectAccount("");
        // load next scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        loginButton.GetComponent<Button>().interactable = false;
        skipButton.GetComponent<Button>().interactable = false;
        tryoutButton.GetComponent<Button>().interactable = false;
        nftGetter.GetNFT();


    }

    public void OnSkip()
    {
        gameplayView.instance.isTryout = false;
        nftGetter.Skip();
    }

    public void OnTryout()
    {
        gameplayView.instance.isTryout = true;

        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(5).gameObject.SetActive(true);

        nftGetter.Skip();
        SceneManager.LoadScene(warriorGameModel.singlePlayerScene1.sceneName);

        
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        

        //transform.GetChild(8).gameObject.SetActive(true);


        //tryoutCanvas.SetActive(true);


    }

}
#endif

