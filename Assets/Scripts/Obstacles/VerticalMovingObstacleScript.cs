using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalMovingObstacleScript : MonoBehaviour
{
    public float distance;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {

        transform.DOMoveY(gameObject.transform.position.y + distance, duration).SetLoops(-1, LoopType.Yoyo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject.transform);
    }
}
