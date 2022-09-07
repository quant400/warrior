using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsCounterText;
    public Text minFpsCounterText;

    public float updateInterval = 0.5f; //How often should the number update

    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;
    float minFps = 1000;


    bool startCalculatingMin;

    // Start is called before the first frame update
    void Start()
    {
        timeleft = updateInterval;

        minFpsCounterText.text = "Min FPS: ";

        startCalculatingMin = false;

        StartCoroutine(startMinCount());
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = (accum / frames);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }

        fpsCounterText.text = "FPS: " + ((int)fps).ToString();

        if(startCalculatingMin)
        {
            if ((fps < minFps))
            {
                //Debug.Log("Min fps");

                minFps = fps;

                minFpsCounterText.text = "Min FPS: " + ((int)minFps).ToString();
            }
        }

    }

    IEnumerator startMinCount()
    {
        yield return new WaitForSeconds(5f);

        startCalculatingMin = true;
    }
}
