using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScreenOrientation = MarksAssets.ScreenOrientationWebGL.ScreenOrientationWebGL.ScreenOrientation;
using System.Runtime.InteropServices;

public class ScreenRotationManager : MonoBehaviour
{
    public GameObject screenRotationImage;

    #region WebGL is on mobile check

    [DllImport(dllName: "__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL

        return IsMobile();

#endif
        return false;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setOrientation(int orient)
    {
        ScreenOrientation orientation = (ScreenOrientation)orient;

        Debug.Log(orientation.ToString());

        /*
        //the 'if' is obviously unnecessary. I'm just testing if the comparisons are working as expected. It's an example after all, might as well be thorough.
        if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown || orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
            text.text = orientation.ToString();
        */

        if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
        {
            screenRotationImage.SetActive(true);
        }
        else
        {
            screenRotationImage.SetActive(false);
        }
            
    }
}
