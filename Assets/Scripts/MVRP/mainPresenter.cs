using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Toolkit;
using UniRx.Triggers;
using UniRx.Operators;
using System;
using UnityEngine.SceneManagement;

namespace Warrior
{
    public class mainPresenter : MonoBehaviour
    {
        [SerializeField] gameplayView gameView;
        [SerializeField] webLoginView webView;
        [SerializeField] characterSelectionView characterSelectionView;
        [SerializeField] uiView uiView;
        [SerializeField] gameEndView gameEndView;
        [SerializeField] DatabaseManagerRestApi dataView;


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if ((scene.name == warriorGameModel.singlePlayerScene1.sceneName))
            {
                Observable.Timer(TimeSpan.Zero)
                            .DelayFrame(2)
                            .Do(_ => warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnStartGame)
                            .Subscribe()
                            .AddTo(this);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            ObservePanelsStatus();
            SceneManager.sceneLoaded += OnSceneLoaded;

        }

        // Update is called once per frame
        void Update()
        {

        }

        void ObservePanelsStatus()
        {
            warriorGameModel.gameCurrentStep
                   .Subscribe(procedeGame)
                   .AddTo(this);

            void procedeGame(warriorGameModel.GameSteps status)
            {
                switch (status)
                {
                    case warriorGameModel.GameSteps.OnLogin:

                        if (warriorGameModel.userIsLogged.Value)
                        {
                            warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnCharacterSelection;

                        }

                        else
                        {
                            uiView.goToMenu("login");
                            uiView.observeLogin();
                        }
                        break;
                    case warriorGameModel.GameSteps.Onlogged:

                        uiView.goToMenu("main");
                        break;
                    case warriorGameModel.GameSteps.OnPlayMenu:

                        uiView.goToMenu("main");

                        break;
                    case warriorGameModel.GameSteps.OnLeaderBoard:

                        uiView.goToMenu("leaderboeard");
                        break;
                    case warriorGameModel.GameSteps.OnCharacterSelection:
                        uiView.goToMenu("characterSelection");
                        if (!gameplayView.instance.isTryout)
                        {
                            characterSelectionView.MoveRight();
                            characterSelectionView.MoveLeft();
                        }

                        //webView.checkUSerLoggedAtStart(); /// condisder when start load again .....  !!!! 
                        break;
                    case warriorGameModel.GameSteps.OnCharacterSelected:
                        uiView.goToMenu("characterSelected");
                        //gameEndView.resetDisplay();
                        scenesView.loadSinglePlayerScene();

                        break;
                    case warriorGameModel.GameSteps.OnStartGame:
                        Observable.Timer(TimeSpan.Zero)
                            .DelayFrame(2)
                            .Do(_ => gameView.StartGame())
                            .Subscribe()
                            .AddTo(this);

                        warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnGameRunning;

                        break;
                    case warriorGameModel.GameSteps.OnGameRunning:
                        Debug.Log("game Is running");
                        break;
                    case warriorGameModel.GameSteps.OnGameEnded:
                        uiView.goToMenu("results");
                        //if(!gameplayView.instance.isTryout)
                        gameEndView.setScoreAtStart();
                        gameView.EndGame();
                        break;
                    case warriorGameModel.GameSteps.OnBackToCharacterSelection:
                        gameEndView.initializeValues();
                        //gameEndView.resetDisplay();
                        dataView.initilizeValues();
                        scenesView.LoadScene(warriorGameModel.mainSceneLoadname.sceneName);
                        Observable.Timer(TimeSpan.Zero)
                           .DelayFrame(2)
                           .Do(_ => warriorGameModel.gameCurrentStep.Value = warriorGameModel.GameSteps.OnCharacterSelection)
                           .Subscribe()
                           .AddTo(this);
                        break;
                    case warriorGameModel.GameSteps.onSceneLoaded:
                        Debug.Log("sceneLoaded");
                        break;


                }

            }
        }
    }
}


