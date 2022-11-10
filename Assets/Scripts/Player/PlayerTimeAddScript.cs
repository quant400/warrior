using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeAddScript : MonoBehaviour
{
    /*
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = -1;
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chicken"))
        {
            PlayerStats.Instance.timeLeft += 15;

            Destroy(collision.gameObject);
        }
    }
}
