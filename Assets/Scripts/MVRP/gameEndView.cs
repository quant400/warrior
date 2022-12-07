using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.IO;
using UniRx;
using UniRx.Triggers;
using UniRx.Operators;
public class gameEndView : MonoBehaviour
{
    [SerializeField]
    Transform characterDisplay;
    GameObject[] characters;
    [SerializeField]
    TMP_Text currentScore, dailyScore, allTimeScore, weeklyScore, longestDistance, sessionCounterText;
    [SerializeField]
    GameObject canvasToDisable;
    [SerializeField]
    AudioClip gameOverClip;
    NFTInfo currentNFT;
    [SerializeField]
    GameObject sessionsLeft, sessionsNotLeft;
    ReactiveProperty<int> scorereactive = new ReactiveProperty<int>();
    ReactiveProperty<int> sessions = new ReactiveProperty<int>();
    ReactiveProperty<bool> gameEnded = new ReactiveProperty<bool>();
    [SerializeField] Button tryAgain, back;
    GameObject localDisplay;
    [SerializeField] private RuntimeAnimatorController playerAnimatorController;

    // [SerializeField]
    //SinglePlayerSpawner spawner;
    [SerializeField]
    GameObject tryoutCanvas;
    private void OnEnable()
    {
        /*
        if (gameplayView.instance.isTryout)
        {
            GameObject.FindGameObjectWithTag("PlayerUI").SetActive(false);
        }
        */

        //GameObject.FindGameObjectWithTag("PlayerUI").SetActive(false);
        //GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);

        if (gameplayView.instance.isTryout)
        {
            transform.parent.transform.GetChild(8).gameObject.SetActive(true);
            tryAgain.gameObject.SetActive(false);
            //Debug.Log(SinglePlayerScoreBoardScript.instance.GetScore().ToString());
            currentScore.text = "DISTANCE TRAVELED : " + PlayerStats.Instance.GetScore().ToString();
            dailyScore.text = "DAILY SCORE : " + 0;
            weeklyScore.text = "WEEKLY SCORE : " + 0;
            allTimeScore.text = "ALL TIME SCORE : " + 0;
            longestDistance.text = "LONGEST RUN : " + 0;
            //sessionCounterText.text = "NFT DAILY RUNS : " + 0 + "/10";
            sessionCounterText.text = "NFT DAILY RUNS : " + 0;
        }
        else
        {
            tryAgain.gameObject.SetActive(true);
        }
        AudioSource ad = GetComponent<AudioSource>();
        ad.clip = gameOverClip;
        ad.loop = false;
        ad.volume = 0.2f;
        ad.Play();
    }
    public void Start()
    {
        observeScoreChange();
        endGameAfterValueChange();
        ObserveGameObverBtns();
       
    }
    public void setScoreAtStart()
    {
        //tryAgain.gameObject.SetActive(false);
        if (canvasToDisable == null)
        {
            canvasToDisable = gameplayView.instance.gameObject.transform.GetChild(0).gameObject;
        }
        currentNFT = gameplayView.instance.chosenNFT;
        /*
        if (gameplayView.instance.GetSessions() <= 10)
        {
            if (gameplayView.instance.isRestApi)
            {
                Debug.Log("before Score");

                //DatabaseManagerRestApi._instance.setScoreRestApiMain(currentNFT.id.ToString(), SinglePlayerScoreBoardScript.instance.GetScore());
                DatabaseManagerRestApi._instance.setScoreRestApiMain(currentNFT.id.ToString(), PlayerStats.Instance.GetScore());
                Debug.Log("posted Score");
            }
            else
            {
                // DatabaseManager._instance.setScore(currentNFT.id.ToString(), currentNFT.name, SinglePlayerScoreBoardScript.instance.GetScore());

            }
        }
        */

        if (gameplayView.instance.isRestApi && !gameplayView.instance.isTryout)
        //if (gameplayView.instance.isRestApi)
        {
            Debug.Log("before Score");

            //DatabaseManagerRestApi._instance.setScoreRestApiMain(currentNFT.id.ToString(), SinglePlayerScoreBoardScript.instance.GetScore());
            DatabaseManagerRestApi._instance.setScoreRestApiMain(currentNFT.id.ToString(), PlayerStats.Instance.GetScore());
            Debug.Log("posted Score");
        }
        else
        {
            // DatabaseManager._instance.setScore(currentNFT.id.ToString(), currentNFT.name, SinglePlayerScoreBoardScript.instance.GetScore());
        }

        gameplayView.instance.GetScores();
        setScoreResutls();

    }
    public void initializeValues()
    {
        scorereactive.Value = -1;
        sessions.Value = -1;
        gameEnded.Value = false;
    }
    public void ObserveGameObverBtns()
    {

        tryAgain.OnClickAsObservable()
            .Where(_=>gameEnded.Value==true)
            .Do(_ => TryAgain())
            .Where(_ => PlaySounds.instance != null)
            .Do(_ => PlaySounds.instance.Play())
            .Subscribe()
            .AddTo(this);
       
    }
    public void updateResults()
    {
        /*
        if (gameplayView.instance.GetSessions() < 10)
        {

            sessionsLeft.SetActive(true);
            sessionsNotLeft.SetActive(false);
            //currentScore.text = "CHICKENS CAUGHT : " + SinglePlayerScoreBoardScript.instance.GetScore().ToString();
            currentScore.text = "DISTANCE TRAVELED : " + PlayerStats.Instance.GetScore().ToString();
            dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
            allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
            //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
            sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());

        }
        else if (gameplayView.instance.GetSessions() >= 10)
        {
            sessionsLeft.SetActive(false);
            sessionsNotLeft.SetActive(true);
            dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
            allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
            //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
            sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());

        }
        //SinglePlayerScoreBoardScript.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        */

        //Debug.Log("1");

        sessionsLeft.SetActive(true);
        sessionsNotLeft.SetActive(false);
        //currentScore.text = "CHICKENS CAUGHT : " + SinglePlayerScoreBoardScript.instance.GetScore().ToString();
        currentScore.text = "DISTANCE TRAVELED : " + PlayerStats.Instance.GetScore().ToString();
        dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
        weeklyScore.text = "WEEKLY SCORE : " + (gameplayView.instance.GetWeeklyScore());
        allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
        longestDistance.text = "LONGEST RUN : " + (gameplayView.instance.GetLongestDistanceScore());
        //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
        sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());
    }
    public void setScoreResutls()
    {
        /*
        if (gameplayView.instance.GetSessions() < 10)
        {

            sessionsLeft.SetActive(true);
            sessionsNotLeft.SetActive(false);
            //currentScore.text = "CHICKENS CAUGHT : " + SinglePlayerScoreBoardScript.instance.GetScore().ToString();
            currentScore.text = "DISTANCE TRAVELED : " + PlayerStats.Instance.GetScore().ToString();
            dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
            allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
            //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
            sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());

        }
        else if (gameplayView.instance.GetSessions() >= 10)
        {
            sessionsLeft.SetActive(false);
            sessionsNotLeft.SetActive(true);
            dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
            allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
            //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
            sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());

        }
        */

        //Debug.Log("2");

        sessionsLeft.SetActive(true);
        sessionsNotLeft.SetActive(false);
        //currentScore.text = "CHICKENS CAUGHT : " + SinglePlayerScoreBoardScript.instance.GetScore().ToString();
        currentScore.text = "DISTANCE TRAVELED : " + PlayerStats.Instance.GetScore().ToString();
        dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
        weeklyScore.text = "WEEKLY SCORE : " + (gameplayView.instance.GetWeeklyScore());
        allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
        longestDistance.text = "LONGEST RUN : " + (gameplayView.instance.GetLongestDistanceScore());
        //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
        sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());


        //characters = spawner.GetCharacterList();
        Debug.Log("Load character");
        //Destroy(GameObject.FindGameObjectWithTag("PlayerBody"));
        //GameObject displayChar = Resources.Load(Path.Combine("SinglePlayerPrefabs/Characters", NameToSlugConvert(currentNFT.name))) as GameObject;
        GameObject displayChar = Resources.Load(Path.Combine(("SinglePlayerPrefabs/FIGHTERS2.0Redone/" + currentNFT.name), NameToSlugConvert(currentNFT.name))) as GameObject;
        Debug.Log(currentNFT.name);
        Debug.Log(displayChar.name);
        //var temp = Instantiate(displayChar, characterDisplay.position, Quaternion.identity, characterDisplay);

        foreach (Transform child in characterDisplay.transform)
        {
            Destroy(child.gameObject);
        }

        var temp = Instantiate(displayChar, characterDisplay.position, Quaternion.identity);

        temp.gameObject.transform.SetParent(characterDisplay.transform);

        //destroying all player related components

        //temp.transform.GetChild(0).gameObject.SetActive(false);

        //Destroy(temp.transform.GetChild(0).gameObject);
        /*
        Destroy(temp.transform.GetChild(1).gameObject);
        Destroy(temp.transform.GetChild(2).gameObject);
        Destroy(temp.transform.GetChild(3).gameObject);
        Destroy(temp.GetComponent<StarterAssetsInputs>());
        Destroy(temp.GetComponent<ThirdPersonController>());
        Destroy(temp.GetComponent<CharacterController>());
        Destroy(temp.GetComponent<PlayerInput>());
        */

        temp.GetComponent<Animator>().runtimeAnimatorController = playerAnimatorController;

        temp.GetComponent<Animator>().SetBool("Ended", true);

        temp.transform.localPosition = Vector3.zero;
        temp.transform.localRotation = Quaternion.identity;
        temp.transform.localScale = Vector3.one * (0.2f);
        localDisplay = temp;
        //upddate other values here form leaderboard
        //SinglePlayerScoreBoardScript.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void endGameAfterValueChange()
    {
        gameEnded
            .Where(_ => _ == true)
            .Do(_ => updateResults())
            .Subscribe()
            .AddTo(this);
    }
    public void observeScoreChange()
    {
        scorereactive
            .Do(_ => setScoreToUI())
            .Subscribe()
            .AddTo(this);

        sessions
            .Do(_ => setScoreToUI())
            .Subscribe()
            .AddTo(this);

    }
    public void resetDisplay()
    {
        if (localDisplay!=null)
        Destroy(localDisplay);
    }
    private void Update()
    {
        if (warriorGameModel.gameCurrentStep.Value == warriorGameModel.GameSteps.OnGameEnded)
        {
            
            Scene currentScene = SceneManager.GetActiveScene();

            if(currentScene.name != "EndScene")
            {
                SceneManager.LoadScene("EndScene");

                SegmentScript.Instance.segmentSelected = -5;
            };

            scorereactive.Value = gameplayView.instance.dailyScore;
            sessions.Value = gameplayView.instance.sessions;
        }
    }
    public void setScoreToUI()
    {
        gameEnded.Value = true;
        /*
        if (gameplayView.instance.GetSessions() < 10)
        {

            sessionsLeft.SetActive(true);
            sessionsNotLeft.SetActive(false);
            //currentScore.text = "CHICKENS CAUGHT : " + SinglePlayerScoreBoardScript.instance.GetScore().ToString();
            currentScore.text = "DISTANCE TRAVELED : " + PlayerStats.Instance.GetScore().ToString();
            dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
            allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
            //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
            sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());

        }
        else if (gameplayView.instance.GetSessions() >= 10)
        {
            sessionsLeft.SetActive(false);
            sessionsNotLeft.SetActive(true);
            dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
            allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
            //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
            sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());

        }
        */

        //Debug.Log("3");

        sessionsLeft.SetActive(true);
        sessionsNotLeft.SetActive(false);
        //currentScore.text = "CHICKENS CAUGHT : " + SinglePlayerScoreBoardScript.instance.GetScore().ToString();
        currentScore.text = "DISTANCE TRAVELED : " + PlayerStats.Instance.GetScore().ToString();
        dailyScore.text = "DAILY SCORE : " + (gameplayView.instance.GetDailyScore());
        weeklyScore.text = "WEEKLY SCORE : " + (gameplayView.instance.GetWeeklyScore());
        allTimeScore.text = "ALL TIME SCORE : " + (gameplayView.instance.GetAllTimeScore());
        longestDistance.text = "LONGEST RUN : " + (gameplayView.instance.GetLongestDistanceScore());
        //sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions()) + "/10";
        sessionCounterText.text = "NFT DAILY RUNS : " + (gameplayView.instance.GetSessions());

    }
    public void TryAgain()
    { 
            warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnCharacterSelected;
   
    }
    public void goToMain()
    {
        scenesView.LoadScene(warriorGameModel.mainSceneLoadname.sceneName);
        warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnBackToMenu;
        
    }

    string NameToSlugConvert(string name)
    {
        string slug;
        slug = name.ToLower().Replace(".", "").Replace("'", "").Replace(" ", "-");
        return slug;
    }
}
