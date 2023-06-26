using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Warrior
{
    public class SlimeDrop : MonoBehaviour
    {
        private GameObject slimeDrop;

        public float distance;
        public float duration;

        private bool tweenEnabled = false;

        private Animator animator;


        // Start is called before the first frame update
        void Start()
        {
            slimeDrop = gameObject.transform.GetChild(0).gameObject;

            animator = gameObject.GetComponent<Animator>();

            slimeDrop.transform.DOMoveY(slimeDrop.transform.position.y + distance, duration).SetLoops(-1, LoopType.Restart);

            tweenEnabled = true;

            StartCoroutine(CannonFireDelay(duration));
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDestroy()
        {
            if (tweenEnabled)
            {
                DOTween.Kill(slimeDrop.transform);
            }
        }

        private void OnEnable()
        {
            if (tweenEnabled)
            {
                DOTween.Play(slimeDrop.transform);
            }
        }

        private void OnDisable()
        {
            if (tweenEnabled)
            {
                DOTween.Pause(slimeDrop.transform);
            }
        }

        IEnumerator CannonFireDelay(float secs)
        {
            while (true)
            {
                animator.SetBool("fireCannon", true);

                yield return new WaitForSeconds(secs / 2f);

                animator.SetBool("fireCannon", false);

                yield return new WaitForSeconds(secs / 2f);
            }

        }
    }
}
