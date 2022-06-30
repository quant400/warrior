using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Application.runInBackground = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SceneChanger(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        Time.timeScale = 1;
    }
}
