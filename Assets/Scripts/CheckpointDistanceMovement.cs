using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckpointDistanceMovement : MonoBehaviour
{

    public float distance;
    public float duration;

    private bool tweenEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 localPosition = transform.localPosition;

        localPosition.x += distance;

        transform.DOLocalMove(localPosition, duration).SetLoops(-1, LoopType.Yoyo);

        tweenEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //transform.DOMoveX(transform.position.x + distance, duration).SetLoops(-1, LoopType.Yoyo);
        if (tweenEnabled)
        {
            DOTween.Play(gameObject.transform);
        }
    }

    private void OnDisable()
    {
        //DOTween.Kill(gameObject.transform);

        if (tweenEnabled)
        {
            DOTween.Pause(gameObject.transform);
        }

        //DOTween.Kill(gameObject.transform);

        //transform.localPosition = startPosition;
    }
}
