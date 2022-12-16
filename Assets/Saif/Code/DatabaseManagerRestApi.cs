using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataApi;
using UnityEngine.Networking;
using UniRx.Triggers;
using UniRx;
using UniRx.Operators;
using System.Text;

public class DatabaseManagerRestApi : MonoBehaviour
{
    public static DatabaseManagerRestApi _instance;
    ReactiveProperty<int> sessionCounterReactive = new ReactiveProperty<int>();
    string localID;
    public int scoreUpdateTried=0;
    private int limit = 20;


    private void Awake()
    {
        _instance = this;
    }

    private void Update() {
        // if(Input.GetKeyDown(KeyCode.Space))
        //     setScore("00256454","THE RED FIGHTER",200);
    }

    //Most of the calls in the api are async calls, which makes code clean and tidy, and powerful! 
    //async calls are running on threads (depending on the platform) therefore it is advised to avoid threadhopping, and editor events (like button clicks)
    //if not familiar with async/await go and have a read on it! it is awesome :) 
    private async void Start()
    {
        //here we are referencing the api, to make a shorthand for firebase. (cause we are lazy devs, and Firebase.Instance is too long to write every time :))
    }
    public void setScore(string _assetID,string _FighterName,int _score)
    {
        getSessionsCounter(_assetID,ss=>
        {
            if((int)ss <= limit)
            {
                getLeaderboardScore(_assetID,res=>
                {
                    setScoreInLeaderboard(_assetID,_FighterName,_score + (((int)res) == -1 ? 0 : (int)res));
                });
                getDailyLeaderboardScore(_assetID,res=>
                {
                    setScoreInDailyLeaderboard(_assetID,_FighterName,(int)ss,_score + (((int)res) == -1 ? 0 : (int)res));
                });
            }else
                Debug.Log("REACHED DAILY SESSIONS LIMIT...");
        });
        
    }
    public void setScoreRestApiMain(string _assetID, int _score)
    {
        setScoreWithRestApi(_assetID, _score);
    }
    public void setScoreWithRestApi(string assetID,int score)
    {
        
        if (sessionCounterReactive.Value <= limit)
        {
            //StartCoroutine(setScoreInLeaderBoeardRestApi(assetID, score));


            if(!gameplayView.instance.isTryout)
            {
                StartCoroutine(KeyMaker.instance.endSessionApi(assetID, score));
            }
            
        }
        else
        {
            Debug.Log("you reach daily Limits");
        }
        

        //StartCoroutine(KeyMaker.instance.endSessionApi(assetID, score));
    }
  
    public void startSessionFromRestApi(string _assetID)
    {
        scoreUpdateTried = 0;
        //StartCoroutine(startSessionApi("https://api.cryptofightclub.io/game/sdk/warrior/start-session", _assetID));

        StartCoroutine(KeyMaker.instance.startSessionApi(_assetID));
    }

    public void getDataFromRestApi(string assetId)
    {
        StartCoroutine(getDataRestApi(assetId));

        //StartCoroutine(getDataRestApi2(assetId));
    }
    public IEnumerator getSessionCounterAndSetScoreFromApi(string url, string assetId, int score)
    {
        leaderboardModel.userGetDataModel idData = new leaderboardModel.userGetDataModel();
        idData.id = assetId;
        localID = assetId;
        string idJsonData = JsonUtility.ToJson(idData);
        using (UnityWebRequest request = UnityWebRequest.Post(url.ToString(), idJsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(idJsonData) ? null : Encoding.UTF8.GetBytes(idJsonData));
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.error == null)
            {

                checkSessionCounter(Encoding.UTF8.GetString(request.downloadHandler.data));


                //setScoreInLeaderBoard(score);
                Debug.Log(Encoding.UTF8.GetString(request.downloadHandler.data));

            }
            else
            {
                Debug.Log("error in server");
            }


        }



    }
   
    /*
    public IEnumerator setScoreInLeaderBoeardRestApi(int id,  int scoreAdded)
    {
         leaderboardModel.userPostedData postedData = new leaderboardModel.userPostedData();
        postedData.id = id;
        postedData.score = scoreAdded;
        string idJsonData = JsonUtility.ToJson(postedData);

        using (UnityWebRequest request = UnityWebRequest.Put("https://api.cryptofightclub.io/game/sdk/warrior/end-session", idJsonData))
        {
            request.timeout= 5;
            byte[] bodyRaw = Encoding.UTF8.GetBytes(idJsonData);
            request.method = "POST";
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.error == null)
            {

                Debug.Log("posted Score in function");
                Debug.Log(idJsonData);
                //Enable try again button once server responds with new score update.
                gameplayView.instance.gameObject.GetComponent<uiView>().SetTryAgain(true);
                getDataFromRestApi(postedData.id);


            }
            else
            {
                //if server responded with an error and resend score 
                if (gameplayView.instance.GetSessions() <= limit && scoreUpdateTried<10)
                {
                    scoreUpdateTried++;
                    gameplayView.instance.transform.GetComponentInChildren<gameEndView>().Invoke("setScoreAtStart", 6);
                }
                Debug.Log("error in server");
            }


        }
    }*/

    public IEnumerator getDataRestApi(string assetId)
    {
        leaderboardModel.userGetDataModel idData = new leaderboardModel.userGetDataModel();
        idData.id = assetId;
        localID = assetId;
        string idJsonData = JsonUtility.ToJson(idData);
        Debug.Log(idData);

        string url = "";

        //Debug.Log("assetId" + assetId);

        /*
        if (KeyMaker.instance.buildType == BuildType.staging)
        {
            url = "https://staging-api.cryptofightclub.io/game/sdk/warrior/score";
        }
        else if (KeyMaker.instance.buildType == BuildType.production)
        {
            url = "https://api.cryptofightclub.io/game/sdk/warrior/score";
        }
        */

        url = "https://api.cryptofightclub.io/game/sdk/warrior/score";

        using (UnityWebRequest request = UnityWebRequest.Put(url, idJsonData))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(idJsonData);
            request.method = "POST";
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.error == null)
            {
                //Debug.Log(request.downloadHandler.text);

                string result = Encoding.UTF8.GetString(request.downloadHandler.data);

                checkSessionCounter(result);

                if (KeyMaker.instance.buildType == BuildType.staging)
                {
                    Debug.Log(request.downloadHandler.text);
                }
            }
            else
            {
                Debug.Log("error in server");
            }


        }
    }

    /*
    public IEnumerator getDataRestApi2(int assetId)
    {
        leaderboardModel.userGetDataModel idData = new leaderboardModel.userGetDataModel();
        idData.id = assetId;
        //localID = assetId;
        string idJsonData = JsonUtility.ToJson(idData);

        Debug.Log(idJsonData);
        
        using (UnityWebRequest request = UnityWebRequest.Get("https://staging-api.cryptofightclub.io/game/sdk/warrior/longest"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(idJsonData);
            //request.method = "POST";
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);

            if (request.error == null)
            {
                string result = Encoding.UTF8.GetString(request.downloadHandler.data);

                //checkSessionCounter(result);


                Debug.Log(request.downloadHandler.text);

            }
            else
            {
                Debug.Log("error in server");
            }


        }
    }
    */

    /*
    public IEnumerator startSessionApi(string url, int assetId)
    {
        leaderboardModel.userGetDataModel idData = new leaderboardModel.userGetDataModel();
        idData.id = assetId;
        string idJsonData = JsonUtility.ToJson(idData);
        using (UnityWebRequest request = UnityWebRequest.Post(url.ToString(), idJsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(idJsonData) ? null : Encoding.UTF8.GetBytes(idJsonData));
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.error == null)
            {
                getDataFromRestApi(assetId);

                Debug.Log("all is good in server" + Encoding.UTF8.GetString(request.downloadHandler.data));

            }
            else
            {
                Debug.Log("error in server");
            }


        }



    }
    */

    public void checkSessionCounter(string url)
    {

        string MatchData = url;

        if(KeyMaker.instance.buildType == BuildType.staging)
        {
            Debug.Log(MatchData);
        }


        leaderboardModel.assetClass playerData = restApiDataView.JsonUtil.fromJson<leaderboardModel.assetClass>(url);
        if (playerData != null)
        {
            sessionCounterReactive.Value = playerData.dailySessionPlayed;
            gameplayView.instance.dailyScore = playerData.dailyScore;
            gameplayView.instance.sessions = playerData.dailySessionPlayed;
            gameplayView.instance.AlltimeScore = playerData.allTimeScore;
            gameplayView.instance.weeklyScore = playerData.weeklyScore;
            gameplayView.instance.longestDistance = playerData.longestDistance;
            gameplayView.instance.dailysessionReactive.Value = playerData.dailySessionPlayed;

            //Debug.Log("weeklyScore = " + gameplayView.instance.weeklyScore);

        }



    }
    public void initilizeValues()
    {

        sessionCounterReactive.Value = -1;
        gameplayView.instance.dailyScore = -1;
        gameplayView.instance.sessions = -1;
        gameplayView.instance.AlltimeScore = -1;
        gameplayView.instance.weeklyScore = -1;
        gameplayView.instance.longestDistance = -1;
        gameplayView.instance.dailysessionReactive.Value = -1;

    }

    public void getJuiceFromRestApi(string assetId)
    {
        StartCoroutine(getJuiceRestApi(assetId));
    }
    struct reply
    {
        public string id;
        public string balance;
    };
    IEnumerator getJuiceRestApi(string assetId)
    {
        string url = "";

        if(KeyMaker.instance.buildType == BuildType.staging)
        {
            url = "https://staging-api.cryptofightclub.io/game/sdk/juice/balance/";
        }
        else if (KeyMaker.instance.buildType == BuildType.production)
        {
            url = "https://api.cryptofightclub.io/game/sdk/juice/balance/";
        }

        using (UnityWebRequest request = UnityWebRequest.Get(url + assetId))
        {
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.error == null)
            {
                string result = Encoding.UTF8.GetString(request.downloadHandler.data);
                reply r = JsonUtility.FromJson<reply>(request.downloadHandler.text);
                if (KeyMaker.instance.buildType == BuildType.staging)
                    Debug.Log(request.downloadHandler.text);
                gameplayView.instance.SetJuiceBal(r.balance);

            }
            else
            {
                Debug.Log("error in server");
            }


        }
    }

    public void SetDemoScore(string _assetID, string _FighterName, int _score)
    {
        SetDemoScoreInLeaderboard(_assetID, _FighterName, _score);
        SetDemoScoreInDailyLeaderboard(_assetID, _FighterName, 1, _score);
    }
    public async void SetDemoScoreInLeaderboard(string _assetID, string _name, int newScore)
    {
       
    }
    public async void SetDemoScoreInDailyLeaderboard(string _assetID, string _name, int _sessionCounter, int newScore)
    {
        
    }

    public async void setScoreInLeaderboard(string _assetID,string _name,int newScore)
    {
        
    
    }
    public async void setScoreInDailyLeaderboard(string _assetID,string _name,int _sessionCounter,int newScore)
    {
        

        
    }
    public async void getDailyLeaderboardScore(string _assetID,Action<long> result)
    {
       
    }

    public async void getLeaderboardScore(string _assetID,Action<long> result)
    {
      
    }
    public async void getSessionsCounter(string _assetID,Action<long> result)
    {
       
    }
    public async void increaseSessionCounter(string _assetID)
    {
           }

}
