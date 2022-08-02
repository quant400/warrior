using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UniRx;
using UnityEngine;


public class scenesView : MonoBehaviour
{
    public static ReactiveCollection<string> loadedScenes = new ReactiveCollection<string>();
    public static string beforeScene;
    public static IObservable<string> loadedScenesAsObservable = loadedScenes.ToObservable();
    void Awake()
    {
        loadedScenes = Enumerable.Range(0, SceneManager.sceneCount).Select(sceneIndex => SceneManager.GetSceneAt(sceneIndex).name).ToReactiveCollection();
    }
    public static void AddScene(string sceneName)
    {
        // ResetARInfo();
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).AsObservable()
            .Take(1)
            .Subscribe(_ => loadedScenes.Add(sceneName));
    }
    public static void UnloadScene(string sceneName)
    {
        beforeScene = sceneName;
        SceneManager.UnloadSceneAsync(sceneName).AsObservable()
            .Take(1)
            .DelayFrame(1)
            .Subscribe(_ => loadedScenes.Remove(sceneName));
    }
    public static void ChangeScene(string sceneName)
    {
        string lastSceneName = loadedScenes.Last();
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).AsObservable()
            .Take(1)
            .Subscribe(_ => { loadedScenes.Add(sceneName); UnloadScene(lastSceneName); });
    }
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
    public static void loadSinglePlayerScene()
    {
        int scene = chickenGameModel.currentNFTSession;
        /*
        switch (scene)
        {
            case 0:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene5.sceneName);
                break;
            case 1:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene4.sceneName);
                break;
            case 2:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene3.sceneName);
                break;
            case 3:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene2.sceneName);
                break;
            case 4:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene1.sceneName);
                break;
            case 5:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene5.sceneName);
                break;
            case 6:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene4.sceneName);
                break;
            case 7:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene3.sceneName);
                break;
            case 8:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene2.sceneName);
                break;
            case 9:
                SceneManager.LoadScene(chickenGameModel.singlePlayerScene1.sceneName);
                break;
        }

        


            if (chickenGameModel.currentNFTSession % 2 == 0)
        {
            SceneManager.LoadScene(chickenGameModel.singlePlayerScene1.sceneName);

        }
        else
        {
            SceneManager.LoadScene(chickenGameModel.singlePlayerScene2.sceneName);

        }*/

        SceneManager.LoadScene(chickenGameModel.singlePlayerScene1.sceneName);
    }
}
