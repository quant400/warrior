using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentScript : MonoBehaviour
{
    public static SegmentScript Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public int segmentSelected = -5;

    public bool easyOnly = false;

    public bool mediumOnly = false;

    public bool hardOnly = false;

    // Start is called before the first frame update
    void Start()
    {
        segmentSelected = -5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
