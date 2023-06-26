
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx.Operators;
using UniRx;
using UniRx.Triggers;
#if UNITY_WEBGL

namespace Warrior
{
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
        [SerializeField]
        GameObject methodSelect;
        [SerializeField]
        GameObject signOutButton;

        // temp for skip
        [SerializeField]
        GameObject skipButton;
        [SerializeField]
        GameObject tryoutButton;
        [SerializeField]
        GameObject tryoutCanvas;
        [SerializeField]
        GameObject segmentSelect;

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
                //Debug.Log("user logged in");

                loginBtn.GetComponent<Button>().interactable = false;
                skipBtn.GetComponent<Button>().interactable = false;
                tryoutBtn.GetComponent<Button>().interactable = false;
                nftGetter.savedLoggedDisplay();
            }
            else
            {
                //Debug.Log("user NOT logged in");

                methodSelect.SetActive(false);
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
            gameplayView.instance.usingMeta = true;
            loginButton.GetComponentInParent<FireBaseWebGLAuth>().Close();
            //signOutButton.SetActive(false);
            nftGetter.GetNFT();
        }

        public void OnSkip()
        {
            PlayerPrefs.SetString("Account", "0xD408B954A1Ec6c53BE4E181368F1A54ca434d2f3");
            gameplayView.instance.isTryout = false;
            nftGetter.Skip();

            /*
            gameplayView.instance.isTryout = false;
            nftGetter.Skip();
            */
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
            transform.GetChild(8).gameObject.SetActive(true);

            nftGetter.Skip();
            SceneManager.LoadScene(warriorGameModel.singlePlayerScene1.sceneName);


            /*
            foreach(Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }
            transform.GetChild(0).gameObject.SetActive(true);
            */

            //transform.GetChild(8).gameObject.SetActive(true);


            //tryoutCanvas.SetActive(true);


        }

        public void OnSegmentSelect()
        {
            /*
            gameplayView.instance.isTryout = true;

            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(true);
            transform.GetChild(8).gameObject.SetActive(true);

            nftGetter.Skip();
            SceneManager.LoadScene(warriorGameModel.singlePlayerScene1.sceneName);
            */

            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(8).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(true);
            transform.GetChild(10).gameObject.SetActive(true);

            segmentSelect.SetActive(true);

            /*
            foreach(Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }
            transform.GetChild(0).gameObject.SetActive(true);
            */

            //transform.GetChild(8).gameObject.SetActive(true);


            //tryoutCanvas.SetActive(true);


        }

    }
#endif
}

