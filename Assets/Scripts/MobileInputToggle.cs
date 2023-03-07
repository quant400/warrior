using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MobileInputToggle : MonoBehaviour
{

    #region WebGL is on mobile check

    [DllImport(dllName:"__Internal")]
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

#if UNITY_WEBGL

        if(!isMobile())
        {
            gameObject.SetActive(false);
        }
#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
