using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Warrior
{
    public class ScreenRotationFade : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        // Start is called before the first frame update
        void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();

            canvasGroup.DOFade(0.5f, 1).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnEnable()
        {
            DOTween.Play(canvasGroup);
        }

        private void OnDisable()
        {
            DOTween.Pause(canvasGroup);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
