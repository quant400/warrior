using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeAdditionPrompt : MonoBehaviour
{
    public float distance;
    public float duration;

    private Vector3 startingPosition;

    private void OnEnable()
    {

        startingPosition = gameObject.transform.localPosition;

        //transform.DOMoveY(gameObject.transform.position.y + distance, duration);

        transform.DOLocalMoveY(gameObject.transform.position.y + distance, duration);
    }

    private void OnDisable()
    {
        PositionReset();
    }

    void PositionReset()
    {
        gameObject.transform.localPosition = startingPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
