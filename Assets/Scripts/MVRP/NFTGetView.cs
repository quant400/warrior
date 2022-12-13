
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[Serializable]
public class NFTInfo
{
    public int id;
    public string name;
}
public class NFTGetView : MonoBehaviour
{
    [SerializeField] characterSelectionView characterSelectView;
    public static UnityWebRequest temp;
    [SerializeField]
    GameObject noNFTCanvas;


    private void Start()
    {
        if (characterSelectView == null)
        {
            characterSelectView = GameObject.FindObjectOfType<characterSelectionView>();
        }
    }
    public void GetNFT()
    {
        StartCoroutine(KeyMaker.instance.GetRequest());

        /*
        Debug.LogWarningFormat("Change this before final build and also rename youngin, sledghammer and long shot");
        string acc = PlayerPrefs.GetString("Account");
        StartCoroutine(GetRequest("https://api.cryptofightclub.io/game/sdk/" + acc));
        */
        //testing link
        //StartCoroutine(GetRequest("https://api.cryptofightclub.io/game/sdk/0xbecd7b5cfab483d65662769ad4fecf05be4d4d05"));
    }

    /*
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    temp = webRequest;
                    Display();
                    break;
            }
        }
    }
    */

    public void Display(NFTInfo[] NFTData)
    {
        //string data = "{\"Items\":" + temp.downloadHandler.text + "}";
        //warriorGameModel.currentNFTString = data;

        //NFTInfo[] NFTData = JsonHelper.FromJson<NFTInfo>(data);
        warriorGameModel.currentNFTArray = NFTData;
        if (NFTData.Length == 0)
        {
            /*
            noNFTCanvas.SetActive(true);
            warriorGameModel.userIsLogged.Value = false;
            */

            gameplayView.instance.usingFreemint = true;
            characterSelectView.FreeMint();
            warriorGameModel.userIsLogged.Value = true;
        }
        else
        {
            gameplayView.instance.usingFreemint = false;

            noNFTCanvas.SetActive(false);
            characterSelectView.SetData(NFTData);
            warriorGameModel.userIsLogged.Value = true;
        }


    }
    public void savedLoggedDisplay()
    {
        if (gameplayView.nftDataArray.Length == 0)
        {
            noNFTCanvas.SetActive(true);
            warriorGameModel.userIsLogged.Value = false;
        }
        else
        {
            noNFTCanvas.SetActive(false);
            characterSelectView.SetData(warriorGameModel.currentNFTArray);
            warriorGameModel.userIsLogged.Value = true;
        }
    }

    //temp Fuction for skip
    public void Skip()
    {
        StartCoroutine(KeyMaker.instance.GetRequestSkip());

        characterSelectView.Skip();
    }
}

